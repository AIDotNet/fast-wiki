import { merge } from '@/utils/merge';

import { DEFAULT_FEATURE_FLAGS, mapFeatureFlagsEnvToState } from './schema';
import { parseFeatureFlag } from './utils/parser';

const env = {
  FEATURE_FLAGS: process.env.FEATURE_FLAGS,
};

export const getServerFeatureFlagsValue = () => {
  const flags = parseFeatureFlag(env.FEATURE_FLAGS);

  return merge(DEFAULT_FEATURE_FLAGS, flags);
};

export const serverFeatureFlags = () => {
  const serverConfig = getServerFeatureFlagsValue();

  return mapFeatureFlagsEnvToState(serverConfig);
};

export * from './schema';
