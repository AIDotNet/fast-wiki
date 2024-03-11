import { memo, useEffect, useState } from "react";
import { ChatShareDto } from "../../../models";
import type { TableProps } from 'antd';
import { Button, Table } from 'antd';
import styled from 'styled-components';
import CreateApplication from "./CreateApplication";
import { GetChatShareList } from "../../../services/ChatApplicationService";
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
        },
        {
            title: '对话类型',
            dataIndex: 'typeName',
            key: 'typeName',
        },
        {
            title: '可用Token数量',
            dataIndex: 'availableToken',
            key: 'availableToken',
        },
        {
            title: '可用数量',
            dataIndex: 'availableQuantity',
            key: 'availableQuantity',
        },
        {
            title: '操作',
            key: 'action',
            render: (_, item) => (
                <Button onClick={() => {
                    copyToClipboard(location.origin + "/share-chat?id=" + item.id)
                }}>分享</Button>
            ),
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