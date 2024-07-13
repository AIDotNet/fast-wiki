import { memo, useEffect, useState } from 'react';
import { Button, List } from 'antd';

import styled from 'styled-components';
import { Avatar, Tag, } from '@lobehub/ui';

import WikiData from '../features/WikiData';
import UploadWikiFile from '../features/UploadWikiFile';
import SearchWikiDetail from '../features/SearchWikiDetail';
import WikiInfo from '../features/WikiInfo';
import UploadWikiWeb from '../features/UploadWikiWeb';
import { CheckQuantizationState, GetWikis } from '@/services/WikiService';
const LeftTabs = styled.div`
    width: 190px;
    min-width: 190px;
    height: 100%;
    border-right: 1px solid #464545;
    display: flex;
    flex-direction: column;
`;


export default memo(() => {
    const query = location.search;

    // 解析query
    const id = query.split('=')[1];

    const [wiki, setWiki] = useState({} as any);

    const [tabs, setTabs] = useState([] as any[]);

    const [quantizationState, setQuantizationState] = useState([]) as any;

    const [tab, setTab] = useState() as any;

    useEffect(() => {
        if (id) {
            loadingWiki();

            // 写一个定时器，每1s刷新一次
            const timer = setInterval(() => {
                if (id) {
                    CheckQuantizationState(id as string)
                        .then((res) => {
                            setQuantizationState(res);
                        });
                }

            }, 1000);

            return () => {
                clearInterval(timer);
            }

        }
    }, [id]);

    async function loadingWiki() {
        GetWikis(id as string)
            .then((wiki) => {
                setWiki(wiki);
            });
    }

    useEffect(() => {
        loadingTabs();
    }, [wiki]);

    function loadingTabs() {
        const tabs = [{
            key: 1,
            label: '数据集'
        }, {
            key: 2,
            label: '搜索测试'
        }, {
            key: 3,
            label: '配置'
        }];

        changeTab(tabs[0]);

        //强制刷新
        setTabs([...tabs]);
    }

    function changeTab(key: any) {
        // 如果key是数字
        if (typeof key === 'number') {
            key = tabs.find(item => item.key === key);
        }

        setTab(key);
    }

    return (
        <>
            <LeftTabs>
                <div style={{
                    borderBottom: "1px solid #464545",
                    marginBottom: 16,
                    marginTop: 16,
                }}>
                    <Avatar size={50}
                        style={{
                            margin: '0 auto',
                            display: 'block',
                            marginBottom: 16
                        }}
                        src={wiki.icon} />
                    <div style={{
                        fontSize: 18,
                        overflow: 'hidden',
                        textAlign: 'center',
                        fontWeight: 600,
                        marginBottom: 16
                    }}>{wiki.name}</div>
                </div>
                <div style={{
                    padding: 8,
                    display: 'flex',
                    flexDirection: 'column',
                }}>
                    {tabs.map((item, index) => {
                        return <Button key={index} onClick={() => {
                            changeTab(item);
                        }} type={tab?.key === item.key ? 'default' : 'text'} style={{
                            marginBottom: 16,
                            width: '100%',

                        }} size='large'>{item.label}</Button>
                    })}
                </div>
                <div style={{
                    position: 'absolute',
                    bottom: 0,
                    padding: 8,
                    height: '20%',
                    overflow: 'auto',
                    width: 190,
                    justifyContent: 'space-around',
                }}>
                    <List
                        itemLayout='vertical'
                        locale={{ emptyText: '暂无量化任务' }}
                        dataSource={quantizationState}
                        renderItem={(item: any, index: number) => (
                            <List.Item style={{
                                height: 45,
                            }}>
                                <List.Item.Meta
                                    title={
                                        <>
                                            <span>{index}：</span>
                                            <span>{item.fileName}</span>
                                            <Tag style={{
                                                float: 'right'
                                            }}>量化中</Tag>
                                        </>
                                    }
                                />
                            </List.Item>
                        )}
                    />
                </div>
            </LeftTabs >
            <div style={{
                width: '100%',
                padding: 20,
                overflow: 'auto',

            }}>
                {
                    tab?.key === 1 && <WikiData onChagePath={key => changeTab(key)} id={id} />
                }
                {
                    tab?.key === 2 && <SearchWikiDetail onChagePath={key => changeTab(key)} id={id} />
                }
                {
                    tab?.key === 3 && <WikiInfo id={id} />
                }
                {
                    tab === 'upload' && <UploadWikiFile id={id} onChagePath={key => changeTab(key)} />
                }
                {
                    tab === 'upload-web' && <UploadWikiWeb id={id} onChagePath={key => changeTab(key)} />
                }
            </div>
        </>
    );
})