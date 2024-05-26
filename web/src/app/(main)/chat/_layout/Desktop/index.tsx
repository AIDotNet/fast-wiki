'use client';

import { Flexbox } from 'react-layout-kit';

import Migration from '../../features/Migration';
import { LayoutProps } from '../type';

const Layout = ({ children, session }: LayoutProps) => {

  if (typeof window === 'undefined') return;
  // 获取当前query中的sharedId
  const query = new URLSearchParams(window.location.search);
  const sharedId = query.get('sharedId');

  return (
    <>
      <Flexbox
        height={'100%'}
        horizontal
        style={{ maxWidth: sharedId ? '100vw' : 'calc(100vw - 64px)', overflow: 'hidden', position: 'relative' }}
        width={'100%'}
      >
        <Flexbox flex={1} style={{ overflow: 'hidden', position: 'relative' }}>
          {children}
        </Flexbox>
      </Flexbox>
      <Migration />
    </>
  );
};

Layout.displayName = 'DesktopChatLayout';

export default Layout;
