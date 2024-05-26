import Desktop from './(desktop)';
import Mobile from './(mobile)';
import ServerLayout from '@/components/server/ServerLayout';

const MainLayout = ServerLayout<any>({ Desktop, Mobile });

export default MainLayout;
