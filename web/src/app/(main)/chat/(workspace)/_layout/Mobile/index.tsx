import MobileContentLayout from '@/components/server/MobileNavLayout';

import { LayoutProps } from '../type';
import ChatHeader from './ChatHeader';
import TopicModal from './TopicModal';
import ChatConversation from '../../@conversation/default';
import Topic from '../../@topic/default';

const Layout = ({ children }: LayoutProps) => {
  return (
    <>
      <MobileContentLayout header={<ChatHeader />} style={{ overflowY: 'hidden' }}>
        <ChatConversation />
        {children}
      </MobileContentLayout>
      <TopicModal>
        <Topic />
      </TopicModal>
    </>
  );
};

Layout.displayName = 'MobileConversationLayout';

export default Layout;
