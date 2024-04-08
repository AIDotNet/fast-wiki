import { DraggablePanel, Tooltip, } from "@lobehub/ui";
import { Select } from 'antd';
import { useEffect, useState } from "react";
import { GetChatApplicationsList, GetChatDialogHistory } from "../../../services/ChatApplicationService";
import Divider from "@lobehub/ui/es/Form/components/FormDivider";
import { Button, message } from 'antd'
import { useNavigate } from "react-router-dom";
import styled from "styled-components";
import { DeleteOutlined } from '@ant-design/icons';
import CreateDialog from "../feautres/CreateDialog";


import { Flexbox } from 'react-layout-kit';
import FastChatInput from "../../../components/FastChatInput";
import ChatAppList from "../../../components/ChatAppList";

import { IndexedDBWrapper } from "../../../utils/IndexedDBWrapper";

const sessionName = 'fast-wiki-dialog';
const sessionVersion = 1;
const sessionStoreName = 'sessions';
const sessionDB = new IndexedDBWrapper(sessionName, sessionVersion, sessionStoreName);
sessionDB.open();

const fileName = 'fast-wiki-file';
const fileVersion = 1;
const fileStoreName = 'files';
const fileDB = new IndexedDBWrapper(fileName, fileVersion, fileStoreName);
fileDB.open();



const DialogList = styled.div`
    margin-top: 8px;
    padding: 8px;
    overflow: auto;
    height: calc(100vh - 110px);
`;

const DialogItem = styled.div`
    padding: 8px;
    border: 1px solid #d9d9d9;
    border-radius: 8px;
    cursor: pointer;
    margin-bottom: 8px;
    transition: border-color 0.3s linear;
    &:hover {
        border-color: #1890ff;
    }

    // 当组件被选中时修改样式
    &.selected {
        border-color: #1890ff;
    }
`;

export default function DesktopLayout() {
    const navigate = useNavigate();
    const [applications, setApplications] = useState([] as any[]);
    const [application, setApplication] = useState(null as any);
    const [dialogs, setDialogs] = useState([] as any[]);
    const [createDialogVisible, setCreateDialogVisible] = useState(false);
    const [dialog, setDialog] = useState({} as any);
    const [history, setHistory] = useState([] as any[]);
    const [input] = useState({
        page: 1,
        pageSize: 5
    });

    async function loadingApplications() {
        try {
            const result = await GetChatApplicationsList(1, 1000);
            setApplications(result.result);
            if (result.total === 0) {
                message.error('您还没有应用，请先创建应用');
                // 等待1秒后跳转
                setTimeout(() => {
                    navigate('/app');
                }, 1000);
                return;
            }
            setApplication(result.result[0]);
        } catch (error) {

        }
    }

    async function loadingDialogs() {
        try {
            if (application.id === undefined) {
                return;
            }

            const result = (await sessionDB.getAll()) as any[];
            setDialogs(result);
            if (result.length === 0) {
                sessionDB.add({
                    name: '默认对话',
                    description: '默认创建的对话',
                    applicationId: application.id,
                    id: Math.random().toString(36).slice(-8),
                    type: 0
                })
                loadingDialogs();
                return;
            }
            setDialog(result[0]);
        } catch (error) {

        }
    }

    async function LoadingSession() {
        try {
            if (dialog.id === undefined) {
                return;
            }
            const result = await GetChatDialogHistory(dialog.id, input.page, input.pageSize);

            const history = result.result.map((item: any) => {
                return {
                    content: item.content,
                    createAt: item.createAt,
                    extra: {
                        referenceFile: item.referenceFile,
                    },
                    id: item.id,
                    meta: {
                        avatar: item.current ? "https://blog-simple.oss-cn-shenzhen.aliyuncs.com/Avatar.jpg" : "https://blog-simple.oss-cn-shenzhen.aliyuncs.com/chatgpt.png",
                        title: item.current ? "我" : "AI助手",
                    },
                    role: item.current ? 'user' : 'assistant',
                };
            });

            setHistory(history);

            // 等待1秒后滚动到底部
            setTimeout(() => {
                const chatlayout = document.getElementById('chat-layout');
                if (chatlayout) {
                    chatlayout.scrollTop = chatlayout.scrollHeight;
                }
            }, 1000);
        } catch (error) {

        }
    }

    useEffect(() => {
        if (application) {
            loadingDialogs();

        }
    }, [application]);

    useEffect(() => {
        if (dialog) {
            LoadingSession();
        }
    }, [dialog, input]);

    useEffect(() => {
        loadingApplications();
    }, []);

    function handleChange(id: any) {
        const app = applications.find(app => app.id === id);
        if (app) {
            setApplication(app);
        }
    }

    async function deleteDialog(id: string) {
        await sessionDB.deleteStr(id);
        loadingDialogs();
    }

    return <>
        <DraggablePanel
            mode="fixed"
            placement="left"
            showHandlerWhenUnexpand={true}
            resize={false}
            pin={true}
            minWidth={0}
        >
            <div
                style={{
                    padding: 8,
                }}>

                <div style={{
                    fontSize: 20,
                    fontWeight: 600,
                    marginBottom: 16
                }}>
                    请选择您的应用
                </div>
                <Select
                    title="请选择您的应用"
                    style={{
                        width: '100%',
                    }}
                    onChange={handleChange}
                    defaultValue={application?.id}
                    value={application?.id}
                    options={applications.map((item) => {
                        return { label: item.name, value: item.id }
                    })}
                />
            </div>
            <Divider />
            <DialogList>
                {
                    dialogs?.map((item: any) => {
                        return <DialogItem
                            key={item.id}
                            // 当组件被选中时修改样式
                            className={dialog?.id === item.id ? 'selected' : ''}
                            onClick={() => {
                                setDialog(item);
                            }}>
                            <Tooltip title={item.description}>
                                {item.name}
                            </Tooltip>
                            <Button
                                style={{
                                    float: 'inline-end',
                                }}
                                size='small'
                                icon={<DeleteOutlined />}
                                onClick={() => deleteDialog(item.id)}
                            />
                        </DialogItem>
                    })
                }
                <Button onClick={() => setCreateDialogVisible(true)} style={{
                    marginTop: 8
                }} block>新建对话</Button>

            </DialogList>
        </DraggablePanel>
        <Flexbox style={{ height: '100vh', position: 'relative', width: '100%' }}>
            <div style={{ height: 60 }}>
                <div style={{
                    fontSize: 20,
                    fontWeight: 600,
                    textAlign: 'left',
                    padding: 15
                }}>
                    {dialog.name}
                </div>
            </div>
            <Divider />

            <Flexbox style={{ overflow: 'auto', flex: 1 }}>
                <ChatAppList setHistory={(v: any) => {
                    setHistory([...v]);
                }} history={history} application={application} />
            </Flexbox>
            <DraggablePanel style={{
                height: '100%'
            }} maxHeight={600} minHeight={180} placement='bottom'>
                <FastChatInput dialog={dialog} application={application} setHistory={(v: any) => {
                    setHistory(v);
                }
                } history={history} />
            </DraggablePanel>
            <CreateDialog db={sessionDB} visible={createDialogVisible} id={application?.id} type={0} onClose={() => {
                setCreateDialogVisible(false);
                loadingDialogs();
            }} onSucess={() => {
                setCreateDialogVisible(false);
                loadingDialogs();
            }} />
        </Flexbox>
    </>
}