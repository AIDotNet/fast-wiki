import ServerLayout from '@/components/server/ServerLayout';

import Desktop from './_layout/Desktop';
import { LayoutProps } from './_layout/type';

const MainLayout = ServerLayout<LayoutProps>({ Desktop, Mobile: Desktop });

MainLayout.displayName = 'MainLayout';

export default MainLayout;
