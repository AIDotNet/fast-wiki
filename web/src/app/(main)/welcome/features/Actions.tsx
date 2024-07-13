
import { Icon } from '@lobehub/ui';
import { Button } from 'antd';
import { SendHorizonal } from 'lucide-react';
import { useNavigate } from 'react-router-dom';
import { memo, startTransition } from 'react';
import { useTranslation } from 'react-i18next';
import { Flexbox } from 'react-layout-kit';
import { Link } from 'react-router-dom';

const Actions = memo<{ mobile?: boolean }>(({ mobile }) => {
  const { t } = useTranslation('welcome');
  const navigate = useNavigate();

  return (
    <Flexbox gap={16} horizontal={!mobile} justify={'center'} width={'100%'} wrap={'wrap'}>
      <Button
        onClick={()=>{
          startTransition(() => navigate('/market'));
        }}
        block={mobile} size={'large'} style={{ minWidth: 160 }} type={'default'}>
        {t('button.market')}
      </Button>
      <Button
        block={mobile}
        onClick={() => {
          startTransition(() => navigate('/chat'));
        }}
        size={'large'}
        style={{ minWidth: 160 }}
        type={'primary'}
      >
        <Flexbox align={'center'} gap={4} horizontal justify={'center'}>
          {t('button.start')}
          <Icon icon={SendHorizonal} />
        </Flexbox>
      </Button>
    </Flexbox>
  );
});

export default Actions;
