import { isMobileDevice } from '@/utils/responsive';

import Actions from './features/Actions';
import Hero from './features/Hero';
import Logo from './features/Logo';


const Page =  () => {
  const mobile = isMobileDevice();
  return (
    <>
      <Logo mobile={mobile} />
      <Hero />
      <Actions mobile={mobile} />
    </>
  );
};

Page.displayName = 'Welcome';

export default Page;
