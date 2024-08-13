import {
  OpenAI,
} from '@lobehub/icons';
import { memo } from 'react';
interface ModelProviderIconProps {
  provider?: string;
}

const ModelProviderIcon = memo<ModelProviderIconProps>(({ provider }) => {
  return <OpenAI size={20} />;
});

export default ModelProviderIcon;
