import {
  OpenAI,
} from '@lobehub/icons';
import { memo } from 'react';

interface ModelIconProps {
  model?: string;
  size?: number;
}

const ModelIcon = memo<ModelIconProps>(({ model, size = 12 }) => {
  if (!model) return;
  return <OpenAI size={size} />;
});

export default ModelIcon;
