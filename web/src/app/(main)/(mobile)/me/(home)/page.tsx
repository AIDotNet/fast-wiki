import { useNavigate } from 'react-router-dom';
import { Center } from 'react-layout-kit';

import BrandWatermark from '@/components/BrandWatermark';
import { metadataModule } from '@/server/metadata';
import { translation } from '@/server/translation';
import { isMobileDevice } from '@/utils/responsive';

import Category from './features/Category';
import UserBanner from './features/UserBanner';

export const generateMetadata = async () => {
  const { t } = await translation('common');
  return metadataModule.generate({
    title: t('tab.me'),
    url: '/me',
  });
};

const Page = () => {
  const mobile = isMobileDevice();
  const navigate = useNavigate();
  if (!mobile) {
    navigate('/chat');
  }

  return (
    <>
      <UserBanner />
      <Category />
      <Center padding={16}>
        <BrandWatermark />
      </Center>
    </>
  );
};

Page.displayName = 'Me';

export default Page;
