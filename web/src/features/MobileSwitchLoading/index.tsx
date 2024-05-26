'use client';

import { useTranslation } from 'react-i18next';

import FullscreenLoading from '@/components/FullscreenLoading';

const MobileSwitchLoading = () => {
  const { t } = useTranslation('common')as any;

  return <FullscreenLoading title={t('layoutInitializing')} />;
};

export default MobileSwitchLoading;
