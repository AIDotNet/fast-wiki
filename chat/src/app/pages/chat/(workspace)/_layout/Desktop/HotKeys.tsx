
import { useCallback } from 'react';
import { useHotkeys } from 'react-hotkeys-hook';

import { ALT_KEY, CLEAN_MESSAGE_KEY, META_KEY, REGENERATE_KEY } from '@/const/hotkeys';

const HotKeys = () => {
  const resendHotkeys = [ALT_KEY, REGENERATE_KEY].join('+');

  const clearHotkeys = [META_KEY, ALT_KEY, CLEAN_MESSAGE_KEY].join('+');

  const resetConversation = useCallback(() => {

  }, []);

  useHotkeys(clearHotkeys, resetConversation, {
    enableOnFormTags: true,
    preventDefault: true,
  });

  useHotkeys(
    resendHotkeys,
    () => {
      console.log('resend');

    },
    {
      enableOnFormTags: true,
      preventDefault: true,
    },
  );

  return null;
};

export default HotKeys;
