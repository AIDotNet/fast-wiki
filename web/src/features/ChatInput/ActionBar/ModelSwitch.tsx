import { ActionIcon } from '@lobehub/ui';
import { AppWindowMac } from 'lucide-react';
import { memo } from 'react';
import { useTranslation } from 'react-i18next';

import ModelSwitchPanel from '@/features/ModelSwitchPanel';

const ModelSwitch = memo(() => {
  const { t } = useTranslation('chat') as any;

  return (
    <ModelSwitchPanel>
      <ActionIcon icon={AppWindowMac} placement={'bottom'} title={t('ModelSwitch.title')} />
    </ModelSwitchPanel>
  );
});

ModelSwitch.displayName = 'ModelSwitch';

export default ModelSwitch;
