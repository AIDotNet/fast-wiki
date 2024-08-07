declare var window: any;
import { createStyles } from 'antd-style';
import { memo, useEffect, useState } from 'react';
import { Flexbox } from 'react-layout-kit';

import { GetQuestions, SharedQuestions } from '@/services/ChatApplicationService';

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

// @ts-ignore
const QuestionSuggest = memo<{ mobile?: boolean }>(({ mobile }) => {

  const { styles } = useStyles();
  const [qas, setQa] = useState<any[]>([]);


  function loadData() {
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
        <div className={styles.title}>
          常见问题
        </div>
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
      </Flexbox>
    </Flexbox>
  );
});

export default QuestionSuggest;
