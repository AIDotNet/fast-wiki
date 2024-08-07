import { ThemeAppearance, createStyles } from 'antd-style';
import 'antd/dist/reset.css';
import { ConfigProvider, NeutralColors, PrimaryColors, ThemeProvider } from '@lobehub/ui';
import { ReactNode, memo } from 'react';
import {
  useUserStore
} from '@/store/user';

export interface AppThemeProps {
  children?: ReactNode;
  defaultAppearance?: ThemeAppearance;
  defaultNeutralColor?: NeutralColors;
  defaultPrimaryColor?: PrimaryColors;
}

const useStyles = createStyles(({ css, token }: any) => ({
  app: css`
      position: relative;
  
      overscroll-behavior: none;
      display: flex;
      flex-direction: column;
      align-items: center;
  
      height: 100%;
      min-height: 100dvh;
      max-height: 100dvh;
  
      @media (min-device-width: 576px) {
        overflow: hidden;
      }
    `,
  scrollbar: css`
      scrollbar-color: ${token.colorFill} transparent;
      scrollbar-width: thin;
  
      #lobe-mobile-scroll-container {
        scrollbar-width: none;
  
        ::-webkit-scrollbar {
          width: 0;
          height: 0;
        }
      }
    `,

  // so this is a polyfill for older browsers
  scrollbarPolyfill: css`
      ::-webkit-scrollbar {
        width: 0.75em;
        height: 0.75em;
      }
  
      ::-webkit-scrollbar-thumb {
        border-radius: 10px;
      }
  
      :hover::-webkit-scrollbar-thumb {
        background-color: ${token.colorText};
        background-clip: content-box;
        border: 3px solid transparent;
      }
  
      ::-webkit-scrollbar-track {
        background-color: transparent;
      }
    `,
}));

const AppTheme = memo<AppThemeProps>(({
  children, defaultAppearance, defaultPrimaryColor, defaultNeutralColor
}) => {
  const {
    theme,
  } = useUserStore();

  const { styles, cx } = useStyles();


  return (
    <ThemeProvider
      className={cx(styles.app, styles.scrollbar, styles.scrollbarPolyfill)}
      customTheme={{
        neutralColor: defaultNeutralColor,
        primaryColor: defaultPrimaryColor,
      }}
      defaultAppearance={defaultAppearance}
      onAppearanceChange={(appearance) => {
        localStorage.setItem('theme', appearance);
      }}
      themeMode={theme}
    >
      <ConfigProvider config={{ imgUnoptimized: true }}>
        {children}
      </ConfigProvider>
    </ThemeProvider>
  );
});

export default AppTheme;