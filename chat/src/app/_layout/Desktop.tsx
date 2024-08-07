
import { memo } from 'react';
import { Flexbox } from 'react-layout-kit';


import { Logo } from '@lobehub/ui';
import { useState } from 'react';
import {
  MenuFoldOutlined,
  MenuUnfoldOutlined,
  SettingOutlined,
  UserOutlined,
  FileTextOutlined
} from '@ant-design/icons';
import { Button, Layout, Menu, theme } from 'antd';

import { useActiveTabKey } from '@/hooks/useActiveTabKey';
import { Outlet, useNavigate } from 'react-router-dom';

const { Header, Sider, Content } = Layout;

const AppLayout = memo(({ }) => {

  const sidebarKey = useActiveTabKey();
  const navigate = useNavigate();

  const [collapsed, setCollapsed] = useState(false);
  const {
    token: { colorBgContainer },
  } = theme.useToken();

  return (
    <Flexbox
      height={'100%'}
      horizontal
      style={{
        position: 'relative',
      }}
      width={'100%'}
    >
      <Layout style={{
        height: '100%',
      }}>
        <Sider trigger={null} collapsible collapsed={collapsed}>
          {
            !collapsed ? (
              <div style={{
                paddingTop: 26,
                display: 'flex',
                justifyContent: 'center',
                alignItems: 'center',
                background: colorBgContainer,
              }}>
                <Logo style={{
                  height: 64,
                  backgroundColor: colorBgContainer,
                }} extra="FastWiki" />
              </div>) : (<span style={{
                backgroundColor: colorBgContainer,
                display: 'flex',
                justifyContent: 'center',
                alignItems: 'center',
                height: 64,
                fontSize: 20,
                fontWeight: 600,
              }}>FastWiki</span>)
          }
          <Menu
            mode='vertical'
            style={{
              height: '100%',
              borderRight: 0,
              background: colorBgContainer,
            }}
            defaultSelectedKeys={[sidebarKey]}
            items={[
              {
                key: 'project',
                icon: <UserOutlined />,
                label: '项目管理',
                onClick: () => {
                  navigate('/app');
                }
              },
              {
                key: 'wiki',
                icon: <FileTextOutlined />,
                label: '知识库管理',
                onClick: () => {
                  navigate('/wiki');
                }
              },
              {
                key: 'user',
                icon: <UserOutlined />,
                label: '用户中心',
                onClick: () => {
                  navigate('/user');
                }
              },
              {
                key: 'setting',
                icon: <SettingOutlined />,
                label: '系统设置',
                onClick: () => {
                  navigate('/setting');
                }
              },
            ]}
          />
        </Sider>
        <Layout>
          <Header style={{ padding: 0, background: colorBgContainer }}>
            <Button
              type="text"
              icon={collapsed ? <MenuUnfoldOutlined /> : <MenuFoldOutlined />}
              onClick={() => setCollapsed(!collapsed)}
              style={{
                fontSize: '16px',
                width: 64,
                height: 64,
              }}
            />
          </Header>
          <Content
            style={{
              margin: '24px 16px',
              padding: 10,
              minHeight: 280,
            }}
          >
            <Outlet />
          </Content>
        </Layout>
      </Layout>
    </Flexbox>
  );
});

AppLayout.displayName = 'DesktopMainLayout';

export default AppLayout;
