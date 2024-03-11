import { GridShowcase, LogoThree, SpotlightCard } from '@lobehub/ui';
import { useEffect, useState } from 'react';
import { DeleteChatApplications, GetChatApplicationsList } from '../../../services/ChatApplicationService';
import { Flexbox } from 'react-layout-kit';
import { message, Button, Pagination } from 'antd';
import styled from 'styled-components';
import { DeleteOutlined } from '@ant-design/icons';
import { ChatApplicationDto } from '../../../models';
import { useNavigate } from 'react-router-dom';


const AppItemDetail = styled.div`
    padding: 16px;
`;

interface IAppListProps {
    input: {
        page: number;
        pageSize: number;
    }
    setInput: (input: any) => void;
}

export function AppList(props: IAppListProps) {
    const navigate = useNavigate();

    const [data, setData] = useState<ChatApplicationDto[]>([]);
    const [total, setTotal] = useState(0);

    const render = (item: ChatApplicationDto) => (
        <Flexbox align={'flex-start'} gap={8} horizontal style={{ padding: 16, height: 100, width: '100%' }}>
            <Flexbox onClick={()=>{
                openAppDetail(item.id)
            }} style={{
                width: '100%',
            }}>
                <div style={{ fontSize: 20, fontWeight: 600, width: '100%' }}>
                    {item.name}
                </div>
                <div style={{
                    // 靠底部对齐
                    display: 'flex',
                    alignItems: 'flex-end',
                    justifyContent: 'space-between',
                    width: '100%',
                    height: '100%',
                    marginTop: 8,
                }}>
                    <AppItemDetail>
                        对话模型：
                        {item.chatModel}
                    </AppItemDetail>
                </div>
            </Flexbox>
            <Button
                style={{
                    float: 'inline-end',
                    position: 'absolute',
                    right: 16,
                }}
                icon={<DeleteOutlined />}
                onClick={() => deleteApp(item.id)}
            />
        </Flexbox>
    )

    function openAppDetail(id: string) {
        navigate(`/app/${id}`)
    }

    async function deleteApp(id: string) {
        try {
            await DeleteChatApplications(id);
            message.success('删除成功');
            props.setInput({
                ...props.input,
                page: 1
            })
        } catch (error) {
            message.error('删除失败');
        }
    }

    async function loadingData() {
        try {
            const data = await GetChatApplicationsList(props.input.page, props.input.pageSize);
            setData(data.result);
            setTotal(data.total);
        } catch (error) {
            console.log(error);
            message.error('获取数据失败');
        }
    }

    useEffect(() => {
        loadingData();
    }, [props.input])

    return (<>
        <GridShowcase style={{ width: '100%' }}>
            <LogoThree size={180} style={{ marginTop: -64 }} />
            <div style={{ fontSize: 48, fontWeight: 600, marginTop: -16 }}>应用列表</div>
        </GridShowcase>
        <SpotlightCard style={{
            margin: 16,
            borderRadius: 8,
            boxShadow: '0 0 8px 0 rgba(0,0,0,0.1)',
            overflow: 'auto',
            maxHeight: 'calc(100vh - 100px)',
            padding: 0,
        }} size={data.length} renderItem={render} items={data} >
        </SpotlightCard>
        <Pagination onChange={(page) => {
            props.setInput({
                ...props.input,
                page
            });
        }} total={total} />
    </>)
}