import { useEffect, useState } from "react";
import { Table, Button } from "antd";
import { GetModelList } from "../../../services/ModelService";



export default function ModelList(updateData: any) {

    const [input, setInput] = useState({
        page: 1,
        pageSize: 10,
        keyword: ''
    });
    const [total, setTotal] = useState(0);
    const [data, setData] = useState([]);
    function handleTableChange(page: number, pageSize: number) {
        setInput({
            ...input,
            page: page,
            pageSize: pageSize,
        });
    }

    useEffect(() => {
        loadingData();
    }, [
        updateData
    ]);

    const columns = [
        {
            title: '名称',
            dataIndex: 'name',
            key: 'name',
        },
        {
            title: '模型类型',
            dataIndex: 'type',
            key: 'type',
        },
        {
            title: '模型代理地址',
            dataIndex: 'url',
            key: 'url',
        },
        {
            title: '描述',
            dataIndex: 'description',
            key: 'description',
        },
        {
            title: '优先级',
            dataIndex: 'order',
            key: 'order',
        },
        {
            title: '测试时间',
            dataIndex: 'testTime',
            key: 'testTime',
        },
        {
            title: '已消耗配额',
            dataIndex: 'usedQuota',
            key: 'usedQuota',
        },
        {
            title: '是否启用',
            dataIndex: 'enable',
            key: 'enable'
        },
        {
            title: '操作',
            dataIndex: 'acting',
            key: 'acting',
            render: (_: any, item: any) => {
                return (
                    <>
                        <Button>操作</Button>

                    </>
                )
            }
        },
    ];

    async function loadingData() {
        try {
            const result = await GetModelList(input.keyword, input.page, input.pageSize);
            setData(result.result);
            setTotal(result.total);
        } catch (error) {

        }
    }

    useEffect(() => {
        loadingData();
    }, []);


    return (
        <Table
            pagination={{
                current: input.page,
                pageSize: input.pageSize,
                total: total,
                onChange: handleTableChange,
            }}
            columns={columns}
            dataSource={data} />
    );
}