"use client"
import { useEffect, useState } from 'react';
import AppDetailInfo from './feautres/AppDetailInfo';
import { Button } from 'antd';

import styled from 'styled-components';
import ReleaseApplication from './feautres/ReleaseApplication';
import { GetChatApplications } from '@/services/ChatApplicationService';
import Questions from './feautres/Questions';

const LeftTabs = styled.div`
    width: 140px;
    height: 100%;
    padding: 20px;
    /* 右边需要有一个分割线 */
    border-right: 1px solid #464545;
`;


export default function AppDetail() {


    const query = location.search;

    // 解析query
    const id = query.split('=')[1];

    const [application, setApplication] = useState({} as any);

    const [tabs, setTabs] = useState([] as any[]);

    const [tab, setTab] = useState() as any;

    useEffect(() => {
        if (id) {
            loadingApplication();
        }
    }, [id]);

    async function loadingApplication() {
        if (id) {
            GetChatApplications(id as string)
                .then((application) => {
                    setApplication(application);
                });
        }
    }

    useEffect(() => {
        loadingTabs();
    }, [application]);

    function loadingTabs() {
        const tabs = [{
            key: 1,
            label: '应用配置'
        }, {
            key: 2,
            label: '管理提问'
        }, {
            key: 3,
            label: '发布应用'
        }];

        changeTab(tabs[0]);

        //强制刷新
        setTabs([...tabs]);
    }

    function changeTab(key: any) {
        setTab(key);
    }

    return (
        <>
            <div style={{ display: 'flex' }}>
                <LeftTabs>
                    {tabs.map((item, index) => {
                        return <Button key={index} onClick={() => {
                            changeTab(item);
                        }} type={tab?.key === item.key ? 'default' : 'text'} style={{
                            marginBottom: 16,
                            width: '100%'
                        }} size='large'>{item.label}</Button>
                    })}
                </LeftTabs>
            </div>
            <div style={{
                width: '100%',
                padding: 20

            }}>
                {
                    tab?.key === 1 && <AppDetailInfo value={application} />
                }
                {
                    tab?.key === 2 && <Questions id={application.id} ></Questions>
                }
                {
                    tab?.key === 3 && <ReleaseApplication id={application.id} />
                }
            </div>
        </>
    );

}