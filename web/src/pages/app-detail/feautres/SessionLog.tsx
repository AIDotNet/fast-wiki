import { ChatDialogDto } from "../../../models";
import type { TableProps } from 'antd';
import { Button, Table } from 'antd';
import { useEffect, useState } from "react";
import { GetSessionLogDialog } from "../../../services/ChatApplicationService";
import styled from 'styled-components';


const Title = styled.div`
    font-size: 30px;
    font-weight: 600;
    margin-bottom: 16px;

`;

interface ISessionLogProps {
    id: string;
}

export default function SessionLog({ id }: ISessionLogProps) {
    const columns: TableProps<ChatDialogDto>['columns'] = [
        {
            title: '对话名称',
            dataIndex: 'name',
            key: 'name',
        },
        {
            title: '对话描述',
            dataIndex: 'description',
            key: 'description',
        },
        {
            title: '对话类型',
            dataIndex: 'typeName',
            key: 'typeName',
        },
        {
            title: '对话创建时间',
            dataIndex: 'creationTime',
            key: 'creationTime',
        },
        {
            title: 'Action',
            key: 'action',
            render: (_, record) => (
                <Button>查看详情</Button>
            ),
        },
    ];

    const [data, setData] = useState<ChatDialogDto[]>([]);
    const [total, setTotal] = useState(0);
    const [input, setInput] = useState({
        page: 1,
        pageSize: 10,
        chatApplicationId: id
    } as any);

    useEffect(() => {
        setInput({
            ...input,
            chatApplicationId: id
        });
    }, [id]);

    function loadingData() {
        GetSessionLogDialog(input.chatApplicationId, input.page, input.pageSize)
            .then((result) => {
                setData(result.result);
                setTotal(result.total);
            })
    }

    useEffect(() => {
        loadingData();
    }, [input]);

    function handleTableChange(page: number, pageSize: number) {
        
        setInput({
            ...input,
            page: page,
            pageSize: pageSize,
        });
    }

    return (
        <>
            <Title>
                对话日志
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
        </>)
}