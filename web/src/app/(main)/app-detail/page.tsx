import { useEffect, useState } from 'react';
import AppDetailInfo from './feautres/AppDetailInfo';
import { Tabs } from 'antd';

import ReleaseApplication from './feautres/ReleaseApplication';
import { GetChatApplications } from '@/services/ChatApplicationService';
import Questions from './feautres/Questions';
import { useParams } from 'react-router-dom';


export default function AppDetail() {

    const query = location.search;

    // 解析query
    const id = query.split('=')[1];

    const [application, setApplication] = useState({} as any);

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
        if (id) {
            loadingApplication();
        }
    }, [id]);


    return (
        <Tabs
            tabPosition={'left'}
            style={{
                marginTop: 20,
                height: '100%',
            }}
            items={[
                {
                    key: '1',
                    label: '应用配置',
                    children: [
                        <AppDetailInfo key="1" value={application} />
                    ]
                },
                {
                    key: '2',
                    label: '管理提问',
                    children: [
                        <Questions key="2" id={application.id} />
                    ]
                },
                {
                    key: '3',
                    label: '发布应用',
                    children: [
                        <ReleaseApplication key="3" id={application.id} />
                    ]
                }
            ]}
        />
    );
}