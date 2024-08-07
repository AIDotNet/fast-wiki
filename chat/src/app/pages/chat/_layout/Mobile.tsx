
import { createStyles } from 'antd-style';
import { memo, useEffect } from 'react';
import { Flexbox } from 'react-layout-kit';

const useStyles = createStyles(({ css, token }) => ({
  main: css`
    position: relative;
    overflow: hidden;
    background: ${token.colorBgLayout};
  `,
}));

const Layout = memo(() => {

  const { styles } = useStyles();
  useEffect(() => {
  }, []);

  return (
    <>
      <Flexbox
        className={styles.main}
        height="100%"
        width="100%"
      >

      </Flexbox>
    </>
  );
});

Layout.displayName = 'MobileChatLayout';

export default Layout;
