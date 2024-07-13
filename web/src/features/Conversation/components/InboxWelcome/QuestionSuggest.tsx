declare var window: any;
import { ActionIcon } from '@lobehub/ui';
import { createStyles } from 'antd-style';
import { shuffle } from 'lodash-es';
import { ArrowRight } from 'lucide-react';
import { memo, useEffect, useState } from 'react';
import { useTranslation } from 'react-i18next';
import { Flexbox } from 'react-layout-kit';

import { USAGE_DOCUMENTS } from '@/const/url';
import { useSendMessage } from '@/features/ChatInput/useSend';
import { useChatStore } from '@/store/chat';
import { GetQuestions, SharedQuestions } from '@/services/ChatApplicationService';
import { Link } from 'react-router-dom';

const useStyles = createStyles(({ css, token, responsive }) => ({
  card: css`
    cursor: pointer;

    padding: 12px 24px;

    color: ${token.colorText};

    background: ${token.colorBgContainer};
    border-radius: 48px;

    &:hover {
      background: ${token.colorBgElevated};
    }

    ${responsive.mobile} {
      padding: 8px 16px;
    }
  `,
  icon: css`
    color: ${token.colorTextSecondary};
  `,
  title: css`
    color: ${token.colorTextDescription};
  `,
}));

const qa = shuffle([
  'q01',
  'q02',
  'q03',
  'q04',
  'q05',
  'q06',
  'q07',
  'q08',
  'q09',
  'q10',
  'q11',
  'q12',
  'q13',
  'q14',
  'q15',
]);

const QuestionSuggest = memo<{ mobile?: boolean }>(({ mobile }) => {
  const [updateInputMessage] = useChatStore((s) => [s.updateInputMessage]);

  const { t } = useTranslation('welcome') as any
  const { styles } = useStyles();
  const sendMessage = useSendMessage();
  const [qas, setQa] = useState<any[]>([]);


  function loadData() {

    if (typeof window === 'undefined') return;
    // 获取当前query中的sharedId
    const query = new URLSearchParams(window.location.search);
    const sharedId = query.get('sharedId');
    const id = query.get('id');

    if (sharedId) {
      SharedQuestions(sharedId)
        .then((res) => {
          setQa(res)
        })
      return null;
    }
    if (id) {
      GetQuestions(id)
        .then((res) => {
          setQa(res)
        })
    }
  }

  useEffect(() => {
    loadData();
  }, [])

  return (
    <Flexbox gap={8} width={'100%'}>
      <Flexbox align={'center'} horizontal justify={'space-between'}>
        <div className={styles.title}>{t('guide.questions.title')}</div>
        <Link to={USAGE_DOCUMENTS} target={'_blank'}>
          <ActionIcon
            icon={ArrowRight}
            size={{ blockSize: 24, fontSize: 16 }}
            title={t('guide.questions.moreBtn')}
          />
        </Link>
      </Flexbox>
      <Flexbox gap={8} horizontal wrap={'wrap'}>
        {
          qas.length === 0 && <>
            <h2 style={{
              fontSize: '20px',
              textAlign: 'center',
              width: '100%',
              marginTop: '20px',
            }}>
              暂无问题
            </h2>
          </>
        }
        {
          qas.map((item) => {
            return <Flexbox
              align={'center'}
              className={styles.card}
              gap={8}
              horizontal
              key={item}
              onClick={() => {
                updateInputMessage(item.question);
                sendMessage({ isWelcomeQuestion: true });
              }}
            >
              {item.question}
            </Flexbox>
          })
        }
      </Flexbox>
    </Flexbox>
  );
});

export default QuestionSuggest;
