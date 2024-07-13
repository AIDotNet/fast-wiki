import { memo, useLayoutEffect } from 'react';
import { createStoreUpdater } from 'zustand-utils';

import { useChatStore } from '@/store/chat';

// sync outside state to useChatStore
const ChatHydration = memo(() => {
  const useStoreUpdater = createStoreUpdater(useChatStore);

  // two-way bindings the topic params to chat store
  const topic = new URLSearchParams(window.location.search).get('topic');
  useStoreUpdater('activeTopicId', topic);

  useLayoutEffect(() => {
    const unsubscribe = useChatStore.subscribe(
      (s) => s.activeTopicId,
      (state) => {
        const searchParams = new URLSearchParams(window.location.search);
        if (!state) {
          searchParams.delete('topic');
        } else {
          searchParams.set('topic', state);
        }
        window.history.replaceState(null, '', `?${searchParams.toString()}`);
      },
    );

    return () => {
      unsubscribe();
    };
  }, []);

  return null;
});

export default ChatHydration;
