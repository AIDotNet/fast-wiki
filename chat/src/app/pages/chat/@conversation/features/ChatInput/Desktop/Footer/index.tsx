import { Icon } from '@lobehub/ui';
import { Button, Skeleton, Space } from 'antd';
import { createStyles } from 'antd-style';
import { ChevronUp, CornerDownLeft, LucideCommand } from 'lucide-react';
import { rgba } from 'polished';
import { memo, useEffect, useState } from 'react';
import { Center, Flexbox } from 'react-layout-kit';

import StopLoadingIcon from '@/components/StopLoading';
import { useSendMessage } from '@/features/ChatInput/useSend';
import { useChatStore } from '@/store/chat';
import SendMore from './SendMore';

const useStyles = createStyles(({ css, prefixCls, token }) => {
  return {
    arrow: css`
      &.${prefixCls}-btn.${prefixCls}-btn-icon-only {
        width: 28px;
      }
    `,
    loadingButton: css`
      display: flex;
      align-items: center;
    `,
    overrideAntdIcon: css`
      .${prefixCls}-btn.${prefixCls}-btn-icon-only {
        display: flex;
        align-items: center;
        justify-content: center;
      }

      .${prefixCls}-btn.${prefixCls}-dropdown-trigger {
        &::before {
          background-color: ${rgba(token.colorBgLayout, 0.1)} !important;
        }
      }
    `,
  };
});

interface FooterProps {
  setExpand?: (expand: boolean) => void;
}

const Footer = memo<FooterProps>(({ setExpand }) => {

  const { theme, styles } = useStyles();

  const {
    isHasMessageLoading,
    isAIGenerating,
    stopGenerateMessage,
  } = useChatStore();



  const sendMessage = useSendMessage();

  const [isMac, setIsMac] = useState<boolean>();
  useEffect(() => {
    setIsMac(false);
  }, [setIsMac]);

  const cmdEnter = (
    <Flexbox gap={2} horizontal>
      {typeof isMac === 'boolean' ? (
        <Icon icon={isMac ? LucideCommand : ChevronUp} />
      ) : (
        <Skeleton.Node active style={{ height: '100%', width: 12 }}>
          {' '}
        </Skeleton.Node>
      )}
      <Icon icon={CornerDownLeft} />
    </Flexbox>
  );

  const enter = (
    <Center>
      <Icon icon={CornerDownLeft} />
    </Center>
  );

  const sendShortcut = enter;

  const wrapperShortcut = cmdEnter;

  const buttonDisabled =
    isHasMessageLoading;

  return (
    <Flexbox
      align={'end'}
      className={styles.overrideAntdIcon}
      distribution={'space-between'}
      flex={'none'}
      gap={8}
      horizontal
      padding={'0 24px'}
    >
      <Flexbox align={'center'} gap={8} horizontal style={{ overflow: 'hidden' }}>
      </Flexbox>
      <Flexbox align={'center'} flex={'none'} gap={8} horizontal>
        <Flexbox
          gap={4}
          horizontal
          style={{ color: theme.colorTextDescription, fontSize: 12, marginRight: 12 }}
        >
          {sendShortcut}
          <span>
            发送
          </span>
          <span>/</span>
          {wrapperShortcut}
          <span>
            换行
          </span>
        </Flexbox>
        <Flexbox style={{ minWidth: 92 }}>
          {isAIGenerating ? (
            <Button
              className={styles.loadingButton}
              icon={<StopLoadingIcon />}
              onClick={stopGenerateMessage}
            >
              停止
            </Button>
          ) : (
            <Space.Compact>
              <Button
                disabled={buttonDisabled}
                loading={buttonDisabled}
                onClick={() => {
                  sendMessage();
                  setExpand?.(false);
                }}
                type={'primary'}
              >
                发送
              </Button>
              <SendMore disabled={buttonDisabled} isMac={isMac} />
            </Space.Compact>
          )}
        </Flexbox>
      </Flexbox>
    </Flexbox>
  );
});

Footer.displayName = 'Footer';

export default Footer;
