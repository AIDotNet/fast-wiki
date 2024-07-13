import { useTheme } from 'antd-style';
import { memo } from 'react';
import { Flexbox } from 'react-layout-kit';

import { usePlatform } from '@/hooks/usePlatform';

import { LayoutProps } from './type';
import Nav from '../@nav/default';

const Layout = memo<LayoutProps>(({ children }) => {
  const { isPWA } = usePlatform();
  const theme = useTheme();

  return (
    <Flexbox
      height={'100%'}
      horizontal
      style={{
        borderTop: isPWA ? `1px solid ${theme.colorBorder}` : undefined,
        position: 'relative',
      }}
      width={'100%'}
    >
      <Nav />
      {children}
    </Flexbox>
  );
});

Layout.displayName = 'DesktopMainLayout';

export default Layout;
