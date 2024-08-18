import { memo, useEffect, useState } from 'react';
import { Tabs } from 'antd';
import WikiData from '../features/WikiData';
import UploadWikiFile from '../features/UploadWikiFile';
import SearchWikiDetail from '../features/SearchWikiDetail';
import WikiInfo from '../features/WikiInfo';
import UploadWikiWeb from '../features/UploadWikiWeb';

import { GetWikis } from '@/services/WikiService';
import VectorPage from '../features/Vector';
import UploadWikiData from '../features/UploadData';

export default memo(() => {
    const query = location.search;

    // 解析query
    const id = query.split('=')[1];

    const [wiki, setWiki] = useState({} as any);

    useEffect(() => {
        if (id) {
            loadingWiki();
        }
    }, [id]);

    async function loadingWiki() {
        GetWikis(id as string).then((wiki) => {
            setWiki(wiki);
        });
    }

    const items = [
        { key: 'data-item', label: '数据集', content: <WikiData onChagePath={key => changeTab(key)} id={id ?? ""} /> },
        { key: 'search', label: '搜索测试', content: <SearchWikiDetail onChagePath={key => changeTab(key)} id={id ?? ""} /> },
        { key: 'config', label: '配置', content: <WikiInfo id={id ?? ""} /> },
        { key: 'vector', label: '量化队列', content: <VectorPage id={id ?? ""} /> },
        { key: 'upload', label: '上传文件', content: <UploadWikiFile id={id ?? ""} onChagePath={key => changeTab(key)} /> },
        { key: 'upload-web', label: '上传网页', content: <UploadWikiWeb id={id ?? ""} onChagePath={key => changeTab(key)} /> },
        { key: 'upload-data', label: '上传数据', content: <UploadWikiData onChagePath={key => changeTab(key)} id={id ?? ""} /> },
    ];

    const [activeKey, setActiveKey] = useState(items[0].key);

    const changeTab = (key: string) => {
        setActiveKey(key);
    };

    return (
        <div style={{
            display: 'flex',
            height: '100%',
            width: '100%',
        }}>
            <Tabs
                tabPosition='top'
                style={{
                    width: '100%',
                    height: '100%',
                    padding: '20px',
                }}
                activeKey={activeKey}
                onChange={changeTab}
                items={items.map(item => ({
                    key: item.key,
                    label: item.label,
                    children: item.content
                }))}
            />
        </div>
    );
});