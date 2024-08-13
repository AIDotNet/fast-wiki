import { Button } from 'antd';
import { memo, useMemo } from 'react';
import { useTranslation } from 'react-i18next';
import { Center, Flexbox } from 'react-layout-kit';

import { useChatStore } from '@/store/chat';

interface APIKeyFormProps {
  id: string;
  provider?: string;
}

const APIKeyForm = memo<APIKeyFormProps>(({ id, provider }) => {
  const { t } = useTranslation('error');

  const [resend, deleteMessage] = useChatStore((s) => [s.regenerateMessage, s.deleteMessage]);

  return (
    <Center gap={16} style={{ maxWidth: 300 }}>
      <Flexbox gap={12} width={'100%'}>
        <Button
          block
          onClick={() => {
            resend(id);
            deleteMessage(id);
          }}
          style={{ marginTop: 8 }}
          type={'primary'}
        >
          {t('unlock.confirm')}
        </Button>
        <Button
          onClick={() => {
            deleteMessage(id);
          }}
        >
          {t('unlock.closeMessage')}
        </Button>
      </Flexbox>
    </Center>
  );
});

export default APIKeyForm;
