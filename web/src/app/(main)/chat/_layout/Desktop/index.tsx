import { Flexbox } from 'react-layout-kit';

import Migration from '../../features/Migration';
import WorkspaceLayout from '../../(workspace)/layout';
import WorkspacePage from '../../(workspace)/page';
import { useEffect, useState } from 'react';
import { Modal } from '@lobehub/ui';
import { Outlet, useLocation } from 'react-router-dom';

const Layout = () => {
  const [workSpacePage, setWorkSpacePage] = useState<any>();
  const location = useLocation();
  const [showSettingModal, setShowSettingModal] = useState(false);

  const [screenWidth, setScreenWidth] = useState(window.innerWidth);
  const [screenHeight, setScreenHeight] = useState(window.innerHeight);

  const updateScreenSize = () => {
    setScreenWidth(window.innerWidth);
    setScreenHeight(window.innerHeight);
  };

  useEffect(() => {
    // 获取路由token
    const query = new URLSearchParams(window.location.search);
    const token = query.get('token');
    if (token) {
      localStorage.setItem('token', token);
    }

    WorkspacePage()
      .then((page) => {
        setWorkSpacePage(page);
      })

    window.addEventListener('resize', updateScreenSize);
    return () => window.removeEventListener('resize', updateScreenSize);
  }, []);

  useEffect(() => {
    if (location.pathname.includes('/chat/settings')) {
      setShowSettingModal(true);
    }else{
      setShowSettingModal(false);
    }
  }, [location]);

  return (
    <>
      <Flexbox
        height={'100%'}
        horizontal
        style={{ maxWidth: 'calc(100vw - 64px)', overflow: 'hidden', position: 'relative' }}
        width={'100%'}
      >
        <Flexbox flex={1} style={{ overflow: 'hidden', position: 'relative' }}>
          <WorkspaceLayout>
            {workSpacePage}
          </WorkspaceLayout>
        </Flexbox>
      </Flexbox>
      <Modal
        width={screenWidth-200}
        onCancel={() => {
          setShowSettingModal(false);
        }}
        onClose={() => {
          setShowSettingModal(false);
        }}
        footer={[]}
        open={showSettingModal}>
        <Outlet />
      </Modal>
      <Migration />
    </>
  );
};

Layout.displayName = 'DesktopChatLayout';

export default Layout;
