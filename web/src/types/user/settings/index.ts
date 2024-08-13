import type { LobeAgentSettings } from '@/types/session';

import { UserGeneralConfig } from './general';
import { UserKeyVaults } from './keyVaults';
import { UserModelProviderConfig } from './modelProvider';
import { UserSystemAgentConfig } from './systemAgent';
import { UserToolConfig } from './tool';

export type UserDefaultAgent = LobeAgentSettings;

export * from './general';
export * from './keyVaults';
export * from './modelProvider';
export * from './systemAgent';

/**
 * 配置设置
 */
export interface UserSettings {
  defaultAgent: UserDefaultAgent;
  general: UserGeneralConfig;
  keyVaults: UserKeyVaults;
  languageModel: UserModelProviderConfig;
  systemAgent: UserSystemAgentConfig;
  tool: UserToolConfig;
}
