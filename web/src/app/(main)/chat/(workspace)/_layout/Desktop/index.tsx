import { Flexbox } from 'react-layout-kit';

import { LayoutProps } from '../type';
import ChatHeader from './ChatHeader';
import HotKeys from './HotKeys';
import TopicPanel from './TopicPanel';
import Topic from '../../@topic/default';
import ChatConversation from '../../@conversation/default';

const Layout = ({ children }: LayoutProps) => {
  return (
    <>
      <ChatHeader />
      <Flexbox
        height={'100%'}
        horizontal
        style={{ overflow: 'hidden', position: 'relative' }}
        width={'100%'}
      >
        <Flexbox
          height={'100%'}
          style={{ overflow: 'hidden', position: 'relative' }}
          width={'100%'}
        >
          <ChatConversation />
        </Flexbox>
        {children}
        <TopicPanel>
          <Topic />
        </TopicPanel>
      </Flexbox>
      <HotKeys />
    </>
  );
};

Layout.displayName = 'DesktopConversationLayout';

export default Layout;
