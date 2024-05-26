import { gerServerDeviceInfo, isMobileDevice } from '@/utils/responsive';

import SettingsModal from './index';
const Page = () => {
  const isMobile = isMobileDevice();
  const { os, browser } = gerServerDeviceInfo();

  return <SettingsModal browser={browser} mobile={isMobile} os={os} />;
};

Page.displayName = 'SettingModal';

export default Page;
