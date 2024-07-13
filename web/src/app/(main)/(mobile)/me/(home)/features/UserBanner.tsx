import { useNavigate } from 'react-router-dom';
import { memo } from 'react';
import { Flexbox } from 'react-layout-kit';

import { enableAuth } from '@/const/auth';
import DataStatistics from '@/features/User/DataStatistics';
import UserInfo from '@/features/User/UserInfo';
import UserLoginOrSignup from '@/features/User/UserLoginOrSignup';
import { useUserStore } from '@/store/user';
import { authSelectors } from '@/store/user/selectors';

const UserBanner = memo(() => {
  const navigate = useNavigate();
  const isLoginWithAuth = useUserStore(authSelectors.isLoginWithAuth);

  return (
    <Flexbox gap={12} paddingBlock={8}>
      {!enableAuth ? (
        <>
          <UserInfo />
          <DataStatistics paddingInline={12} />
        </>
      ) : isLoginWithAuth ? (
        <>
          <UserInfo onClick={() => navigate('/me/profile')} />
          <DataStatistics paddingInline={12} />
        </>
      ) : (
        <UserLoginOrSignup onClick={() => window.location.href = 'http://api.token-ai.cn/login?redirect_uri=' + window.location.origin + "/auth"} />
      )}
    </Flexbox>
  );
});

export default UserBanner;
