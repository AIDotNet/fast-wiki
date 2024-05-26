'use client';

import { Avatar, type AvatarProps } from '@lobehub/ui';
import { createStyles } from 'antd-style';
import { memo, useEffect, useState } from 'react';

import { DEFAULT_USER_AVATAR_URL } from '@/const/meta';
import { useUserStore } from '@/store/user';
import { authSelectors, userProfileSelectors } from '@/store/user/selectors';
import { GetUser } from '@/services/UserService';

const useStyles = createStyles(({ css, token }) => ({
  clickable: css`
    position: relative;
    transition: all 200ms ease-out 0s;

    &::before {
      content: '';

      position: absolute;
      transform: skewX(-45deg) translateX(-400%);

      overflow: hidden;

      box-sizing: border-box;
      width: 25%;
      height: 100%;

      background: rgba(255, 255, 255, 50%);

      transition: all 200ms ease-out 0s;
    }

    &:hover {
      box-shadow: 0 0 0 2px ${token.colorPrimary};

      &::before {
        transform: skewX(-45deg) translateX(400%);
      }
    }
  `,
}));

export interface UserAvatarProps extends AvatarProps {
  clickable?: boolean;
}

const UserAvatar = memo<UserAvatarProps>(
  ({ size = 40, avatar, background, clickable, className, style, ...rest }) => {
    const { styles, cx } = useStyles();
    const [username] = useUserStore((s) => [
      userProfileSelectors.userAvatar(s),
      userProfileSelectors.username(s),
    ]);

    const isSignedIn = useUserStore(authSelectors.isLogin);
    function getAvatar() {
      if (avatar) {
        return avatar;
      }

      // 从缓存中获取用户头像
      var user = JSON.parse(localStorage.getItem('user') as string);
      if (user && user.avatar) {
        return user.avatar;
      }

    }

    return (
      <Avatar
        alt={isSignedIn ? (username as string) : 'FastWki-Chat'}
        avatar={getAvatar()}
        background={isSignedIn && avatar ? background : undefined}
        className={cx(clickable && styles.clickable, className)}
        size={size}
        style={{ flex: 'none', ...style }}
        unoptimized
        {...rest}
      />
    );
  },
);

UserAvatar.displayName = 'UserAvatar';

export default UserAvatar;
