

import { MobileChatInputArea, MobileChatSendButton } from '@lobehub/ui';
import { useTheme } from 'antd-style';
import { memo } from 'react';

import { useChatInput } from '@/features/ChatInput/useChatInput';


const MobileChatInput = memo(() => {
  const theme = useTheme();
  const { ref, onSend, loading, value, onInput, onStop, expand, setExpand } = useChatInput();

  return (
    <MobileChatInputArea
      expand={expand}
      loading={loading}
      onInput={onInput}
      onSend={onSend}
      placeholder={"输入消息..."}
      ref={ref}
      setExpand={setExpand}
      style={{
        background: theme.colorBgLayout,
        top: expand ? 0 : undefined,
        width: '100%',
        zIndex: 101,
      }}
      textAreaRightAddons={
        <MobileChatSendButton loading={loading} onSend={onSend} onStop={onStop} />
      }
      topAddons={
        <>
        </>
      }
      value={value}
    />
  );
});

MobileChatInput.displayName = 'MobileChatInput';

export default MobileChatInput;
