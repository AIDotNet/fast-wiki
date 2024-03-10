import { memo, useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import { Button } from 'antd';

import styled from 'styled-components';
import { GetWikis } from '../../../services/WikiService';
import { Avatar } from '@lobehub/ui';
import WikiData from '../features/WikiData';
import UploadWikiFile from '../features/UploadWikiFile';
import SearchWikiDetail from '../features/SearchWikiDetail';
const LeftTabs = styled.div`
    width: 190px;
    min-width: 190px;
    height: 100%;
    /* 右边需要有一个分割线 */
    border-right: 1px solid #464545;
    display: flex;
    flex-direction: column;

`;


export default memo(() => {
    const { id } = useParams<{ id: string }>();
    if (id === undefined) return (<div>id is undefined</div>)

    const [wiki, setWiki] = useState({} as any);

    const [tabs, setTabs] = useState([] as any[]);

    const [tab, setTab] = useState() as any;

    useEffect(() => {
        loadingWiki();
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
            </LeftTabs >
            <div style={{
                width: '100%',
                padding: 20

            }}>
                {
                    tab?.key === 1 && <WikiData onChagePath={key => changeTab(key)} id={id} />
                }
                {
                    tab?.key === 2 && <SearchWikiDetail onChagePath={key => changeTab(key)} id={id} />
                }
                {
                    tab?.key === 3 && null
                }
                {
                    tab?.key === 3 && null
                }
                {
                    tab === 'upload' && <UploadWikiFile id={id} onChagePath={key => changeTab(key)} />
                }
            </div>
        </>
    );
})