import { TextAreaRef } from 'antd/es/input/TextArea';
import { RefObject, useEffect } from 'react';

import { useChatStore } from '@/store/chat';

export const useAutoFocus = (inputRef: RefObject<TextAreaRef>) => {
  const chatKey = useChatStore();

  useEffect(() => {
    inputRef.current?.focus();
  }, [chatKey]);
};
