

import isEqual from 'fast-deep-equal';
import { memo } from 'react';
import { useTranslation } from 'react-i18next';

import PageTitle from '@/components/PageTitle';
import { useSessionStore } from '@/store/session';
import { sessionMetaSelectors } from '@/store/session/selectors';

const EditPage = memo(() => {
  const { t } = useTranslation('setting');
  const [title] = useSessionStore((s) => [
    sessionMetaSelectors.currentAgentTitle(s),
  ]);

  return (
    <>
      <PageTitle title={t('header.sessionWithName', { name: title })} />
    </>
  );
});

export default EditPage;
