import {
  Anthropic,
  Azure,
  Bedrock,
  Google,
  Groq,
  Minimax,
  Mistral,
  Moonshot,
  Ollama,
  OpenAI,
  OpenRouter,
  Perplexity,
  Together,
  ZeroOne,
  Zhipu,
} from '@lobehub/icons';
import { memo } from 'react';

interface ModelProviderIconProps {
  provider?: string;
}

const ModelProviderIcon = memo<ModelProviderIconProps>(({ provider }) => {
  return <OpenAI size={20} />;
});

export default ModelProviderIcon;
