import { Icon, MobileTabBar, type MobileTabBarProps } from '@lobehub/ui';
import { createStyles } from 'antd-style';
import { Bot, MessageSquare, User } from 'lucide-react';
import { useNavigate } from 'react-router-dom';
import { rgba } from 'polished';
import { memo, useMemo } from 'react';
import { useTranslation } from 'react-i18next';

import { useOpenSettings } from '@/hooks/useInterceptingRoutes';
import { SidebarTabKey } from '@/store/global/initialState';

const useStyles = createStyles(({ css, token }) => ({
  active: css`
    svg {
      fill: ${rgba(token.colorPrimary, 0.25)};
    }
  `,
}));

interface Props {
  className?: string;
  tabBarKey?: SidebarTabKey;
}

export default memo<Props>(({ className, tabBarKey }) => {
  const { t } = useTranslation('common');
  const { styles } = useStyles();
  const openSettings = useOpenSettings();
  const navigate = useNavigate();
  const items: MobileTabBarProps['items'] = useMemo(
    () => [
      {
        icon: (active) => (
          <Icon className={active ? styles.active : undefined} icon={MessageSquare} />
        ),
        key: SidebarTabKey.Chat,
        onClick: () => {
          navigate('/chat');
        },
        title: t('tab.chat'),
      },
      {
        icon: (active) => <Icon className={active ? styles.active : undefined} icon={User} />,
        key: SidebarTabKey.Setting,
        onClick: openSettings,
        title: t('tab.setting'),
      },
    ],
    [t],
  );

  return <MobileTabBar activeKey={tabBarKey} className={className} items={items} safeArea />;
});
