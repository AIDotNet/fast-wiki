import {  Checkbox } from 'antd';
import { memo } from 'react';
import { useTranslation } from 'react-i18next';
import { Flexbox } from 'react-layout-kit';

import { useUserStore } from '@/store/user';
import { settingsSelectors } from '@/store/user/selectors';
import { DallEImageItem } from '@/types/tool/dalle';

interface ToolBarProps {
  content: DallEImageItem[];
  messageId: string;
}

const ToolBar = memo<ToolBarProps>(({ }) => {
  const { t } = useTranslation('tool');
  const [isAutoGenerate, setSettings] = useUserStore((s) => [
    settingsSelectors.isDalleAutoGenerating(s),
    s.setSettings,
  ]);

  return (
    <Flexbox align={'center'} height={28} horizontal justify={'space-between'}>
      {t('dalle.images')}
      <Flexbox align={'center'} gap={8} horizontal>
        <Checkbox
          checked={isAutoGenerate}
          onChange={(e) => {
            setSettings({ tool: { dalle: { autoGenerate: e.target.checked } } });
          }}
        >
          {t('dalle.autoGenerate')}
        </Checkbox>
      </Flexbox>
    </Flexbox>
  );
});

export default ToolBar;
