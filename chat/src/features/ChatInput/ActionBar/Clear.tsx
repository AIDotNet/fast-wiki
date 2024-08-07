import { ActionIcon } from '@lobehub/ui';
import { Popconfirm } from 'antd';
import { Eraser } from 'lucide-react';
import { memo, useCallback, useState } from 'react';


import HotKeys from '@/components/HotKeys';
import { ALT_KEY, CLEAN_MESSAGE_KEY, META_KEY } from '@/const/hotkeys';
import { useChatStore } from '@/store/chat';

const Clear = memo(() => {
  const {
    clearMessage
  } = useChatStore();
  const hotkeys = [META_KEY, ALT_KEY, CLEAN_MESSAGE_KEY].join('+');
  const [confirmOpened, updateConfirmOpened] = useState(false);

  const resetConversation = useCallback(async () => {
    await clearMessage();
  }, []);

  const actionTitle: any = confirmOpened ? (
    void 0
  ) : (
    <HotKeys desc={'清空当前会话消息'} inverseTheme keys={hotkeys} />
  );

  return (
    <Popconfirm
      arrow={false}
      okButtonProps={{ danger: true, type: 'primary' }}
      onConfirm={resetConversation}
      onOpenChange={updateConfirmOpened}
      open={confirmOpened}
      placement={'topRight'}
      title={'即将清空当前会话消息，清空后将无法找回，请确认你的操作'}
    >
      <ActionIcon 
        icon={Eraser} 
        overlayStyle={{ maxWidth: 'none' }}
        placement={'bottom'} 
        title={actionTitle} />
    </Popconfirm>
  );
});

export default Clear;
