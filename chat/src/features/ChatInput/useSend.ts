import { useCallback } from 'react';

import { useChatStore } from '@/store/chat';

export const useSendMessage = () => {
  const { updateInputMessage } = useChatStore();

  return useCallback(() => {


    updateInputMessage('');

  }, []);
};
