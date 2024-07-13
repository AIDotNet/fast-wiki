'use client';

import { LogOut, ShieldCheck, UserCircle } from 'lucide-react';
import { useNavigate } from 'react-router-dom';
import { memo, startTransition } from 'react';
import { useTranslation } from 'react-i18next';

import Cell, { CellProps } from '@/components/Cell';
import { useUserStore } from '@/store/user';

const Category = memo(() => {
  const navigate = useNavigate();
  const { t } = useTranslation('auth');
  const signOut = useUserStore((s) => s.logout);
  const items: CellProps[] = [
    {
      icon: UserCircle,
      key: 'profile',
      label: t('profile'),
      onClick: () => startTransition(() => navigate('/profile')),
    },
    {
      icon: ShieldCheck,
      key: 'security',
      label: t('security'),
      onClick: () => startTransition(() => navigate('/security')),
    },
    {
      type: 'divider',
    },
    {
      icon: LogOut,
      key: 'logout',
      label: t('signout', { ns: 'auth' }),
      onClick: () => {
        signOut();
        window.location.href = 'http://api.token-ai.cn/login?redirect_uri=' + window.location.origin+"/auth";
      },
    },
  ];

  return items?.map((item, index) => <Cell key={item.key || index} {...item} />);
});

export default Category;
