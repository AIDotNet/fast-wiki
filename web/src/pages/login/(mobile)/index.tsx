import { memo, useState } from "react";
import { Input, Button } from "antd";
import { EyeInvisibleOutlined, EyeTwoTone, GithubOutlined } from "@ant-design/icons";
import styled from "styled-components";
import { Logo, LogoProps, useControls, useCreateStore } from '@lobehub/ui';
import { handleLogin } from "../features/login";

const LoginContainer = styled.div`
  display: flex;
  flex-direction: column;
  align-items: center;
  margin-top: 40%;
  width:100%;
`;

const LogoContainer = styled.div`
  /* 根据你的需求设置Logo的样式 */
`;

const InputContainer = styled.div`
  display: flex;
  flex-direction: column;
  align-items: center;
  width: 100%;
  padding: 0 20px;
  margin-top: 20px;
`;

const StyledInputPassword = styled(Input.Password)`
  width: 100%;
  margin-bottom: 16px; /* Add margin bottom for spacing */
`;

const StyledInput = styled(Input)`
  width: 100%;
  margin-bottom: 16px; /* Add margin bottom for spacing */
`;


const StyledButton = styled(Button)`
  width: 100%;
  margin-bottom: 16px; /* Add margin bottom for spacing */
`;

const GithubLogin = styled.div`
  display: flex;
  align-items: center;
  justify-content: center;
  cursor: pointer;
  transition: all 0.3s;
  &:hover {
    transition: all 0.3s;
    color: #0366d6;
  }
  margin-top: 20px;
`;

const MobileLayout = memo(() => {
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
    <LoginContainer>
      <LogoContainer>
        <Logo {...control} />
      </LogoContainer>
      <InputContainer>
        <StyledInput
          placeholder="请输入账号"
          size="large"
          value={user}
          onChange={(e) => setUser(e.target.value)}
        />
        <StyledInputPassword
          placeholder="请输入密码"
          size="large"
          value={password}
          onChange={(e) => setPassword(e.target.value)}
          iconRender={(visible) =>
            visible ? <EyeTwoTone /> : <EyeInvisibleOutlined />
          }
        />
        <StyledButton
          loading={loading}
          onClick={()=>{
            setLoading(true);
            handleLogin(user, password,()=>{
               window.location.href = '/';
            });
            setLoading(false);
          }}
          size="large" type="primary" block>
          登录
        </StyledButton>
        <GithubLogin>
          <GithubOutlined /> GitHub登录
        </GithubLogin>
      </InputContainer>
    </LoginContainer>
  );
});

export default MobileLayout;