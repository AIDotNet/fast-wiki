import { ActionIcon } from '@lobehub/ui';
import { Book, Github } from 'lucide-react';
import Link from 'next/link';
import { memo } from 'react';
import { useTranslation } from 'react-i18next';

import { DOCUMENTS, GITHUB } from '@/const/url';

const BottomActions = memo(() => {
  const { t } = useTranslation('common')as any;

  return (
    <>
      <Link aria-label={'GitHub'} href={GITHUB} target={'_blank'}>
        <ActionIcon icon={Github} placement={'right'} title={'GitHub'} />
      </Link>
      
      {/* @ts-ignore */}
      <Link aria-label={t('document')} href={DOCUMENTS} target={'_blank'}>
      {/* @ts-ignore */}
        <ActionIcon icon={Book} placement={'right'} title={t('document')} />
      </Link>
    </>
  );
});

export default BottomActions;
