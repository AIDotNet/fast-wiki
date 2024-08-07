

import { Icon } from '@lobehub/ui';
import { useTheme } from 'antd-style';
import { Loader2Icon } from 'lucide-react';
import { memo, useCallback, useEffect, useRef, useState } from 'react';
import { Center, Flexbox } from 'react-layout-kit';
import { Virtuoso, VirtuosoHandle } from 'react-virtuoso';
import { WELCOME_GUIDE_CHAT_ID } from '@/const/session';
import { useChatStore } from '@/store/chat';
import { useSessionStore } from '@/store/session';

import AutoScroll from '../AutoScroll';
import InboxWelcome from '../InboxWelcome';
import SkeletonList from '../SkeletonList';

interface VirtualizedListProps {
  mobile?: boolean;
}
const VirtualizedList = memo<VirtualizedListProps>(({ mobile }) => {
  const virtuosoRef = useRef<VirtuosoHandle>(null);
  const [atBottom, setAtBottom] = useState(true);
  const [isScrolling, setIsScrolling] = useState(false);

  const { id } = useChatStore();

  const { activeTopicId, useFetchMessages, isFirstLoading, isCurrentChatLoaded } = useChatStore();

  const { sessionId } = useSessionStore();
  useFetchMessages(sessionId, activeTopicId);

  const { data } = useChatStore();

  useEffect(() => {
    if (virtuosoRef.current) {
      virtuosoRef.current.scrollToIndex({ align: 'end', behavior: 'auto', index: 'LAST' });
    }
  }, [id]);

  const prevDataLengthRef = useRef(data.length);

  const getFollowOutput = useCallback(() => {
    const newFollowOutput = data.length > prevDataLengthRef.current ? 'auto' : false;
    prevDataLengthRef.current = data.length;
    return newFollowOutput;
  }, [data.length]);

  const theme = useTheme();
  // overscan should be 1.5 times the height of the window
  const overscan = typeof window !== 'undefined' ? window.innerHeight * 1.5 : 0;

  const itemContent = useCallback(
    // @ts-ignore
    (index: number, id: string) => {
      if (id === WELCOME_GUIDE_CHAT_ID) return <InboxWelcome />;

      return <div style={{ height: 24 + (mobile ? 0 : 64) }} />
    },
    [mobile],
  );

  // first time loading or not loaded
  if (isFirstLoading) return <SkeletonList mobile={mobile} />;

  if (!isCurrentChatLoaded)
    // use skeleton list when not loaded in server mode due to the loading duration is much longer than client mode
    return <Center height={'100%'} width={'100%'}>
      <Icon
        icon={Loader2Icon}
        size={{ fontSize: 32 }}
        spin
        style={{ color: theme.colorTextTertiary }}
      />
    </Center>;

  return (
    <Flexbox height={'100%'}>
      <Virtuoso
        atBottomStateChange={setAtBottom}
        atBottomThreshold={50 * (mobile ? 2 : 1)}
        computeItemKey={(_, item) => item}
        data={data}
        followOutput={getFollowOutput}
        // increaseViewportBy={overscan}
        initialTopMostItemIndex={data?.length - 1}
        isScrolling={setIsScrolling}
        itemContent={itemContent}
        overscan={overscan}
        ref={virtuosoRef}
      />
      <AutoScroll
        atBottom={atBottom}
        isScrolling={isScrolling}
        onScrollToBottom={(type: any) => {
          const virtuoso = virtuosoRef.current;
          switch (type) {
            case 'auto': {
              virtuoso?.scrollToIndex({ align: 'end', behavior: 'auto', index: 'LAST' });
              break;
            }
            case 'click': {
              virtuoso?.scrollToIndex({ align: 'end', behavior: 'smooth', index: 'LAST' });
              break;
            }
          }
        }}
      />
    </Flexbox>
  );
});

export default VirtualizedList;
