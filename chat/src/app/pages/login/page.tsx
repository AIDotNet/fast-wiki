import { memo, useState } from 'react';
import { Input, Button, } from 'antd';
import { EyeInvisibleOutlined, EyeTwoTone } from '@ant-design/icons';
import { Logo, LogoProps, useControls, useCreateStore } from '@lobehub/ui';
import styled from 'styled-components';
import { login } from '@/services/AuthorizeService';
import { useNavigate } from 'react-router-dom';

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

const LoginPage = memo(() => {
    const navigate = useNavigate();
    const [loading, setLoading] = useState(false);
    const [account, setAccount] = useState('');
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
            <div style={{
                display: 'flex',
                flexDirection: 'column',
                alignItems: 'center',
                margin: '0 auto',
                width: '100%',
                height: '100%',
                justifyContent: 'center',
                textAlign: 'center',

            }}>
                <div style={{ display: 'flex', flexDirection: 'column', alignItems: 'center', margin: '0 auto', width: '380px', marginBottom: '20px' }}>
                    <div style={{ textAlign: 'center', marginBottom: '20px', marginTop: '50%' }}>
                        <Logo {...control} />
                        <h2>
                            智能笔记系统登录账号
                        </h2>
                    </div>
                    <div style={{ marginBottom: '20px', width: '100%' }}>
                        <Input
                            value={account}
                            onChange={(e) => setAccount(e.target.value)}
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
                <div style={{
                    marginBottom: '20px',
                    display: 'flex',
                    flexDirection: 'column',
                    alignItems: 'center',
                    width: '380px',
                    marginTop: '20px',
                }}>
                    <Button
                        loading={loading}
                        onClick={async () => {
                            try {

                                setLoading(true);
                                const token = await login({
                                    account: account,
                                    password: password,
                                });

                                localStorage.setItem('token', token.token);
                                navigate('/');
                                
                            } catch (e) {

                            }
                            setLoading(false);

                        }}
                        size='large'
                        type="primary"
                        block >
                        登录
                    </Button>
                </div>
                <FunctionTools>
                    <span onClick={() => {
                        navigate('/register');
                    }}>
                        注册账号
                    </span>
                </FunctionTools>
            </div>
        </>
    );
});

export default LoginPage;