import { Icon } from '@lobehub/ui';
import { Bot, Info, Settings2 } from 'lucide-react';
import { useMemo } from 'react';
import { useTranslation } from 'react-i18next';

import type { MenuProps } from '@/components/Menu';
import { SettingsTabs } from '@/store/global/initialState';
import { featureFlagsSelectors, useServerConfigStore } from '@/store/serverConfig';

interface UseCategoryOptions {
  mobile?: boolean;
}

export const useCategory = ({ mobile }: UseCategoryOptions = {}) => {
  const { t } = useTranslation('setting') as any;
  const { enableWebrtc, showLLM } = useServerConfigStore(featureFlagsSelectors);

  const iconSize = mobile ? { fontSize: 20 } : undefined;

  const cateItems: MenuProps['items'] = useMemo(
    () =>
      [
        {
          icon: <Icon icon={Settings2} size={iconSize} />,
          key: SettingsTabs.Common,
          label: t('tab.common'),
        },
        {
          icon: <Icon icon={Info} size={iconSize} />,
          key: SettingsTabs.About,
          label: t('tab.about'),
        },
      ].filter(Boolean) as MenuProps['items'],
    [t, enableWebrtc, showLLM],
  );

  return cateItems;
};
