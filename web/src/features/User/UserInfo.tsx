'use client';

import { createStyles } from 'antd-style';
import { memo, useState, useEffect } from 'react';
import { Flexbox, FlexboxProps } from 'react-layout-kit';

import PlanTag from '@/features/User/PlanTag';
import { useUserStore } from '@/store/user';
import { userProfileSelectors } from '@/store/user/selectors';

import UserAvatar, { type UserAvatarProps } from './UserAvatar';
import { GetUser } from '@/services/UserService';
import { Tag } from '@lobehub/ui';

const useStyles = createStyles(({ css, token }) => ({
  nickname: css`
    font-size: 16px;
    font-weight: bold;
    line-height: 1;
  `,
  username: css`
    line-height: 1;
    color: ${token.colorTextDescription};
  `,
}));

export interface UserInfoProps extends FlexboxProps {
  avatarProps?: Partial<UserAvatarProps>;
}

const UserInfo = memo<UserInfoProps>(({ avatarProps, ...rest }: any) => {
  const { styles, theme } = useStyles();
  const [user, setUser] = useState({ avatar: '' } as any);

  const [username] = useUserStore((s) => [
    userProfileSelectors.nickName(s),
    userProfileSelectors.username(s),
  ]);


  function getInfo() {
    GetUser().then((res) => {
      localStorage.setItem('user', JSON.stringify(res));
      setUser(res);
    });
  }

  useEffect(() => {
    getInfo();
  }, []);


  return (
    <Flexbox
      align={'center'}
      gap={12}
      horizontal
      justify={'space-between'}
      paddingBlock={12}
      paddingInline={12}
      {...rest}
    >
      <Flexbox align={'center'} gap={12} horizontal>
        <UserAvatar avatar={user.avatar} background={theme.colorFill} size={48} {...avatarProps} />
        <Flexbox flex={1} gap={6}>
          <div className={styles.nickname}>{user.name}</div>
          <div className={styles.username}>
            <Tag>
              {user.roleName}
            </Tag>
          </div>
        </Flexbox>
      </Flexbox>
      <PlanTag />
    </Flexbox>
  );
});

export default UserInfo;
