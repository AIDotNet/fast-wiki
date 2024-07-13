import { lazy, memo, Suspense } from 'react';

import { useAgentStore } from '@/store/agent';
import { agentSelectors } from '@/store/agent/slices/chat';
import { useUserStore } from '@/store/user';
import { modelProviderSelectors } from '@/store/user/selectors';

const LargeTokenContent = lazy(() => import('./TokenTag'));

const Token = memo(() => {
  const model = useAgentStore(agentSelectors.currentAgentModel);
  const showTag = useUserStore(modelProviderSelectors.isModelHasMaxToken(model));

  return (
    <Suspense fallback={<div>Loading...</div>}>
      {showTag && <LargeTokenContent />}
    </Suspense>
  );
});

export default Token;
