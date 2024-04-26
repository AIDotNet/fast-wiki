import { memo, useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import AppDetailInfo from '../feautres/AppDetailInfo';
import { Button } from 'antd';

import styled from 'styled-components';
import { GetChatApplications } from '../../../services/ChatApplicationService';
import ReleaseApplication from '../feautres/ReleaseApplication';

const LeftTabs = styled.div`
    width: 140px;
    height: 100%;
    padding: 20px;
    /* 右边需要有一个分割线 */
    border-right: 1px solid #464545;
`;


export default memo(() => {
    const { id } = useParams<{ id: string }>();
    if (id === undefined) return (<div>id is undefined</div>)

    const [application, setApplication] = useState({} as any);

    const [tabs, setTabs] = useState([] as any[]);

    const [tab, setTab] = useState() as any;

    useEffect(() => {
        loadingApplication();
    }, [id]);

    async function loadingApplication() {
        GetChatApplications(id as string)
            .then((application) => {
                setApplication(application);
            });
    }

    useEffect(() => {
        loadingTabs();
    }, [application]);

    function loadingTabs() {
        const tabs = [{
            key: 1,
            label: '应用配置'
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
                    tab?.key === 1 ? <AppDetailInfo value={application} /> : (
                        tab?.key === 2 ? <></> :
                            (
                                tab?.key === 3 ? <ReleaseApplication id={application.id} /> : null
                            )
                    )
                }
            </div>
        </>
    );
})