import { Footer, FooterProps, Header, Layout, Avatar } from '@lobehub/ui';
import { Button, Flex } from 'antd';
import { Publicity } from '../features/Publicity';

export default () => {

  const columns: FooterProps['columns'] = [
    {
      items: [
        {
          description: 'Fast Wiki文档',
          openExternal: true,
          title: 'Fast Wiki文档',
          url: 'https://ai-dotnet.com/',
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
            logo={<Avatar src="/vite.svg" alt="logo"  />}
            nav={
              <Flex wrap="wrap" gap="small" className="site-button-ghost-wrapper">
                <Button type="text" onClick={() => {
                  window.open('mailto:239573049@token-ai.cn');
                }}>
                  商务合作
                </Button>
                <Button type="text" onClick={() => {
                  window.open('https://ai-dotnet.com/')
                }}>
                  文档
                </Button>
              </Flex>
            }
            actions={
              <Button type='text' onClick={()=>{
                window.location.href = '/app';
              }}>
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