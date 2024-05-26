import { memo, useEffect, useState } from "react";
import { copyToClipboard } from '@lobehub/ui';
import type { TableProps } from 'antd';
import { Button, Table, message, Dropdown, MenuProps } from 'antd';
import styled from 'styled-components';
import CreateApplication from "./CreateApplication";
import { FAST_API_URL } from "@/const/trace";
import { GetChatShareList, RemoveChatShare } from "@/services/ChatApplicationService";


const Title = styled.div`
    font-size: 30px;
    font-weight: 600;
    margin-bottom: 16px;

`;
interface IReleaseApplicationProps {
    id: string;
}

export default memo((props: IReleaseApplicationProps) => {
    const columns: TableProps<any>['columns'] = [
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
                        copyToClipboard(location.origin + "/chat?sharedId=" + item.id)
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
                    label: '复制飞书对接地址',
                    onClick: async () => {
                        let url = FAST_API_URL;
                        if (!url) {
                            url = location.origin;
                        }
                        // 删除最后的/
                        if (url.endsWith('/')) {
                            url = url.slice(0, url.length - 1);
                        }
                        copyToClipboard(url + "/v1/feishu/completions/" + item.id)
                        message.success('复制成功');
                    }
                })

                items.push({
                    key: '6',
                    label: '复制微信公众号对接地址',
                    onClick: async () => {
                        let url = FAST_API_URL;
                        if (!url) {
                            url = location.origin;
                        }
                        // 删除最后的/
                        if (url.endsWith('/')) {
                            url = url.slice(0, url.length - 1);
                        }
                        copyToClipboard(url + "/api/v1/WeChatService/ReceiveMessage/" + item.id)
                        message.success('复制成功');
                    }
                })

                items.push({
                    key: '4',
                    label: '复制悬浮球接入地址',
                    onClick: async () => {
                        let value = `
                        <script \n
                            src="${location.origin}/js/chat.js" \n
                            id="fastwiki-iframe" \n
                            data-bot-src="${location.origin}/chat?sharedId=${item.id}" \n
                            data-default-open="true" \n
                            data-drag="true" \n
                            data-open-icon="data:image/svg+xml;base64,PD94bWwgdmVyc2lvbj0iMS4wIiBzdGFuZGFsb25lPSJubyI/PjwhRE9DVFlQRSBzdmcgUFVCTElDICItLy9XM0MvL0RURCBTVkcgMS4xLy9FTiIgImh0dHA6Ly93d3cudzMub3JnL0dyYXBoaWNzL1NWRy8xLjEvRFREL3N2ZzExLmR0ZCI+PHN2ZyB0PSIxNzE1MDA0NjQyNTU1IiBjbGFzcz0iaWNvbiIgdmlld0JveD0iMCAwIDEwMjQgMTAyNCIgdmVyc2lvbj0iMS4xIiB4bWxucz0iaHR0cDovL3d3dy53My5vcmcvMjAwMC9zdmciIHAtaWQ9IjU1MDciIHhtbG5zOnhsaW5rPSJodHRwOi8vd3d3LnczLm9yZy8xOTk5L3hsaW5rIiB3aWR0aD0iMjAwIiBoZWlnaHQ9IjIwMCI+PHBhdGggZD0iTTUxMiA2NGMyNTkuMiAwIDQ2OS4zMzMzMzMgMjAwLjU3NiA0NjkuMzMzMzMzIDQ0OHMtMjEwLjEzMzMzMyA0NDgtNDY5LjMzMzMzMyA0NDhhNDg0LjQ4IDQ4NC40OCAwIDAgMS0yMzIuNzI1MzMzLTU4Ljg4bC0xMTYuMzk0NjY3IDUwLjY0NTMzM2E0Mi42NjY2NjcgNDIuNjY2NjY3IDAgMCAxLTU4LjUxNzMzMy00OS4wMDI2NjZsMjkuNzYtMTI1LjAxMzMzNEM3Ni42MjkzMzMgNzAzLjQwMjY2NyA0Mi42NjY2NjcgNjExLjQ3NzMzMyA0Mi42NjY2NjcgNTEyIDQyLjY2NjY2NyAyNjQuNTc2IDI1Mi44IDY0IDUxMiA2NHogbTAgNjRDMjg3LjQ4OCAxMjggMTA2LjY2NjY2NyAzMDAuNTg2NjY3IDEwNi42NjY2NjcgNTEyYzAgNzkuNTczMzMzIDI1LjU1NzMzMyAxNTUuNDM0NjY3IDcyLjU1NDY2NiAyMTkuMjg1MzMzbDUuNTI1MzM0IDcuMzE3MzM0IDE4LjcwOTMzMyAyNC4xOTItMjYuOTY1MzMzIDExMy4yMzczMzMgMTA1Ljk4NC00Ni4wOCAyNy40NzczMzMgMTUuMDE4NjY3QzM3MC44NTg2NjcgODc4LjIyOTMzMyA0MzkuOTc4NjY3IDg5NiA1MTIgODk2YzIyNC41MTIgMCA0MDUuMzMzMzMzLTE3Mi41ODY2NjcgNDA1LjMzMzMzMy0zODRTNzM2LjUxMiAxMjggNTEyIDEyOHogbS0xNTcuNjk2IDM0MS4zMzMzMzNhNDIuNjY2NjY3IDQyLjY2NjY2NyAwIDEgMSAwIDg1LjMzMzMzNCA0Mi42NjY2NjcgNDIuNjY2NjY3IDAgMCAxIDAtODUuMzMzMzM0eiBtMTU5LjAxODY2NyAwYTQyLjY2NjY2NyA0Mi42NjY2NjcgMCAxIDEgMCA4NS4zMzMzMzQgNDIuNjY2NjY3IDQyLjY2NjY2NyAwIDAgMSAwLTg1LjMzMzMzNHogbTE1OC45OTczMzMgMGE0Mi42NjY2NjcgNDIuNjY2NjY3IDAgMSAxIDAgODUuMzMzMzM0IDQyLjY2NjY2NyA0Mi42NjY2NjcgMCAwIDEgMC04NS4zMzMzMzR6IiBmaWxsPSIjMTI5NmRiIiBwLWlkPSI1NTA4Ij48L3BhdGg+PC9zdmc+" \n
                            data-close-icon="data:image/svg+xml;base64,PHN2ZyB0PSIxNjkwNTM1NDQxNTI2IiBjbGFzcz0iaWNvbiIgdmlld0JveD0iMCAwIDEwMjQgMTAyNCIgdmVyc2lvbj0iMS4xIiB4bWxucz0iaHR0cDovL3d3dy53My5vcmcvMjAwMC9zdmciIHAtaWQ9IjYzNjciIHhtbG5zOnhsaW5rPSJodHRwOi8vd3d3LnczLm9yZy8xOTk5L3hsaW5rIj48cGF0aCBkPSJNNTEyIDEwMjRBNTEyIDUxMiAwIDEgMSA1MTIgMGE1MTIgNTEyIDAgMCAxIDAgMTAyNHpNMzA1Ljk1NjU3MSAzNzAuMzk1NDI5TDQ0Ny40ODggNTEyIDMwNS45NTY1NzEgNjUzLjYwNDU3MWE0NS41NjggNDUuNTY4IDAgMSAwIDY0LjQzODg1OCA2NC40Mzg4NThMNTEyIDU3Ni41MTJsMTQxLjYwNDU3MSAxNDEuNTMxNDI5YTQ1LjU2OCA0NS41NjggMCAwIDAgNjQuNDM4ODU4LTY0LjQzODg1OEw1NzYuNTEyIDUxMmwxNDEuNTMxNDI5LTE0MS42MDQ1NzFhNDUuNTY4IDQ1LjU2OCAwIDEgMC02NC40Mzg4NTgtNjQuNDM4ODU4TDUxMiA0NDcuNDg4IDM3MC4zOTU0MjkgMzA1Ljk1NjU3MWE0NS41NjggNDUuNTY4IDAgMCAwLTY0LjQzODg1OCA2NC40Mzg4NTh6IiBmaWxsPSIjNGU4M2ZkIiBwLWlkPSI2MzY4Ij48L3BhdGg+PC9zdmc+" \n
                            defer \n
                        ></script>`

                        copyToClipboard(value)
                        message.success('复制成功');
                    }
                })

                items.push({
                    key: '5',
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

    const [data, setData] = useState<any[]>([]);
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