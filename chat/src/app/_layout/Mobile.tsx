

import { memo } from 'react';

import { Outlet } from 'react-router-dom';

const Layout = memo(() => {

  return (
    <>
      <Outlet />
    </>
  );
});

Layout.displayName = 'MobileMainLayout';

export default Layout;