import { memo, useEffect, useState } from "react";
import { ChatShareDto } from "../../../models";
import type { TableProps } from 'antd';
import { Button, Table, message, Dropdown, MenuProps } from 'antd';
import styled from 'styled-components';
import CreateApplication from "./CreateApplication";
import { GetChatShareList, RemoveChatShare } from "../../../services/ChatApplicationService";
import { copyToClipboard } from "../../../utils/stringHelper";


const Title = styled.div`
    font-size: 30px;
    font-weight: 600;
    margin-bottom: 16px;

`;
interface IReleaseApplicationProps {
    id: string;
}

export default memo((props: IReleaseApplicationProps) => {
    const columns: TableProps<ChatShareDto>['columns'] = [
        {
            title: '文件名',
            dataIndex: 'name',
            key: 'name',
        },
        {
            title: '过期时间',
            dataIndex: 'expires',
            key: 'expires',
            render: (text) => {
                return text ? new Date(text).toLocaleString() : '永久有效';
            }
        },
        {
            title: '使用Token数量',
            dataIndex: 'usedToken',
            key: 'usedToken',
        },
        {
            title: '可用Token数量',
            dataIndex: 'availableToken',
            key: 'availableToken',
            render: (text, item) => {
                if (text === -1) {
                    return '无限制';
                }
                return text - item.usedToken;
            }
        },
        {
            title: '可用数量',
            dataIndex: 'availableQuantity',
            key: 'availableQuantity',
        },
        {
            title: <div style={{
                textAlign: 'center',
            }}>
                操作
            </div>,
            key: 'action',
            render: (_, item) => {

                const items: MenuProps['items'] = [];
                items.push({
                    key: '1',
                    label: '分享链接',
                    onClick: () => {
                        copyToClipboard(location.origin + "/share-chat?id=" + item.id)
                        message.success('复制成功');
                    }
                })
                items.push({
                    key: '2',
                    label: '复制Key',
                    onClick: () => {
                        copyToClipboard(item.apiKey)
                        message.success('复制APIKey成功');
                    }
                })
                items.push({
                    key: '3',
                    label: '删除',
                    onClick: async () => {
                        await RemoveChatShare(item.id);
                        message.success('删除成功');
                        setInput({
                            ...input,
                            page: 1
                        })
                    }
                })

                return (
                    <>
                        <Dropdown menu={{ items }} trigger={['click']}>
                            <Button>操作</Button>
                        </Dropdown>
                    </>
                )
            },
        },
    ];

    const [data, setData] = useState<ChatShareDto[]>([]);
    const [visible, setVisible] = useState(false);
    const [input, setInput] = useState({
        page: 1,
        pageSize: 10,
        chatApplicationId: props.id
    } as any);

    const [total, setTotal] = useState(0);
    useEffect(() => {
        setInput({
            ...input,
            chatApplicationId: props.id
        });

    }, [props.id])

    function handleTableChange(page: number, pageSize: number) {
        setInput({
            ...input,
            page: page,
            pageSize: pageSize,
        });
    }

    function loadingData() {
        GetChatShareList(input.chatApplicationId, input.page, input.pageSize)
            .then((result) => {
                setData(result.result);
                setTotal(result.total);
            })
    }

    useEffect(() => {
        loadingData();
    }, [input]);


    return (<>
        <Title>
            发布应用
            <Button
                onClick={() => setVisible(true)}
                style={{
                    float: 'inline-end',
                    position: 'absolute',
                    right: 16,
                }}
            >发布</Button>
        </Title>
        <Table
            pagination={{
                current: input.page,
                pageSize: input.pageSize,
                total: total,
                onChange: handleTableChange,
            }}
            scroll={{ y: 'calc(100vh - 240px)' }}
            columns={columns}
            dataSource={data} />
        <CreateApplication id={props.id} visible={visible}
            onClose={() => setVisible(false)}
            onSuccess={() => {
                setInput({
                    ...input,
                    page: 1
                });
                setVisible(false);
            }}
        />
    </>
    );
});