import { Book, CircleUserRound, Database, Download, GithubIcon, Settings2 } from 'lucide-react';
import { useTranslation } from 'react-i18next';

import { CellProps } from '@/components/Cell';
import { enableAuth } from '@/const/auth';
import { DISCORD, DOCUMENTS, FEEDBACK, GITHUB } from '@/const/url';
import { usePWAInstall } from '@/hooks/usePWAInstall';
import { useUserStore } from '@/store/user';
import { authSelectors } from '@/store/user/slices/auth/selectors';
import { useCategory as useSettingsCategory } from '../../settings/features/useCategory';
import { useNavigate } from 'react-router-dom';
import { startTransition } from 'react';
export const useCategory = () => {
  const navigate = useNavigate();
  const { canInstall, install } = usePWAInstall();
  const { t } = useTranslation(['common', 'setting', 'auth']);
  const [isLogin, isLoginWithAuth, isLoginWithClerk] = useUserStore((s) => [
    authSelectors.isLogin(s),
    authSelectors.isLoginWithAuth(s),
    authSelectors.isLoginWithClerk(s),
  ]);

  const profile: CellProps[] = [
    {
      icon: CircleUserRound,
      key: 'profile',
      label: t('userPanel.profile'),
      onClick: () => {
        startTransition(() => { navigate('/me/profile') });
      },
    },
  ];


  const helps: CellProps[] = [
    {
      icon: Book,
      key: 'docs',
      label: t('document'),
      onClick: () => window.open(DOCUMENTS, '__blank'),
    },
    {
      icon: GithubIcon,
      key: 'github',
      label: 'Github',
      onClick: () => window.open(GITHUB, '__blank'),
    },
  ];

  const mainItems = [
    {
      type: 'divider',
    },
    ...(isLoginWithClerk ? profile : []),
    ...helps,
  ].filter(Boolean) as CellProps[];

  return mainItems;
};
