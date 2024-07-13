import { StyleProvider, extractStaticStyle } from 'antd-style';
import { PropsWithChildren } from 'react';

const StyleRegistry = ({ children }: PropsWithChildren) => {
  return <StyleProvider cache={extractStaticStyle.cache}>{children}</StyleProvider>;
};

export default StyleRegistry;
