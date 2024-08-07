import { Table, Button, Dropdown, MenuProps, message, Select } from 'antd';
import { useEffect, useState } from 'react';
import WikiDetailFile from './WikiDetailFile';
import { WikiQuantizationState } from './index.d';
import { DeleteWikiDetails, DetailsRenameName, GetWikiDetailsList, RetryVectorDetail } from '@/services/WikiService';

interface IWikiDataProps {
    id: string;
    onChagePath(key: string): void;
}

export default function WikiData({ id, onChagePath }: IWikiDataProps) {


    const columns = [
        {
            title: '文件名',
            dataIndex: 'fileName',
            key: 'fileName',
            render: (text: string, item: any) => {
                return item.isedit ? <input
                    autoFocus
                    style={{
                        width: '100%',
                        border: 'none',
                        outline: 'none',
                        background: 'transparent',
                        fontSize: 14
                    }}
                    onBlur={async (el) => {
                        item.isedit = false;
                        if (el.target.value !== text) {
                            item.fileName = el.target.value;
                        }
                        setData([...data]);

                        try {
                            await DetailsRenameName(item.id, el.target.value);
                            message.success('修改成功');
                        } catch (error) {
                            message.error('修改失败');
                        }
                    }}
                    defaultValue={text}
                ></input> : <span onDoubleClick={() => handleDoubleClick(item)}>{text}</span>;
            },
        },
        {
            title: '索引数量',
            dataIndex: 'dataCount',
            key: 'dataCount',
        },
        {
            title: '数据类型',
            dataIndex: 'type',
            key: 'type',
        },
        {
            title: '数据状态',
            key: 'stateName',
            dataIndex: 'stateName',
        },
        {
            title: '创建时间',
            key: 'creationTime',
            dataIndex: 'creationTime',
        },
        {
            title: '操作',
            key: 'action',
            render: (_: any, item: any) => {
                const items = [
                    {
                        key: '1',
                        onClick: () => {
                            setOpenItem(item);
                            setVisible(true);
                        },
                        label: (
                            <span>
                                详情
                            </span>
                        ),
                    },
                    {
                        key: '2',
                        onClick: () => {
                            RemoveDeleteWikiDetails(item.id);
                        },
                        label: (
                            <span>
                                删除
                            </span>
                        ),
                    },]
                // 如果失败了则显示量化。
                if (item.state !== 0) {
                    items.push({
                        key: '3',
                        onClick: () => {
                            onRetryVectorDetail(item);
                        },
                        label: (
                            <span>
                                重试
                            </span>
                        ),
                    })
                }
                return (
                    <Dropdown
                        menu={{
                            items: items
                        }}
                        placement="bottomLeft"
                    >
                        <Button>
                            操作
                        </Button>
                    </Dropdown>
                )
            },
        },
    ]

    const [data, setData] = useState([] as any[]);
    const [visible, setVisible] = useState(false);
    const [openItem, setOpenItem] = useState({} as any);
    const [total, setTotal] = useState(0);
    const [input, setInput] = useState({
        keyword: '',
        page: 1,
        pageSize: 10,
        state: null as any | null
    });

    const items: MenuProps['items'] = [
        {
            key: '1',
            onClick: () => {
                onChagePath('upload')
            },
            label: (
                <span>
                    上传文件
                </span>
            ),
        },
        {
            key: '2',
            onClick: () => {
                onChagePath('upload-web')
            },
            label: (
                <span>
                    网页链接
                </span>
            ),
        },
        {
            key: '3',
            label: (
                <span>
                    自定义文本
                </span>
            ),
        },
    ];

    function handleDoubleClick(item: any) {
        item.isedit = true;
        // 修改更新
        setData([...data]);
    }

    async function RemoveDeleteWikiDetails(id: string) {
        try {
            await DeleteWikiDetails(id);
            message.success('删除成功');
            setInput({
                ...input,
                page: 1
            })
        } catch (error) {
            message.error('删除失败');
        }
    }

    async function onRetryVectorDetail(item: any) {
        try {

            await RetryVectorDetail(item.id);
            message.success('成功');
            loadingData()
        } catch (error) {

            message.error('失败');
        }

    }


    function handleTableChange(page: number, pageSize: number) {
        setInput({
            ...input,
            page: page,
            pageSize: pageSize,
        });
    }

    async function loadingData() {
        try {
            const result = await GetWikiDetailsList(id, input.keyword, input.page, input.pageSize, input.state);
            setData(result.result);
            setTotal(result.total);
        } catch (error) {

        }
    }

    useEffect(() => {
        loadingData();
    }, [id, input]);

    return (<>
        <header style={{
            padding: 16,
            fontSize: 20,
            fontWeight: 600
        }}>
            文件列表
            <div style={{
                float: 'right'
            }}>
                <Dropdown menu={{ items }} placement="bottomLeft">
                    <Button >上传文件</Button>
                </Dropdown>
            </div>
            <Select
                defaultValue={null}
                style={{
                    width: 120,
                    marginLeft: 16,
                    marginRight: 16,
                    float: 'right'
                }}
                onChange={(v: WikiQuantizationState | null) => {
                    setInput({
                        ...input,
                        state: v
                    })
                }}
                options={[
                    { value: null, label: '全部' },
                    { value: WikiQuantizationState.None, label: '处理中' },
                    { value: WikiQuantizationState.Accomplish, label: '完成' },
                    { value: WikiQuantizationState.Fail, label: '失败' },
                ]}
            />
        </header>
        <Table dataSource={data}
            pagination={{
                current: input.page,
                pageSize: input.pageSize,
                total: total,
                onChange: handleTableChange,
            }} scroll={{ y: 'calc(100vh - 240px)' }} columns={columns} style={{
                overflow: 'auto',
                padding: 16,
                borderRadius: 8,
            }} />
        <WikiDetailFile onClose={() => {
            setVisible(false);
        }} wikiDetail={openItem} visible={visible} />
    </>)
}