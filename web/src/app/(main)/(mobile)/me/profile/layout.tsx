import { useNavigate } from 'react-router-dom';
import { PropsWithChildren, startTransition } from 'react';

import MobileContentLayout from '@/components/server/MobileNavLayout';
import { enableClerk } from '@/const/auth';

import Header from './features/Header';

const Layout = ({ children }: PropsWithChildren) => {
  if (!enableClerk) {
    const navigate = useNavigate();
    startTransition(() => {
      navigate('/me')
    });
    return null;
  }
  return <MobileContentLayout header={<Header />}>{children}</MobileContentLayout>;
};

Layout.displayName = 'MeProfileLayout';

export default Layout;
