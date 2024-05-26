import { ModelProviderCard } from '@/types/llm';

// ref https://platform.openai.com/docs/models
const OpenAI: ModelProviderCard = {
  chatModels: [
    {
      description: 'GPT 3.5 Turbo，适用于各种文本生成和理解任务',
      enabled: true,
      functionCall: true,
      id: 'gpt-3.5-turbo',
      tokens: 16_385,
    },
    {
      functionCall: true,
      id: 'gpt-3.5-turbo-0125',
      tokens: 16_385,
    },
    {
      functionCall: true,
      id: 'gpt-3.5-turbo-1106',
      tokens: 16_385,
    },
    {
      id: 'gpt-3.5-turbo-instruct',
      tokens: 4096,
    },
    {
      id: 'gpt-3.5-turbo-16k',
      tokens: 16_385,
    },
    {
      name: 'GPT-3.5 Turbo (0613)',
      id: 'gpt-3.5-turbo-0613',
      legacy: true,
      tokens: 4096,
    },
    {
      name: 'GPT-3.5 Turbo 16K (0613)',
      id: 'gpt-3.5-turbo-16k-0613',
      legacy: true,
      tokens: 4096,
    },
    {
      name: 'GPT-4 Turbo Preview',
      functionCall: true,
      id: 'gpt-4-turbo-preview',
      tokens: 128_000,
    },
    {
      name: 'GPT-4 Turbo Preview (0125)',
      functionCall: true,
      id: 'gpt-4-0125-preview',
      tokens: 128_000,
    },
    {
      description: 'GPT-4 视觉预览版，支持视觉任务',
      name: 'GPT-4 Turbo Vision Preview',
      id: 'gpt-4-vision-preview',
      tokens: 128_000,
      vision: true,
    },
    {
      name: 'GPT-4 Turbo Vision Preview (1106)',
      id: 'gpt-4-1106-vision-preview',
      tokens: 128_000,
      vision: true,
    },
    {
      name: 'GPT-4 Turbo Preview (1106)',
      functionCall: true,
      id: 'gpt-4-1106-preview',
      tokens: 128_000,
    },
    {
      name: 'GPT-4',
      functionCall: true,
      id: 'gpt-4',
      tokens: 8192,
    },
    {
      name: 'GPT-4 (0613)',
      functionCall: true,
      id: 'gpt-4-0613',
      tokens: 8192,
    },
    {
      name: 'GPT-4 32K',
      functionCall: true,
      id: 'gpt-4-32k',
      tokens: 32_768,
    },
    {
      name: 'GPT-4 32K (0613)',
      functionCall: true,
      id: 'gpt-4-32k-0613',
      tokens: 32_768,
    },
    {
      description: 'GPT-4 Turbo 视觉版',
      name: 'GPT-4 Turbo',
      enabled: true,
      functionCall: true,
      id: 'gpt-4-turbo',
      tokens: 128_000,
      vision: true,
    },
    {
      description: 'GPT-4 Turbo 视觉版 (240409)',
      name: 'GPT-4 Turbo Vision (240409)',
      functionCall: true,
      id: 'gpt-4-turbo-2024-04-09',
      tokens: 128_000,
      vision: true,
    },
  ],
  enabled: true,
  id: 'openai',
};

export default OpenAI;
