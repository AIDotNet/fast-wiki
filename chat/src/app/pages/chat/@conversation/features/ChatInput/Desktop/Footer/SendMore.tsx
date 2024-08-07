import { Icon } from '@lobehub/ui';
import { Button, Dropdown } from 'antd';
import { createStyles } from 'antd-style';
import {  LucideCheck, LucideChevronDown } from 'lucide-react';
import { memo } from 'react';
import { useHotkeys } from 'react-hotkeys-hook';
import { ALT_KEY } from '@/const/hotkeys';

const useStyles = createStyles(({ css, prefixCls }) => {
  return {
    arrow: css`
      &.${prefixCls}-btn.${prefixCls}-btn-icon-only {
        width: 28px;
      }
    `,
  };
});

interface SendMoreProps {
  disabled?: boolean;
  isMac?: boolean;
}

const SendMore = memo<SendMoreProps>(({ disabled }) => {

  const { styles } = useStyles();

  const hotKey = [ALT_KEY, 'enter'].join('+');
  useHotkeys(
    hotKey,
    (keyboardEvent, hotkeysEvent) => {
      console.log(keyboardEvent, hotkeysEvent);
    },
    {
      enableOnFormTags: true,
      preventDefault: true,
    },
  );

  return (
    <Dropdown
      disabled={disabled}
      menu={{
        items: [
          {
            icon:<Icon icon={LucideCheck} /> ,
            key: 'sendWithEnter',
            label: '发送消息',
            onClick: () => {
              
            },
          },
          {
            icon:<Icon icon={LucideCheck} />,
            key: 'sendWithCmdEnter',
            label: '快捷键发送消息',
            onClick: () => {
              
            },
          },
          { type: 'divider' }
        ],
      }}
      placement={'topRight'}
      trigger={['hover']}
    >
      <Button
        aria-label={'More'}
        className={styles.arrow}
        icon={<Icon icon={LucideChevronDown} />}
        type={'primary'}
      />
    </Dropdown>
  );
});

SendMore.displayName = 'SendMore';

export default SendMore;
