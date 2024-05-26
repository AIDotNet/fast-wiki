export const LOBE_CHAT_TRACE_HEADER = 'X-lobe-trace';
export const LOBE_CHAT_TRACE_ID = 'X-lobe-chat-trace-id';
export const LOBE_CHAT_OBSERVATION_ID = 'X-lobe-observation-id';

let FAST_API_URL = 'http://localhost:5124';

// 从环境变量中获取
if (process.env.FAST_API_URL) {
  FAST_API_URL = process.env.FAST_API_URL;
}

if(typeof window !== 'undefined' && window.FAST_API_URL){
  FAST_API_URL = window.FAST_API_URL;
}

export {
  FAST_API_URL
}

export enum TraceNameMap {
  ConnectivityChecker = 'Connectivity Checker',
  Conversation = 'Conversation',
  EmojiPicker = 'Emoji Picker',
  FetchPluginAPI = 'Fetch Plugin API',
  InvokePlugin = 'Invoke Plugin',
  LanguageDetect = 'Language Detect',
  SummaryAgentDescription = 'Summary Agent Description',
  SummaryAgentTags = 'Summary Agent Tags',
  SummaryAgentTitle = 'Summary Agent Title',
  SummaryTopicTitle = 'Summary Topic Title',
  Translator = 'Translator',
  // mean user have relative events
  UserEvents = 'User Events',
}

export enum TraceEventType {
  CopyMessage = 'Copy Message',
  DeleteAndRegenerateMessage = 'Delete And Regenerate Message',
  ModifyMessage = 'Modify Message',
  RegenerateMessage = 'Regenerate Message',
}

export enum TraceTopicType {
  AgentSettings = 'Agent Settings',
}

export enum TraceTagMap {
  Chat = 'Chat Competition',
  SystemChain = 'System Chain',
  ToolCalling = 'Tool Calling',
  ToolsCall = 'Tools Call',
}

export interface TracePayload {
  /**
   * if user allow to trace
   */
  enabled?: boolean;
  observationId?: string;
  /**
   * chat session: agentId or groupId
   */
  sessionId?: string;
  tags?: string[];
  /**
   * chat topicId
   */
  topicId?: string;
  traceId?: string;
  traceName?: TraceNameMap;
  /**
   * user uuid
   */
  userId?: string;
}
