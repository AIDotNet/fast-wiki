import { ChatList, DraggablePanel, } from "@lobehub/ui";
import { Select } from 'antd';
import { useEffect, useState } from "react";
import { CreateChatDialog, CreateChatDialogHistory, GetChatApplicationsList, GetChatDialog, GetChatDialogHistory, GetChatShareApplication } from "../../../services/ChatApplicationService";
import Divider from "@lobehub/ui/es/Form/components/FormDivider";
import { Button, message } from 'antd'
import { useNavigate } from "react-router-dom";
import styled from "styled-components";
import {
    ActionIcon,
    ChatInputActionBar,
    ChatInputArea,
    ChatSendButton,
} from '@lobehub/ui';
import {
    ActionsBar,
    ChatListProps,
} from '@lobehub/ui';

import { Eraser, Languages } from 'lucide-react';
import { Flexbox } from 'react-layout-kit';
import { fetchRaw } from "../../../utils/fetch";
import CreateDialog from "../feautres/CreateDialog";
import { generateRandomString } from "../../../utils/stringHelper";

const DialogList = styled.div`
    margin-top: 8px;
    padding: 8px;
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
    const id = new URLSearchParams(window.location.search).get('id');
    if (!id) {
        return (<div style={{
            textAlign: 'center',
            fontSize: "20px"
        }}>
            请提供分享Id
        </div>)
    }

    /**
     * 获取游客id
     */
    let guestId = localStorage.getItem('ChatShare')
    if (!guestId) {
        guestId = generateRandomString(10)
        localStorage.setItem('ChatShare', guestId)
    }
    const navigate = useNavigate();
    const [applications, setApplications] = useState([] as any[]);
    const [application, setApplication] = useState(null as any);
    const [dialogs, setDialogs] = useState([] as any[]);
    const [createDialogVisible, setCreateDialogVisible] = useState(false);
    const [dialog, setDialog] = useState({} as any);
    const [history, setHistory] = useState([] as any[]);
    const [value, setValue] = useState('' as string);
    const [input] = useState({
        page: 1,
        pageSize: 5
    });

    useEffect(() => {
        loadingApplication();
    }, [id])


    async function loadingApplication() {
        const app = await GetChatShareApplication(id as any);
        setApplication(app)
    }


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

            const result = (await GetChatDialog(application.id, true)) as any[];
            setDialogs(result);
            if (result.length === 0) {
                await AddChatDialog({
                    name: '默认对话',
                    description: '默认创建的对话',
                    applicationId: application.id,
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
            const result = await GetChatDialogHistory(dialog.id, input.page, input.pageSize);

            const history = result.result.map((item: any) => {
                return {
                    content: item.content,
                    createAt: item.createAt,
                    extra: {},
                    id: item.id,
                    meta: {
                        avatar: item.current ? "https://blog-simple.oss-cn-shenzhen.aliyuncs.com/Avatar.jpg" : "https://blog-simple.oss-cn-shenzhen.aliyuncs.com/chatgpt.png",
                        title: item.current ? "我" : "AI助手",
                    },
                    role: item.current ? 'user' : 'assistant',
                };
            });

            setHistory(history);
        } catch (error) {

        }
    }

    async function AddChatDialog(data: any) {
        try {
            await CreateChatDialog(data)
        } catch (error) {
            message.error('创建失败');
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

    function handleChange(value: any) {
        console.log(`selected ${value}`);
    }

    const control: ChatListProps | any =
    {
        showTitle: false,
    }

    async function sendChat() {
        const v = value;
        setValue('');

        history.push({
            content: v,
            createAt: new Date().toISOString(),
            extra: {},
            id: new Date().getTime(),
            meta: {
                avatar: "https://blog-simple.oss-cn-shenzhen.aliyuncs.com/Avatar.jpg",
                title: "我",
            },
            role: 'user',
        })

        setHistory([...history]);

        const stream = await fetchRaw('/api/v1/ChatApplications/Completions', {
            chatDialogId: dialog.id,
            content: v,
            chatId: application.id
        });

        let chat = {
            content: '',
            createAt: new Date().toISOString(),
            extra: {},
            id: new Date().getTime(),
            meta: {
                avatar: "https://blog-simple.oss-cn-shenzhen.aliyuncs.com/chatgpt.png",
                title: "AI助手",
            },
            role: 'assistant',
        };

        setHistory([...history, chat]);

        for await (const c of stream) {
            if (c) {
                let content = c;
                // 先匹配删除前缀 [ 和后缀 ]
                if (content.startsWith('[') || content.startsWith(',')) {
                    // 删除第一个字符
                    content = c.slice(1);
                }
                if (content.endsWith(']')) {
                    // 删除最后一个字符
                    content = c.slice(0, c.length - 1);
                }
                if (content === '') {
                    return;
                }

                if (content.startsWith(',') === true) {
                    content = content.slice(1);
                }

                content = "[" + content + "]";
                console.log(content);

                var obj = JSON.parse(content) as any[];

                obj.forEach((item) => {
                    chat.content += item.content;
                });

                setHistory([...history, chat]);
            }
        }

        await CreateChatDialogHistory({
            chatDialogId: dialog.id,
            content: v,
            current: true,
            type: 0
        })


        await CreateChatDialogHistory({
            chatDialogId: dialog.id,
            content: chat.content,
            current: false,
            type: 0
        })
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
                            }}>{item.name}</DialogItem>
                    })
                }
                <Button onClick={() => setCreateDialogVisible(true)} style={{
                    marginTop: 8
                }} block>新建对话</Button>

            </DialogList>
        </DraggablePanel>
        <div style={{
            height: '100vh',
            width: '100%',
        }}>
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
            <div id='chat-layout' style={{ height: 'calc(100vh - 362px)' }}>

                <ChatList

                    data={history}
                    renderActions={ActionsBar}
                    renderMessages={{
                        default: ({ id, editableContent }) => <div id={id}>{editableContent}</div>,
                    }}
                    style={{ width: '100%' }}
                    {...control}
                />
            </div>
            <Divider />
            <div style={{ height: 300 }}>
                <Flexbox style={{ height: 300, position: 'relative' }}>
                    <ChatInputArea
                        value={value}
                        onChange={(e) => {
                            setValue(e.target.value);
                        }}
                        placeholder="请输入您的消息"
                        bottomAddons={<ChatSendButton onSend={() => sendChat()} />}
                        topAddons={
                            <ChatInputActionBar
                                leftAddons={
                                    <>
                                        <ActionIcon icon={Languages} color={undefined} fill={undefined} fillOpacity={undefined} fillRule={undefined} focusable={undefined} />
                                        <ActionIcon onClick={() => {
                                            setValue('');
                                        }} icon={Eraser} color={undefined} fill={undefined} fillOpacity={undefined} fillRule={undefined} focusable={undefined} />
                                    </>
                                }
                            />
                        }
                    />
                </Flexbox>
            </div>
            <CreateDialog visible={createDialogVisible} id={application?.id} type={0} onClose={() => {
                setCreateDialogVisible(false);
                loadingDialogs();
            }} onSucess={() => {
                setCreateDialogVisible(false);
                loadingDialogs();
            }} />
        </div>
    </>
}