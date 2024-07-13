

import { ChatHeader, ChatHeaderTitle } from '@lobehub/ui';
import { useNavigate } from 'react-router-dom';
import { memo } from 'react';
import { useTranslation } from 'react-i18next';

import { pathString } from '@/utils/url';

import HeaderContent from '../../features/HeaderContent';

const Header = memo(() => {
  const { t } = useTranslation('setting');
  const navigate = useNavigate();

  return (
    <ChatHeader
      left={<ChatHeaderTitle title={t('header.session')} />}
      onBackClick={() => navigate(pathString('/chat', { search: location.search }))}
      right={<HeaderContent />}
      showBackButton
    />
  );
});

export default Header;
