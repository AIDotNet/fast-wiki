import { memo } from 'react';
import { Flexbox } from 'react-layout-kit';

import ModelTag from '@/components/ModelTag';
import ModelSwitchPanel from '@/features/ModelSwitchPanel';
import { useAgentStore } from '@/store/agent';
import { agentSelectors } from '@/store/agent/selectors';
import { useUserStore } from '@/store/user';
import { modelProviderSelectors } from '@/store/user/selectors';

import PluginTag from '../../../features/PluginTag';
import { Tag } from '@lobehub/ui';

const TitleTags = memo(() => {
  const [model] = useAgentStore((s) => [
    agentSelectors.currentAgentModel(s),
    agentSelectors.currentAgentPlugins(s),
  ]);

  // const showPlugin = useUserStore(modelProviderSelectors.isModelEnabledFunctionCall(model));

  // TODO: 显示应用
  return (
    <Flexbox align={'center'} horizontal>
      {/* <ModelSwitchPanel>
        <ModelTag model={model} />
      </ModelSwitchPanel> */}
      {/* <Tag>
        应用：TokenAI
      </Tag> */}
      {/* {showPlugin && plugins?.length > 0 && <PluginTag plugins={plugins} />} */}
    </Flexbox>
  );
});

export default TitleTags;
