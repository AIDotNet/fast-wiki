import { useEffect, useState } from "react";
import { Table } from "antd";
import { Button, Dropdown, message } from "antd";
import ShowCode from "./ShowCode";
import { DeleteFunction, EnableFunctionCall, GetFunctionList } from "@/services/FunctionService";

interface IFunctionCallListProps {
    updateFunctionCall: any;
    input:any;
    setInput:any;
}

export default function FunctionCallList({
    updateFunctionCall,
    input,
    setInput
}: IFunctionCallListProps) {
    const [functionCallList, setFunctionCallList] = useState([] as any[]);
    const [total, setTotal] = useState(0);
    const [showCode, setShowCode] = useState(false);
    const [record, setRecord] = useState(null as any);
    const columns = [
        {
            title: '名称',
            dataIndex: 'name',
            key: 'name',
        },
        {
            title: '描述',
            dataIndex: 'description',
            key: 'description',
        },
        {
            title: 'js function',
            dataIndex: 'content',
            key: 'content',
            render: (_text: any, record: any) => {
                return (
                    <Button
                        onClick={() => {
                            setRecord(record)
                            setShowCode(true)
                        }}
                    >
                        查看源码
                    </Button>
                )
            }
        },
        {
            title: '是否启用',
            dataIndex: 'enable',
            key: 'enable',
            render: (_text: any, record: any) => {
                return record.enable ? '启用' : '禁用';
            }
        },
        {
            title: '创建时间',
            dataIndex: 'creationTime',
            key: 'creationTime',
        },
        {
            title: '操作',
            dataIndex: 'action',
            key: 'action',
            render: (_text: any, record: any) => {
                const items = [
                    {
                        label: '编辑',
                        onClick: () => {
                            updateFunctionCall(record);
                        }
                    },
                    {
                        label: record.enable ? '禁用' : '启用',
                        onClick: () => {
                            EnableFunctionCall(record.id, !record.enable)
                                .then(() => {
                                    message.success('操作成功')
                                    getFunctionCallList();
                                })
                                .catch(() => {
                                    message.error('操作失败')
                                })
                        }
                    },
                    {
                        label: '删除',
                        onClick: () => {
                            DeleteFunction(record.id)
                                .then(() => {
                                    message.success('删除成功');
                                    getFunctionCallList();
                                })
                                .catch(() => {
                                    message.error('删除失败');
                                })
                        }
                    }
                ] as any[];
                return (
                    <Dropdown menu={{ items }} trigger={['click']}>
                        <Button>操作</Button>
                    </Dropdown>
                )

            }
        }
    ]

    useEffect(() => {
        getFunctionCallList();
    }, [input]);

    function handleTableChange(page: number, pageSize: number) {
        setInput({
            ...input,
            page: page,
            pageSize: pageSize,
        });
    }

    function getFunctionCallList() {
        GetFunctionList(input)
            .then((res) => {
                setFunctionCallList(res.result);
                setTotal(res.total);
            })
    }

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
                dataSource={functionCallList} />
            <ShowCode visible={showCode} setVisible={()=>{
                setRecord(null);
                setShowCode(false);
            }} record={record}/>
        </>
    )
}