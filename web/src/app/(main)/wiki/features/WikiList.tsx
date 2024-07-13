import { Avatar, GridShowcase, LogoThree, SpotlightCard } from '@lobehub/ui';
import { useEffect, useState } from 'react';
import { Flexbox } from 'react-layout-kit';
import { message, Button, Pagination } from 'antd';

import { DeleteOutlined } from '@ant-design/icons';
import { DeleteWikis, GetWikisList } from '@/services/WikiService';
import { useNavigate } from 'react-router-dom';


interface IAppListProps {
    input: {
        page: number;
        pageSize: number;
    }
    setInput: (input: any) => void;
}

export function AppList(props: IAppListProps) {
    const navigate = useNavigate();

    const [input, setInput] = useState({
        keyword: '',
        page: 1,
        pageSize: 12
    });

    const [data, setData] = useState<any[]>([]);
    const [total, setTotal] = useState(0);

    const render = (item: any) => (
        <Flexbox align={'flex-start'} gap={8} horizontal style={{ padding: 16, height: 100 }}>

            <Avatar size={50} src={item.icon} style={{ flex: 'none' }} />
            <Flexbox onClick={() => {
                openWikiDetail(item.id);
            }}>
                <div style={{ fontSize: 15, fontWeight: 600 }}>{item.name}</div>
                <div style={{ opacity: 0.6 }}>
                    QA模型：
                    {item.model}
                </div>
            </Flexbox>
            <Button
                style={{
                    float: 'inline-end',
                    position: 'absolute',
                    right: 16,
                }}
                icon={<DeleteOutlined />}
                onClick={() => deleteWiki(item.id)}
            />
        </Flexbox>
    )

    async function deleteWiki(id: number) {
        await DeleteWikis(id);
        message.success('删除成功');
        setInput({
            ...input,
            page: 1
        });
        loadingData();
    }

    function openWikiDetail(id: number) {
        navigate(`/wiki-detail?id=${id}`);
    }

    async function loadingData() {
        try {
            const data = await GetWikisList(input.keyword, input.page, input.pageSize);
            setData(data.result);
            setTotal(data.total);
        } catch (error) {
            console.log(error);
            message.error('获取数据失败');
        }
    }

    useEffect(() => {
        loadingData();
    }, [props.input])

    return (<>
        <GridShowcase style={{ width: '100%' }}>
            <LogoThree size={180} style={{ marginTop: -64 }} />
            <div style={{ fontSize: 48, fontWeight: 600, marginTop: -16 }}>知识库列表</div>
        </GridShowcase>
        <SpotlightCard style={{
            margin: 16,
            borderRadius: 8,
            boxShadow: '0 0 8px 0 rgba(0,0,0,0.1)',
            overflow: 'auto',
            maxHeight: 'calc(100vh - 100px)',
            padding: 0,
        }} size={data.length} renderItem={render} items={data} >
        </SpotlightCard>
        <Pagination onChange={(page) => {
            props.setInput({
                ...props.input,
                page
            });
        }} total={total} />
    </>)
}