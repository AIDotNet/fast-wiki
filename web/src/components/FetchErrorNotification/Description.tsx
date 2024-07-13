import { Icon } from '@lobehub/ui';
import { css, cx } from 'antd-style';
import { ChevronDown, ChevronUp } from 'lucide-react';
import { memo, useState } from 'react';
import { useTranslation } from 'react-i18next';
import { Flexbox } from 'react-layout-kit';
import Highlighter from '@lobehub/ui/es/Highlighter';

const container = css`
  pre.shiki {
    padding: 8px !important;
  }
`;


const Description = memo<{ message: string; status: number }>(({ message, status }) => {
  const { t } = useTranslation('error');
  const [show, setShow] = useState(false);
  return (
    <Flexbox gap={8}>
      {t(`response.${status}` as any)}
      <Flexbox
        gap={4}
        horizontal
        onClick={() => {
          setShow(!show);
        }}
        style={{ cursor: 'pointer', fontSize: 12 }}
      >
        {t('fetchErrorDetail')} <Icon icon={show ? ChevronUp : ChevronDown} />
      </Flexbox>
      <Highlighter
        className={cx(container)}
        language={'text'}
        style={{ display: show ? undefined : 'none', maxHeight: 80 }}
      >
        {message}
      </Highlighter>
    </Flexbox>
  );
});

export default Description;
