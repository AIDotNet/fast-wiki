import { FluentEmoji, Markdown } from '@lobehub/ui';
import { createStyles } from 'antd-style';
import { memo} from 'react';
import { Center, Flexbox } from 'react-layout-kit';

import { useApplication, useGreeting } from '@/hooks/useGreeting';
import { useServerConfigStore } from '@/store/serverConfig';

import QuestionSuggest from './QuestionSuggest';

const useStyles = createStyles(({ css, responsive }) => ({
  container: css`
    align-items: center;
    ${responsive.mobile} {
      align-items: flex-start;
    }
  `,
  desc: css`
    font-size: 14px;
    text-align: center;
    ${responsive.mobile} {
      text-align: left;
    }
  `,
  title: css`
    margin-top: 0.2em;
    margin-bottom: 0;

    font-size: 32px;
    font-weight: bolder;
    line-height: 1;
    ${responsive.mobile} {
      font-size: 24px;
    }
  `,
}));

const InboxWelcome = memo(() => {
  const { styles } = useStyles();
  const mobile = useServerConfigStore((s) => s.isMobile);
  const greeting = useGreeting();
  const application = useApplication() as any;


  return (
    <Center padding={16} width={'100%'}>
      <Flexbox className={styles.container} gap={16} style={{ maxWidth: 800 }} width={'100%'}>
        <Flexbox align={'center'} gap={8} horizontal>
          <FluentEmoji emoji={'👋'} size={40} type={'anim'} />
          <h1 className={styles.title}>{greeting}</h1>
        </Flexbox>
        <Markdown className={styles.desc} variant={'chat'}>
          {application?.opener}
        </Markdown>
        <QuestionSuggest mobile={mobile} />
      </Flexbox>
    </Center>
  );
});

export default InboxWelcome;
