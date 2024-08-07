import {  SpotlightCard } from '@lobehub/ui';
import { useEffect, useState } from 'react';
import { Flexbox } from 'react-layout-kit';
import { message, Button, Pagination, Dropdown } from 'antd';
import { DeleteChatApplications, GetChatApplicationsList } from '@/services/ChatApplicationService';
import { useNavigate } from 'react-router-dom';
import { MenuIcon } from 'lucide-react';

interface IAppListProps {
    input: {
        page: number;
        pageSize: number;
    }
    setInput: (input: any) => void;
}

export function AppList(props: IAppListProps) {
    const navigate = useNavigate();

    const [data, setData] = useState<any[]>([]);
    const [total, setTotal] = useState(0);

    const render = (item: any) => (
        <Flexbox align={'flex-start'} gap={8} horizontal style={{ padding: 16, height: 100, width: '100%' }}>
            <Flexbox style={{
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
                    对话模型：
                    {item.chatModel}
                </div>
            </Flexbox>
            <Dropdown
                menu={{
                    items: [
                        {
                            key: 'open-app',
                            label: '进入应用',
                            onClick: () => openAppDetail(item.id)
                        },
                        {
                            key: 'open-chat',
                            label: '进入对话',
                            onClick: () => openChat(item.id)
                        },
                        {
                            key: 'delete',
                            label: '删除',
                            onClick: () => deleteApp(item.id)
                        },
                    ]
                }}
            >
                <Button
                    style={{
                        float: 'inline-end',
                        position: 'absolute',
                        right: 16,
                    }}
                    icon={<MenuIcon />}
                />
            </Dropdown>
        </Flexbox>
    )

    function openChat(id: string) {
        navigate(`/chat?id=${id}`);
    }

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