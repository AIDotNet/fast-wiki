/* eslint-disable sort-keys-fix/sort-keys-fix , typescript-sort-keys/interface */
import { ActionIcon } from '@lobehub/ui';
import { AppWindowMac, SquareFunction, Album, User } from 'lucide-react';
import Link from 'next/link';
import { memo, useEffect, useState } from 'react';
import { useTranslation } from 'react-i18next';

import { useGlobalStore } from '@/store/global';
import { SidebarTabKey } from '@/store/global/initialState';
import { GetUser } from '@/services/UserService';

export interface TopActionProps {
  tab?: SidebarTabKey;
}

const TopActions = memo<TopActionProps>(({ tab }) => {
  const { t } = useTranslation('common')as any;
  const [items, setItems] = useState([] as any[]);
  const switchToApp = useGlobalStore((s) => s.switchToApp);
  const switchToFunctionCall = useGlobalStore((s) => s.switchToFunctionCall);
  const switchToWiki = useGlobalStore((s) => s.switchToWiki);
  const switchToUser = useGlobalStore((s) => s.switchToUser);


  function getUser() {
    GetUser()
      .then((res) => {
        let items = [];
        items.push({
          href: '/app',
          icon: AppWindowMac,      
          // @ts-ignore
          title: t('tab.app'),
          key: SidebarTabKey.App,
          onClick: () => {
            switchToApp();
          }
        });
        items.push({
          href: '/function-call',
          icon: SquareFunction,
          // @ts-ignore
          title: t('tab.function-call'),
          key: SidebarTabKey.FuncationCall,
          onClick: () => {
            switchToFunctionCall();
          }
        });

        items.push({
          href: '/wiki',
          icon: Album,
          // @ts-ignore
          title: t('tab.wiki'),
          key: SidebarTabKey.Wiki,
          onClick: () => {
            switchToWiki();
          }
        });

        // 管理员
        if (res.role === 2) {
          items.push({
            href: '/user',
            icon: User,
            // @ts-ignore
            title: t('tab.user'),
            key: SidebarTabKey.User,
            onClick: () => {
              switchToUser();
            }
          });
        }

        setItems(items);
      })
  }

  useEffect(() => {
    getUser();
  }, []);


  return (
    <>
      {items.map((item) => {
        return (
          <Link
            aria-label={item.title}
            href={item.href}
            onClick={(e) => {
              e.preventDefault();
              item.onClick();
            }}
          >
            <ActionIcon
              active={tab === item?.key}
              icon={item.icon}
              placement={'right'}
              size="large"
              title={item.title}
            />
          </Link>)
      })}
    </>
  );
});

export default TopActions;
