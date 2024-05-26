
import { ChatModelCard, ModelProviderCard } from "@/types/llm";
import OpenAIProvider from './openai';

export const filterEnabledModels = (provider: ModelProviderCard) => {
  return provider.chatModels.filter((v) => v.enabled).map((m) => m.id);
};

export const LOBE_DEFAULT_MODEL_LIST: ChatModelCard[] = [
  OpenAIProvider.chatModels,
].flat();


export const DEFAULT_MODEL_PROVIDER_LIST = [
  OpenAIProvider,
];
export { default as OpenAIProviderCard } from './openai';