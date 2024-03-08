import { Footer, FooterProps, Header, Layout, Logo, LogoProps, useControls, useCreateStore } from '@lobehub/ui';
import { Button, Flex } from 'antd';
import { Publicity } from '../features/Publicity';

export default () => {
  const store = useCreateStore();
  const control: LogoProps | any = useControls(
    {
      size: {
        max: 240,
        min: 16,
        step: 4,
        value: 45,
      },
      type: {
        options: ['3d', 'flat', 'high-contrast', 'text', 'combine'],
        value: '3d',
      },
    },
    { store },
  );

  const columns: FooterProps['columns'] = [
    {
      items: [
        {
          description: 'Fast Wiki文档',
          openExternal: true,
          title: 'Fast Wiki文档',
          url: 'https://docs.token-ai.cn/',
        },
      ],
      title: '文档',
    },
    {
      items: [
        {
          description: '商务合作邮箱',
          openExternal: true,
          title: '商务合作邮箱',
          url: 'mailto:239573049@token-ai.cn',
        },
      ],
      title: '合作',
    },
  ];

  return (
    <div style={{ height: '100vh', overflow: 'auto' }}>
      <Layout
        footer={
          <Footer columns={columns}>

          </Footer>
        }
        header={
          <Header
            logo={<Logo {...control} style={{
              marginLeft: '20px'
            }} />}
            nav={
              <Flex wrap="wrap" gap="small" className="site-button-ghost-wrapper">
                <Button type="text" onClick={() => {
                  window.open('mailto:239573049@token-ai.cn');
                }}>
                  商务合作
                </Button>
                <Button type="text" onClick={() => {
                  window.open('https://docs.token-ai.cn')
                }}>
                  文档
                </Button>
              </Flex>
            }
            actions={
              <Button type='text'>
                立即开始
              </Button>
            }
          />
        }
      >
        <Publicity />
      </Layout>
    </div>
  );
};