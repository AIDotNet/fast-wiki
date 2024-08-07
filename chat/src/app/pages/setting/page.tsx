import React from 'react';
import { Avatar, Typography, Button } from 'antd';
import { GithubOutlined } from '@ant-design/icons';

const { Title, Paragraph } = Typography;

const SettingPage: React.FC = () => {
    return (
        <div style={{ display: 'flex', justifyContent: 'center', alignItems: 'center', height: '100vh', width: '100%' }}>
            <div style={{ width: 450, textAlign: 'center', }}>
                <Avatar size={64} icon={<GithubOutlined />} />
                <Title level={2}>关于FastWiki</Title>
                <Paragraph>
                    FastWiki是一个免费开源的知识管理系统，基于React、Ant Design、TypeScript开发，我们也提供了企业级服务，欢迎使用！
                </Paragraph>
                <Paragraph>
                    GitHub: <a href="https://github.com/AIDotNet/fast-wiki" target="_blank" rel="noopener noreferrer">https://github.com/AIDotNet/fast-wiki</a>
                </Paragraph>
                <Button onClick={()=>{
                    window.open('https://github.com/AIDotNet/fast-wiki')
                }}>
                    给我们一个Star
                </Button>
            </div>
        </div>
    );
};

export default SettingPage;