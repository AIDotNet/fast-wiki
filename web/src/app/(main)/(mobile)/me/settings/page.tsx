import { useNavigate } from 'react-router-dom';

import { metadataModule } from '@/server/metadata';
import { translation } from '@/server/translation';
import { isMobileDevice } from '@/utils/responsive';

import Category from './features/Category';

export const generateMetadata = async () => {
  const { t } = await translation('setting');
  return metadataModule.generate({
    description: t('header.desc'),
    title: t('header.title'),
    url: '/me/settings',
  });
};

const Page = () => {
  const mobile = isMobileDevice();
  const navigate = useNavigate();

  if (!mobile) {
    navigate('/settings/common');
  }

  return <Category />;
};

Page.displayName = 'MeSettings';

export default Page;
