import { Icon } from '@lobehub/ui';
import { Tag } from 'antd';
import { Bot, Brain, Cloudy, Info, Mic2, Settings2, Sparkles } from 'lucide-react';
import { startTransition, useMemo } from 'react';
import { useTranslation } from 'react-i18next';
import { Flexbox } from 'react-layout-kit';

import type { MenuProps } from '@/components/Menu';
import { SettingsTabs } from '@/store/global/initialState';
import { featureFlagsSelectors, useServerConfigStore } from '@/store/serverConfig';
import { Link, useNavigate } from 'react-router-dom';

export const useCategory = () => {
  const { t } = useTranslation('setting');
  const { enableWebrtc, showLLM } = useServerConfigStore(featureFlagsSelectors);
  const navigate = useNavigate();

  const cateItems: MenuProps['items'] = useMemo(
    () =>
      [
        {
          icon: <Icon icon={Settings2} />,
          key: SettingsTabs.Common,
          label: (
            <span onClick={() => startTransition(() => navigate('/settings/common'))}>
              {t('tab.common')}
            </span>
          ),
        },
        {
          icon: <Icon icon={Sparkles} />,
          key: SettingsTabs.SystemAgent,
          label: (
            <span onClick={() => startTransition(() => navigate('/settings/system-agent'))}>
              {t('tab.system-agent')}
            </span>
          ),
        },
        enableWebrtc && {
          icon: <Icon icon={Cloudy} />,
          key: SettingsTabs.Sync,
          label: (
            <span onClick={() => startTransition(() => navigate('/settings/sync'))}>
              <Flexbox align={'center'} gap={8} horizontal>
                {t('tab.sync')}
                <Tag bordered={false} color={'warning'}>
                  {t('tab.experiment')}
                </Tag>
              </Flexbox>
            </span>
          ),
        },
        showLLM && {
          icon: <Icon icon={Brain} />,
          key: SettingsTabs.LLM,
          label: (
            <span onClick={() => startTransition(() => navigate('/settings/llm'))}>
              {t('tab.llm')}
            </span>
          ),
        },

        {
          icon: <Icon icon={Mic2} />,
          key: SettingsTabs.TTS,
          label: (
            <span onClick={() => startTransition(() => navigate('/settings/tts'))}>
              {t('tab.tts')}
            </span>
          ),
        },
        {
          icon: <Icon icon={Bot} />,
          key: SettingsTabs.Agent,
          label: (
            <span onClick={() => startTransition(() => navigate('/settings/agent'))}>
              {t('tab.agent')}
            </span>
          ),
        },
        {
          icon: <Icon icon={Info} />,
          key: SettingsTabs.About,
          label: (
            <span onClick={() => startTransition(() => navigate('/settings/about'))}>
              {t('tab.about')}
            </span>
          ),
        },
      ].filter(Boolean) as MenuProps['items'],
    [t, enableWebrtc, showLLM],
  );

  return cateItems;
};
