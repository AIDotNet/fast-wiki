import { useTheme } from 'antd-style';
import { memo, useEffect, useState, } from 'react';
import { Flexbox } from 'react-layout-kit';

import { LayoutProps } from './type';
import { Layout, Menu, theme } from 'antd';
import { useNavigate } from 'react-router-dom';
import { Logo } from '@lobehub/ui';
import { useActiveTabKey } from '@/hooks/useActiveTabKey';
import {
  FunctionOutlined,
  UserOutlined,
  SettingOutlined,
  ProjectOutlined,
  FileTextOutlined
} from '@ant-design/icons';

const { Sider, Content } = Layout;

const DesktopLayout = memo<LayoutProps>(({ children }) => {
  const navigate = useNavigate();
  const sidebarKey = useActiveTabKey();
  // 解析query
  const query = new URLSearchParams(window.location.search);
  const sharedId = query.get('sharedId');

  const {
    token: { colorBgContainer },
  } = theme.useToken();

  // 如果是分享页面，不显示侧边栏
  if (sharedId) {
    return children;
  }

  const [items, setItems] = useState<any[]>([]);

  useEffect(() => {

    const jwt = localStorage.getItem('token');
    const jwtObj = jwt ? JSON.parse(atob(jwt.split('.')[1])) : {};
    const role = jwtObj.role;
    console.log('role', role);
    
    if (role === 'Admin') {
      setItems([
        {
          key: 'project',
          icon: <ProjectOutlined />,
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
          key: 'function-call',
          icon: <FunctionOutlined />,
          label: '函数管理',
          onClick: () => {
            navigate('/function-call');
          }
        },
        {
          key: 'system-manager',
          icon: <SettingOutlined />,
          label: '系统管理',
          children: [
            {
              key: 'user',
              icon: <UserOutlined />,
              label: '用户中心',
              onClick: () => {
                navigate('/user');
              }
            }
          ]
        },
        {
          key: 'me',
          icon: <UserOutlined />,
          label: '个人中心',
          onClick: () => {
            navigate('/me');
          }
        }
      ])
    } else {
      setItems([

        {
          key: 'project',
          icon: <ProjectOutlined />,
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
          key: 'function-call',
          icon: <FunctionOutlined />,
          label: '函数管理',
          onClick: () => {
            navigate('/function-call');
          }
        },
        {
          key: 'me',
          icon: <UserOutlined />,
          label: '个人中心',
          onClick: () => {
            navigate('/me');
          }
        }
      ]);
    }
  }, []);


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

        <Sider trigger={null} collapsible >
          {<span style={{
            backgroundColor: colorBgContainer,
            display: 'flex',
            justifyContent: 'center',
            alignItems: 'center',
            height: 64,
            fontSize: 20,
            fontWeight: 600,
          }}>FastWiki</span>
          }
          <Menu
            mode='inline'
            style={{
              height: '100%',
              borderRight: 0,
              background: colorBgContainer,
            }}
            defaultSelectedKeys={[sidebarKey]}
            items={items}
          />
        </Sider>
        <Layout>
          <Content
            style={{
              minHeight: 280,
            }}
          >
            {children}
          </Content>
        </Layout>
      </Layout>
    </Flexbox>
  );
});

DesktopLayout.displayName = 'DesktopMainLayout';

export default DesktopLayout;
