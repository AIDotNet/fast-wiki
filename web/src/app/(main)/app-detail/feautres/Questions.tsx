import { CreateQuestions, GetQuestions, RemoveQuestions } from '@/services/ChatApplicationService';
import { Input, Modal } from '@lobehub/ui';
import { Button, message } from 'antd';
import { createStyles } from 'antd-style';
import { Delete } from 'lucide-react';
import { useEffect, useState } from 'react';
import { Flexbox } from 'react-layout-kit';

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
interface IQuestionsProps {
    id: string;

}

export default function Questions({
    id
}: IQuestionsProps) {

    const [qa, setQa] = useState<any[]>([]);
    const { styles } = useStyles();
    const [createVisible, setCreateVisible] = useState(false);

    const [question, setQuestion] = useState('')

    function createQuestion() {
        CreateQuestions({
            applicationId: id,
            question,
            order: -1
        }).then((res) => {
            message.success('创建成功');
            setCreateVisible(false);
            loadData();
        })
    }

    function loadData() {
        GetQuestions(id)
            .then((res) => {
                setQa(res)
            })
    }

    useEffect(() => {
        if (id) {
            loadData();
        }
    }, [id])

    return (
        <>
            <div style={{
                height: '40px',
                display: 'flex',
                alignItems: 'center',
            }}>
                <Button onClick={() => {
                    setCreateVisible(true);
                }}>新建提问</Button>
            </div>
            <div style={{
                height: 'calc(100vm - 40px)',
                overflow: 'auto',
                padding: '16px',
            }}>
                <Flexbox gap={8} horizontal wrap={'wrap'}>
                    {qa.map((item) => {
                        return <Flexbox
                            align={'center'}
                            className={styles.card}
                            gap={8}
                            horizontal
                            key={item}
                            onClick={() => {

                            }}
                        >
                            {item.question}
                            <Button type='text' icon={<Delete/>} onClick={() => {
                                RemoveQuestions(item.id)
                                    .then(() => {
                                        message.success('删除成功');
                                        loadData();
                                    })
                            }
                            }></Button>
                        </Flexbox>
                    })}
                </Flexbox>
            </div>
            <Modal
                title="新建提问"
                onCancel={() => {
                    setCreateVisible(false);
                }}
                footer={null}
                open={createVisible}>
                <Input value={question} onChange={(e) => {
                    setQuestion(e.target.value)
                }} placeholder='提问' />
                <Button onClick={() => { createQuestion() }} style={{
                    marginTop: '16px',
                }} block>提交</Button>
            </Modal>
        </>
    )
}