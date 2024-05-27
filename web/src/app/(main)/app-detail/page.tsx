"use client"
import { useEffect, useState } from 'react';
import AppDetailInfo from './feautres/AppDetailInfo';
import { Button, Tabs } from 'antd';

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

    const [tab, setTab] = useState(1) as any;

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
            label: '应用配置',
            children: <AppDetailInfo value={application} />
        }, {
            key: 2,
            label: '管理提问',
            children: <Questions id={application.id} />
        }, {
            key: 3,
            label: '发布应用',
            children: <ReleaseApplication id={application.id} />
        },{
            key: 4,
            label: '高级编排'
        }] ;

        changeTab(tabs[0].key);

        //强制刷新
        setTabs([...tabs]);
    }

    function changeTab(key: any) {
        setTab(key);
        if(key === 4){
            location.href = '/app-flow?id=' + application.id;
        }
    }

    return (
        <Tabs
            style={{
                width: '100%',
                height: '100%',
                margin: '8px',
                overflowY: 'hidden',
            }}
            tabPosition={'left'}
            activeKey={tab}
            onChange={(key) => changeTab(key)}
            items={tabs}
        />
    );

}