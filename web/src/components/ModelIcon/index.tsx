import {
  OpenAI,
} from '@lobehub/icons';
import { memo } from 'react';

interface ModelProviderIconProps {
  model?: string;
  size?: number;
}

const ModelIcon = memo<ModelProviderIconProps>(({ model: originModel, size = 12 }) => {
  if (!originModel) return;

  // lower case the origin model so to better match more model id case
  const model = originModel.toLowerCase();

  // currently supported models, maybe not in its own provider
  if (model.includes('gpt-3')) return <OpenAI.Avatar size={size} type={'gpt3'} />;
  if (model.includes('gpt-4')) return <OpenAI.Avatar size={size} type={'gpt4'} />;

  return <OpenAI.Avatar size={size} type={model as any} />;
});

export default ModelIcon;
