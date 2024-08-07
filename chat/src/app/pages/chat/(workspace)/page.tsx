import ServerLayout from '@/components/server/ServerLayout';

import Desktop from './_layout/Desktop';
import Mobile from './_layout/Mobile';

const Layout = ServerLayout({ Desktop, Mobile });

Layout.displayName = 'ChatConversationLayout';

export default Layout;
