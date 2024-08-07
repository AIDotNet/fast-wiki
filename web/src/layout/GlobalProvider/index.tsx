import  {  useState, useEffect } from 'react';
import { ServerConfigStoreProvider } from '@/store/serverConfig';

import AppTheme from './AppTheme';
import Locale from './Locale';
import StoreInitialization from './StoreInitialization';
import StyleRegistry from './StyleRegistry';

const GlobalLayout = ({ children }: any) => {
  const [appearance, setAppearance] = useState('');
  const [neutralColor, setNeutralColor] = useState('');
  const [primaryColor, setPrimaryColor] = useState('');

  useEffect(() => {
    setAppearance(appearance);
    setNeutralColor(neutralColor);
    setPrimaryColor(primaryColor);
  }, []);

  return (
    <ServerConfigStoreProvider>
      <AppTheme>
        <Locale>
          <StoreInitialization>
          </StoreInitialization>
            <StyleRegistry>
              {children}
            </StyleRegistry>
        </Locale>
      </AppTheme>
    </ServerConfigStoreProvider>
  );
};

export default GlobalLayout;