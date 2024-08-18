
import { CheckQuantizationState } from '@/services/WikiService';
import { QuantizedListState } from '@/types/wiki';
import { Table, Button } from 'antd';
import { useEffect, useState } from 'react';

interface VectorProps {
    id?: string;
}

export default function VectorPage({
    id
}: VectorProps) {
    const [data, setData] = useState([] as any[]);
    const [total, setTotal] = useState(0);
    const [loading, setLoading] = useState(false);
    const [state, setState] = useState<QuantizedListState | null>(null);
    const [input, setInput] = useState({
        page: 1,
        pageSize: 10
    });

    useEffect(() => {
        if (id) {
            loadData();
        }
    }, [id, input]);

    function loadData() {
        setLoading(true);
        CheckQuantizationState(id as string, state, input.page, input.pageSize).then((res) => {
            setData(res.result);
            setTotal(res.total);
        }).finally(() => {
            setLoading(false);
        });
    }

    return (
        <div>
            <div style={{
                fontSize: '20px',
                fontWeight: 'bold',
                marginBottom: '20px'
            }}>
                量化队列
                <div style={{
                    float: 'right',
                    display: 'flex',
                }} >
                    <Button onClick={() => {
                        loadData();
                    }}>
                        刷新
                    </Button>
                </div>
            </div>
            <Table
                loading={loading}
                dataSource={data}
                pagination={{
                    total: total,
                    current: input.page,
                    pageSize: input.pageSize,
                    onChange: (page, pageSize) => {
                        setInput({
                            ...input,
                            page,
                            pageSize
                        });
                    }
                }}
                columns={[
                    {
                        key: 'fileName',
                        title: '文件名',
                        dataIndex: 'fileName'
                    },
                    {
                        key: 'stateName',
                        title: '状态',
                        dataIndex: 'stateName'
                    },
                    {
                        key: 'remark',
                        title: '备注',
                        dataIndex: 'remark'
                    },
                    {
                        key: 'processTime',
                        title: '处理时间',
                        dataIndex: 'processTime'
                    },
                    {
                        key: 'creationTime',
                        title: '创建时间',
                        dataIndex: 'creationTime'
                    }
                ]}
            >

            </Table>
        </div>
    )
}