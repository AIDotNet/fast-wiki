import { useEffect, useState } from "react";
import { Table, Button, Dropdown, message } from "antd";
import { GetModelList, DeleteFastModel, EnableFastModel } from "../../../services/ModelService";
import UpdateModel from "./UpdateModel";



export default function ModelList(updateData: any) {

    const [input, setInput] = useState({
        page: 1,
        pageSize: 10,
        keyword: ''
    });
    const [total, setTotal] = useState(0);
    const [data, setData] = useState([]);
    const [updateValue, setUpdateValue] = useState({} as any);
    const [updateVisible, setUpdateVisible] = useState(false);
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
            render: (_: any, item: any) => {
                // 自动换算，如果超过1000，显示为1K， 如果超过1000000，显示为1M, 以此类推到1B
                item.usedQuota = item.usedQuota || 0;
                if (item.usedQuota > 1000000000) {
                    return `${(item.usedQuota / 1000000000).toFixed(2)}B`;
                }
                if (item.usedQuota > 1000000) {
                    return `${(item.usedQuota / 1000000).toFixed(2)}M`;
                }
                if (item.usedQuota > 1000) {
                    return `${(item.usedQuota / 1000).toFixed(2)}K`;
                }
                return item.usedQuota;                
            }
        },
        {
            title: '是否启用',
            dataIndex: 'enable',
            key: 'enable',
            render: (_: any, item: any) => {
                return (
                    <div>
                        {item.enable ? '是' : '否'}
                    </div>
                )
            }
        },
        {
            title: '操作',
            dataIndex: 'acting',
            key: 'acting',
            render: (_: any, item: any) => {

                const items = [
                    {
                        key: '1',
                        label: '编辑',
                        onClick: () => {
                            setUpdateValue(item);
                            setUpdateVisible(true);
                        }
                    },
                    {
                        key: '2',
                        label: item.enable ? '禁用' : '启用',
                        onClick: async () => {
                            try {
                                await EnableFastModel(item.id, !item.enable);
                                message.success('操作成功');
                                loadingData();
                            } catch (error) {
                                message.error('操作失败');
                            }    
                        }
                    },
                    {
                        key: '3',
                        label: '删除',
                        onClick: () => {
                            removeModel(item.id)
                        }
                    }
                ]

                return (
                    <>
                        <Dropdown menu={{ items }} trigger={['click']}>
                            <Button>操作</Button>
                        </Dropdown>
                    </>
                )
            }
        },
    ];

    async function removeModel(id: string) {
        await DeleteFastModel(id);
        message.success('删除成功');
        loadingData();
    }

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
        <>
            <Table
                pagination={{
                    current: input.page,
                    pageSize: input.pageSize,
                    total: total,
                    onChange: handleTableChange,
                }}
                columns={columns}
                dataSource={data} />
            <UpdateModel visible={updateVisible} onSuccess={() => {
                setUpdateVisible(false);
                loadingData();
            }} value={updateValue} onCancel={() => setUpdateVisible(false)} />
        </>
    );
}