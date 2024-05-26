/* eslint-disable sort-keys-fix/sort-keys-fix */
// ******* Runtime Biz Error ******* //
export const AgentRuntimeErrorType = {
  AgentRuntimeError: 'AgentRuntimeError', // Agent Runtime 模块运行时错误
  LocationNotSupportError: 'LocationNotSupportError',

  OpenAIBizError: 'OpenAIBizError',

  NoOpenAIAPIKey: 'NoOpenAIAPIKey',
} as const;

export type ILobeAgentRuntimeErrorType =
  (typeof AgentRuntimeErrorType)[keyof typeof AgentRuntimeErrorType];
