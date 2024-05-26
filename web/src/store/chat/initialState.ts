import { ChatMessageState, initialMessageState } from './slices/message/initialState';
import { ChatToolState, initialToolState } from './slices/tool/initialState';
import { ChatTopicState, initialTopicState } from './slices/topic/initialState';

export type ChatStoreState = ChatTopicState & ChatMessageState & ChatToolState;

export const initialState: ChatStoreState = {
  ...initialMessageState,
  ...initialTopicState,
  ...initialToolState,
};
