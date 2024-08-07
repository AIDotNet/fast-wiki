import { UAParser } from 'ua-parser-js';

/**
 * check mobile device in server
 */
export const isMobileDevice = () => {

  const device = new UAParser('').getDevice();

  return device.type === 'mobile';
};

/**
 * check mobile device in server
 */
export const gerServerDeviceInfo = () => {
  const parser = new UAParser( '');

  return {
    browser: parser.getBrowser().name,
    isMobile: isMobileDevice(),
    os: parser.getOS().name,
  };
};
