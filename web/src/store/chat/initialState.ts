import { ChatMessageState, initialMessageState } from './slices/message/initialState';
import { ChatTopicState, initialTopicState } from './slices/topic/initialState';

export type ChatStoreState = ChatTopicState & ChatMessageState ;

export const initialState: ChatStoreState = {
  ...initialMessageState,
  ...initialTopicState,
};
