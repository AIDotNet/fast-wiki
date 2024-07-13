
import GlobalProvider from '@/layout/GlobalProvider';
import { Outlet } from 'react-router-dom';

const RootLayout =  () => {
  return (
    <GlobalProvider>
      <Outlet />
    </GlobalProvider>
  );
};

export default RootLayout;
