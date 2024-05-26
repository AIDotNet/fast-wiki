/* eslint-disable sort-keys-fix/sort-keys-fix , typescript-sort-keys/interface */
'use client';

import { Icon, MobileTabBar, type MobileTabBarProps } from '@lobehub/ui';
import { createStyles } from 'antd-style';
import { AppWindowMac, MessageSquare, SquareFunction, Album, User } from 'lucide-react';
import { useRouter } from 'next/navigation';
import { rgba } from 'polished';
import { memo, useEffect, useMemo, useState } from 'react';
import { useTranslation } from 'react-i18next';

import { useActiveTabKey } from '@/hooks/useActiveTabKey';
import { SidebarTabKey } from '@/store/global/initialState';
import { GetUser } from '@/services/UserService';

const useStyles = createStyles(({ css, token }) => ({
  active: css`
    svg {
      fill: ${rgba(token.colorPrimary, 0.33)};
    }
  `,
  container: css`
    position: fixed;
    z-index: 100;
    right: 0;
    bottom: 0;
    left: 0;
  `,
}));

const Nav = memo(() => {
  const { t } = useTranslation('common')as any;
  const { styles } = useStyles();
  const activeKey = useActiveTabKey();
  const [items, setItems] = useState([] as any[]);


  const router = useRouter();

  function getUser() {
    GetUser()
      .then((res) => {
        let items = [
          {
            icon: (active: any) => (
              <Icon className={active ? styles.active : undefined} icon={AppWindowMac} />
            ),
            key: SidebarTabKey.App,
            onClick: () => {
              router.push('/app');
            },
            // @ts-ignore
            title: t('tab.app'),
          },
          {
            icon: (active: any) => (
              <Icon className={active ? styles.active : undefined} icon={SquareFunction} />
            ),
            key: SidebarTabKey.FuncationCall,
            onClick: () => {
              router.push('/function-call');
            },
            // @ts-ignore
            title: t('tab.function-call'),
          },
          {
            icon: (active: any) => (
              <Icon className={active ? styles.active : undefined} icon={Album} />
            ),
            key: SidebarTabKey.Wiki,
            onClick: () => {
              router.push('/wiki');
            },
            // @ts-ignore
            title: t('tab.wiki'),
          }
        ];

        if (res.role === 2) {
          items.push(
            {
              icon: (active: any) => <Icon className={active ? styles.active : undefined} icon={User} />,
              key: SidebarTabKey.Me,
              onClick: () => {
                router.push('/me');
              },
              // @ts-ignore
              title: t('tab.me'),
            },
          );
        }

        setItems(items);
      })
  }

  useEffect(() => {
    getUser();
  }, []);


  return <MobileTabBar activeKey={activeKey} className={styles.container} items={items} />;
});

Nav.displayName = 'MobileNav';

export default Nav;
