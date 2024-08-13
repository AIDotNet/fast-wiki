import { Tooltip } from '@lobehub/ui';
import { Tag } from 'antd';
import { useTheme } from 'antd-style';
import { CSSProperties, memo, useMemo } from 'react';
import { useTranslation } from 'react-i18next';

export enum PlanType {
  Preview = 'preview',
}

export interface PlanTagProps {
  type?: PlanType;
}

const PlanTag = memo<PlanTagProps>(({ type = PlanType.Preview }) => {
  const { t } = useTranslation('common');
  const theme = useTheme();
  const tag: {
    desc: string;
    style: CSSProperties;
    title: string;
  } = useMemo(() => {
    switch (type) {
      case PlanType.Preview: {
        return {
          desc: '开源版，指在Github上开源的版本，完全开源免费使用。',
          style: {
            background: theme.colorFill,
          },
          title: '开源版',
        };
      }
    }
  }, []);

  return (
    <Tooltip title={tag.desc}>
      <Tag bordered={false} style={{ ...tag.style, borderRadius: 12, cursor: 'pointer' }}>
        {tag.title}
      </Tag>
    </Tooltip>
  );
});

export default PlanTag;
