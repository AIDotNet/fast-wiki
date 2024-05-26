'use client';
import { memo, useState } from 'react';
import { Input, Button } from 'antd';
import { EyeInvisibleOutlined, EyeTwoTone } from '@ant-design/icons';
import { Logo, LogoProps, useControls, useCreateStore } from '@lobehub/ui';
import styled from 'styled-components';
import { handleRegister } from './features/register';

const FunctionTools = styled.div`
    display: flex;
    justify-content: space-between;
    align-items: center;
    margin-bottom: 20px;
    cursor: pointer;
    justify-content: center;
    align-items: center;
    text-align: center;
    margin: 0 auto;
    width: 380px;
    margin-top: 20px;
    color: #0366d6;
`;

const DesktopLayout = memo(() => {
    const [loading, setLoading] = useState(false);
    const [account, setAccount] = useState('');
    const [password, setPassword] = useState('');
    const [name, setName] = useState('');
    const [email, setEmail] = useState('');
    const [phone, setPhone] = useState('');
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
                <div style={{ textAlign: 'center', marginBottom: '20px', marginTop: '20%' }}>
                    <Logo {...control} />
                    <h2>
                        注册账号
                    </h2>
                </div>
                <div style={{ marginBottom: '20px', width: '100%' }}>
                    <Input
                        value={account}
                        onChange={(e) => setAccount(e.target.value)}
                        size='large'
                        placeholder="请输入账号" />
                </div>
                <div style={{ marginBottom: '20px', width: '100%' }}>
                    <Input
                        value={name}
                        onChange={(e) => setName(e.target.value)}
                        size='large'
                        placeholder="请输入昵称" />
                </div>
                <div style={{ marginBottom: '20px', width: '100%' }}>
                    <Input
                        value={email}
                        onChange={(e) => setEmail(e.target.value)}
                        size='large'
                        placeholder="请输入邮箱" />
                </div>
                <div style={{ marginBottom: '20px', width: '100%' }}>
                    <Input
                        value={phone}
                        onChange={(e) => setPhone(e.target.value)}
                        size='large'
                        placeholder="请输入手机号" />
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
                        handleRegister(account, password, name, email, phone,()=>{
                            window.location.href = '/auth-login';
                        });
                        setLoading(false);

                    }}
                    size='large'
                    type="primary"
                    block >
                    登录
                </Button>
            </div>
            <FunctionTools>
                <span onClick={()=>{
                    window.location.href = '/auth-login';
                }}>
                    已经有账号？去登录
                </span>
            </FunctionTools>
        </>
    );
});

export default DesktopLayout;