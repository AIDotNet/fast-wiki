import {
  Anthropic,
  Google,
  Groq,
  Minimax,
  Mistral,
  Moonshot,
  OpenAI,
  OpenRouter,
  Perplexity,
  Together,
  ZeroOne,
  Zhipu,
} from '@lobehub/icons';
import { useTheme } from 'antd-style';
import { memo } from 'react';

import { ModelProvider } from '@/libs/agent-runtime';

interface ProviderAvatarProps {
  provider: ModelProvider;
}

const ProviderAvatar = memo<ProviderAvatarProps>(({ provider }) => {
  const theme = useTheme();
  return <OpenAI color={theme.colorText} size={64} />;
});

export default ProviderAvatar;
