import { Table, Button, Dropdown, MenuProps,message } from 'antd';
import { useEffect, useState } from 'react';
import { DeleteWikiDetails, GetWikiDetailsList } from '../../../services/WikiService';
import WikiDetailFile from './WikiDetailFile';

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
            render: (i: any, item: any) => (
                <>
                    <Button onClick={() => {
                        setVisible(true);
                        setOpenItem(item);
                    }}>查看详情</Button>
                    <Button type='primary' style={{
                        marginLeft: 8
                    }} onClick={() => {
                        RemoveDeleteWikiDetails(item.id)
                        setInput({
                            ...input,
                            page: 1
                        })
                    }}>删除</Button>
                </>
            ),
        },
    ]

    const [data, setData] = useState([] as any[]);
    const [visible, setVisible] = useState(false);
    const [openItem, setOpenItem] = useState({} as any);
    const [total, setTotal] = useState(0);
    const [input, setInput] = useState({
        keyword: '',
        page: 1,
        pageSize: 10
    } as any);

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


    async function RemoveDeleteWikiDetails(id: string) {
        try {
            await DeleteWikiDetails(id);
            message.success('删除成功');
        } catch (error) {
            message.error('删除失败');
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
            const result = await GetWikiDetailsList(id, input.keyword, input.page, input.pageSize);
            setData(result.result);
            setTotal(result.total);
        } catch (error) {

        }
    }

    useEffect(() => {
        loadingData();
    }, [id, input]);

    return (<>
        <div style={{
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
        </div>
        <Table dataSource={data}
            pagination={{
                current: input.page,
                pageSize: input.pageSize,
                total: total,
                onChange: handleTableChange,
            }} columns={columns} />
        <WikiDetailFile onClose={() => {
            setVisible(false);
        }} wikiDetail={openItem} visible={visible} />
    </>)
}