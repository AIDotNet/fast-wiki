'use client';

import { ChatHeaderTitle } from '@lobehub/ui';
import { memo, useEffect, useState } from 'react';
import { useTranslation } from 'react-i18next';
import { Flexbox } from 'react-layout-kit';

import { useOpenChatSettings } from '@/hooks/useInterceptingRoutes';
import { useSessionStore } from '@/store/session';
import { sessionMetaSelectors, sessionSelectors } from '@/store/session/selectors';

import Tags from './Tags';
import { useApplication } from '@/hooks/useGreeting';

const Main = memo(() => {
  const { t } = useTranslation('chat') as any
  const application = useApplication() as any;

  const [isInbox, description,] = useSessionStore((s) => [
    sessionSelectors.isSomeSessionActive(s),
    sessionSelectors.isInboxSession(s),
    sessionMetaSelectors.currentAgentTitle(s),
    sessionMetaSelectors.currentAgentDescription(s),
    sessionMetaSelectors.currentAgentAvatar(s),
    sessionMetaSelectors.currentAgentBackgroundColor(s),
  ]);

  const displayDesc = isInbox ? t('inbox.desc') : description;

  return (
    <Flexbox align={'flex-start'} gap={12} horizontal>
      <ChatHeaderTitle desc={displayDesc} tag={<Tags />} title={application?.name} />
    </Flexbox>
  )
});

export default Main;
