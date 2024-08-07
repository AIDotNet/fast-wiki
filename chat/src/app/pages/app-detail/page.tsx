import { useEffect, useState } from 'react';
import AppDetailInfo from './feautres/AppDetailInfo';
import { Tabs } from 'antd';

import ReleaseApplication from './feautres/ReleaseApplication';
import { GetChatApplications } from '@/services/ChatApplicationService';
import Questions from './feautres/Questions';
import { useParams } from 'react-router-dom';


export default function AppDetail() {
    // 解析 /app/:id
    const { id } = useParams();

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

    return (
        <Tabs
            tabPosition={'left'}
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