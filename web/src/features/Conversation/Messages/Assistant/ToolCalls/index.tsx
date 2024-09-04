import { Avatar, Icon } from '@lobehub/ui';
import isEqual from 'fast-deep-equal';
import { Loader2, LucideChevronDown, LucideChevronRight, LucideToyBrick } from 'lucide-react';
import { CSSProperties, memo, useState } from 'react';
import { useTranslation } from 'react-i18next';
import { Center, Flexbox } from 'react-layout-kit';

import { useChatStore } from '@/store/chat';
import { chatSelectors } from '@/store/chat/selectors';

import Arguments from '../../components/Arguments';
import { useStyles } from './style';

export interface InspectorProps {
  arguments?: string;
  identifier: string;
  index: number;
  messageId: string;
  style: CSSProperties;
}

const CallItem = memo<InspectorProps>(
  ({ arguments: requestArgs, messageId, index, identifier, style }) => {
    const { t } = useTranslation('plugin');
    const { styles } = useStyles();
    const [open, setOpen] = useState(false);
    const loading = useChatStore(chatSelectors.isToolCallStreaming(messageId, index));

    const avatar = <Icon icon={LucideToyBrick} />;

    return (
      <Flexbox gap={8} style={style}>
        <Flexbox
          align={'center'}
          className={styles.container}
          distribution={'space-between'}
          gap={8}
          height={32}
          horizontal
          onClick={() => {
            setOpen(!open);
          }}
        >
          <Flexbox align={'center'} gap={8} horizontal>
            {loading ? (
              <Center height={30} width={24}>
                <Icon icon={Loader2} spin />
              </Center>
            ) : (
              avatar
            )}
          </Flexbox>
          <Icon icon={open ? LucideChevronDown : LucideChevronRight} />
        </Flexbox>
        {(open || loading) && <Arguments arguments={requestArgs} />}
      </Flexbox>
    );
  },
);

export default CallItem;
