import { MobileNavBar, MobileNavBarTitle } from '@lobehub/ui';
import { useNavigate } from 'react-router-dom';
import { memo, startTransition } from 'react';
import { useTranslation } from 'react-i18next';
import { Flexbox } from 'react-layout-kit';

import { mobileHeaderSticky } from '@/styles/mobileHeader';

const Header = memo(() => {
  const { t } = useTranslation('common');

  const navigate = useNavigate();
  return (
    <MobileNavBar
      center={
        <MobileNavBarTitle
          title={
            <Flexbox align={'center'} gap={4} horizontal>
              {t('userPanel.profile')}
            </Flexbox>
          }
        />
      }
      onBackClick={() => {
        startTransition(() => navigate('/me'));
      }}
      showBackButton
      style={mobileHeaderSticky}
    />
  );
});

export default Header;
