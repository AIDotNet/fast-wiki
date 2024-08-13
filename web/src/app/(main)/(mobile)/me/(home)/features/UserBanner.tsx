import { useNavigate } from 'react-router-dom';
import { memo } from 'react';
import { Flexbox } from 'react-layout-kit';

import DataStatistics from '@/features/User/DataStatistics';
import UserInfo from '@/features/User/UserInfo';
import { useUserStore } from '@/store/user';
import { authSelectors } from '@/store/user/selectors';

const UserBanner = memo(() => {
  return (
    <Flexbox gap={12} paddingBlock={8}>
       <UserInfo avatarProps={{
          src: './vite.svg',
       }} />
       <DataStatistics paddingInline={12} />
    </Flexbox>
  );
});

export default UserBanner;
