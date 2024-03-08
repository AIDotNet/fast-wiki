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
            logo={
              <Flex wrap="wrap" gap="small" className="site-button-ghost-wrapper">
                <Button type="text" onClick={()=>{
                  window.open('mailto:239573049@token-ai.cn');
                }}>
                  商务合作
                </Button>
              </Flex>}
            nav={<Logo {...control} style={{
              marginLeft: '20px'
            }} />
            }
            actions={
              <Button  onClick={()=>{
                window.location.href = '/app';
            }} type='text'>
                立即开始
              </Button>
            }
          />
        }
      >
        <Publicity/>
      </Layout>
    </div>
  );
};