import { TextAreaRef } from 'antd/es/input/TextArea';
import { useCallback, useRef, useState } from 'react';

import { useChatStore } from '@/store/chat';
import { useUserStore } from '@/store/user';

import { useSendMessage } from './useSend';

export const useChatInput = () => {
  const ref = useRef<TextAreaRef>(null);
  const [expand, setExpand] = useState<boolean>(false);
  const onSend = useSendMessage();

  const { canUpload } = useUserStore();

  const { loading, value, onInput, onStop } = useChatStore();

  const handleSend = useCallback(() => {
    setExpand(false);

    onSend();
  }, [onSend]);

  return {
    canUpload,
    expand,
    loading,
    onInput,
    onSend: handleSend,
    onStop,
    ref,
    setExpand,
    value,
  };
};
