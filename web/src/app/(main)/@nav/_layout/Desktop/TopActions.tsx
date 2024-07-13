import { ActionIcon } from '@lobehub/ui';
import { AppWindowMac, SquareFunction, Album, User } from 'lucide-react';
import { useNavigate, Link } from 'react-router-dom';
import { memo, startTransition, useEffect, useState } from 'react';
import { useTranslation } from 'react-i18next';

import { useGlobalStore } from '@/store/global';
import { SidebarTabKey } from '@/store/global/initialState';
import { useSessionStore } from '@/store/session';
import { GetUser } from '@/services/UserService';

export interface TopActionProps {
  tab?: SidebarTabKey;
}

const TopActions = memo<TopActionProps>(({ tab }) => {
  const { t } = useTranslation('common');
  const navigate = useNavigate();
  const switchBackToChat = useGlobalStore((s) => s.switchBackToChat);
  const [items, setItems] = useState<any[]>();

  function getUser() {
    GetUser()
      .then((res) => {
        let items = [];
        items.push({
          href: '/app',
          icon: AppWindowMac,
          // @ts-ignore
          title: t('tab.app') ,
          key: SidebarTabKey.App
        } );
        items.push({
          href: '/function-call',
          icon: SquareFunction,
          // @ts-ignore
          title: t('tab.function-call'),
          key: SidebarTabKey.FuncationCall
        });

        items.push({
          href: '/wiki',
          icon: Album,
          // @ts-ignore
          title: t('tab.wiki'),
          key: SidebarTabKey.Wiki
        });

        // 管理员
        if (res.role === 2) {
          items.push({
            href: '/user',
            icon: User,
            // @ts-ignore
            title: t('tab.user'),
            key: SidebarTabKey.User
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
      {items?.map((item) => {
        return (
          <Link
            aria-label={item.title}
            to={item.href}
            onClick={(e) => {
              navigate(item.href);
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
