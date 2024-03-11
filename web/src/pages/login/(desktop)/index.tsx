
import { memo, useState } from 'react';
import { Input, Button } from 'antd';
import { EyeInvisibleOutlined, EyeTwoTone, GithubOutlined } from '@ant-design/icons';
import { Footer, Logo, LogoProps, useControls, useCreateStore } from '@lobehub/ui';
import styled from 'styled-components';
import { handleLogin } from '../features/login';

const GithubButton = styled.span`
    transition: all 0.3s;
    display: inline-flex;
    align-items: center;
    cursor: pointer;
    &:hover {
        transition: all 0.3s;
        color: #0366d6;
        cursor: pointer;
    }
`;

const Login = memo(() => {
    const [loading, setLoading] = useState(false);
    const [user, setUser] = useState('');
    const [password, setPassword] = useState('');
    const store = useCreateStore();
    const control: LogoProps | any = useControls(
        {
            size: {
                max: 240,
                min: 16,
                step: 4,
                value: 64,
            },
            type: {
                options: ['3d', 'flat', 'high-contrast', 'text', 'combine'],
                value: '3d',
            },
        },
        { store },
    );

    return (
        <>
            <div style={{ display: 'flex', flexDirection: 'column', alignItems: 'center', margin: '0 auto', width: '380px', marginBottom: '20px' }}>
                <div style={{ textAlign: 'center', marginBottom: '20px', marginTop: '50%' }}>
                    <Logo {...control} />
                </div>
                <div style={{ marginBottom: '20px', width: '100%' }}>
                    <Input
                        value={user}
                        onChange={(e) => setUser(e.target.value)}
                        size='large'
                        placeholder="请输入账号" />
                </div>
                <div style={{ width: '100%' }}></div>
                <Input.Password
                    value={password}
                    onChange={(e) => setPassword(e.target.value)}
                    size='large'
                    placeholder="请输入密码"
                    iconRender={visible => (visible ? <EyeTwoTone /> : <EyeInvisibleOutlined />)}
                />
            </div>
            <div style={{ marginBottom: '20px', marginTop: '20px', display: 'flex', flexDirection: 'column', alignItems: 'center', margin: '0 auto', width: '380px' }}>
                <Button
                    loading={loading}
                    onClick={() => {
                        setLoading(true);
                        handleLogin(user, password,()=>{
                           window.location.href = '/';
                        });
                        setLoading(false);
                    }}
                    size='large'
                    type="primary"
                    block >
                    登录
                </Button>
            </div>
            <div style={{ textAlign: 'center', display: 'flex', flexDirection: 'column', alignItems: 'center', margin: '0 auto', width: '380px', paddingBottom: '8px', paddingTop: '20px' }}>
                <GithubButton rel="noopener noreferrer">
                    <GithubOutlined /> GitHub登录
                </GithubButton>
            </div>
            <Footer bottom="TokenAI © 2024" columns={[]} />
        </>
    );
});

export default Login;