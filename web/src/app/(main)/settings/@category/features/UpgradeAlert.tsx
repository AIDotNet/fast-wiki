import { Alert } from '@lobehub/ui';
import { Button } from 'antd';
import { memo } from 'react';
import { useTranslation } from 'react-i18next';

import { MANUAL_UPGRADE_URL } from '@/const/url';
import { useGlobalStore } from '@/store/global';
import { Link } from 'react-router-dom';

const UpgradeAlert = memo(() => {
  const [hasNewVersion, latestVersion] = useGlobalStore((s) => [s.hasNewVersion, s.latestVersion]);
  const { t } = useTranslation('common');

  if (!hasNewVersion) return null;

  return (
    <Alert
      action={
        <Link
          aria-label={t('upgradeVersion.action')}
          to={MANUAL_UPGRADE_URL}
          style={{ marginBottom: 12 }}
          target={'_blank'}
        >
          <Button block size={'small'} type={'primary'}>
            {t('upgradeVersion.action')}
          </Button>
        </Link>
      }
      closable
      message={t('upgradeVersion.newVersion', { version: latestVersion })}
      showIcon={false}
      type={'info'}
    />
  );
});

export default UpgradeAlert;
