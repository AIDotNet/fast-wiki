import { useState } from "react";
import { CreateChatDialogHistory } from "../services/ChatApplicationService";
import { generateRandomString } from "../utils/stringHelper";
import { fetchRaw } from "../utils/fetch";
import { ActionIcon, ChatInputActionBar, ChatInputArea, ChatSendButton } from "@lobehub/ui";
import { Flexbox } from 'react-layout-kit';
import { Eraser, Languages } from 'lucide-react';
import React from "react";

interface IFastChatInputProps {
    dialog: any;
    application: any;
    id?: string;
    history: any[];
    setHistory: any;
}

export default function FastChatInput({
    dialog,
    application,
    id,
    history,
    setHistory
}: IFastChatInputProps) {

    const [value, setValue] = useState<string>();
    const [loading, setLoading] = useState(false);
    const ref = React.useRef(null);

    async function sendChat() {
        // ref获取value
        const data = (ref as any).current?.resizableTextArea.textArea.value ?? value;
        if ((ref as any).current?.resizableTextArea.textArea.value) {
            (ref as any).current.resizableTextArea.textArea.value = '';
        }
        setValue('');

        if (!data || data === '') {
            return;
        }
        if (loading) {
            return;
        }
        setLoading(true);
        const chatlayout = document.getElementById('chat-layout');

        history.push({
            content: data,
            createAt: new Date().toISOString(),
            extra: {},
            id: generateRandomString(10),
            meta: {
                avatar: "https://blog-simple.oss-cn-shenzhen.aliyuncs.com/Avatar.jpg",
                title: "我",
            },
            role: 'user',
        })

        setHistory([...history]);

        let chat = {
            content: '',
            createAt: new Date().toISOString(),
            extra: {},
            id: generateRandomString(10),
            meta: {
                avatar: "https://blog-simple.oss-cn-shenzhen.aliyuncs.com/chatgpt.png",
                title: "AI助手",
            },
            role: 'assistant',
        };

        setHistory([...history, chat]);

        // 滚动到底部
        if (chatlayout) {
            chatlayout.scrollTop = chatlayout.scrollHeight;
        }

        let stream;

        if (id) {
            stream = await fetchRaw('/api/v1/ChatApplications/ChatShareCompletions', {
                chatDialogId: dialog.id,
                content: data,
                chatId: application.id,
                chatShareId: id,
            });
        } else {
            stream = await fetchRaw('/api/v1/ChatApplications/Completions', {
                chatDialogId: dialog.id,
                content: data,
                chatId: application.id
            });
        }

        for await (const c of stream) {
            if (c) {
                let content = c;
                // 先匹配删除前缀 [ 和后缀 ]
                if (content.startsWith('[')) {
                    content = c.slice(1);
                }

                if (content.startsWith(',')) {
                    content = content.slice(1);
                }

                if (content.endsWith(']')) {
                    // 删除最后一个字符
                    content = c.slice(0, c.length - 1);
                }
                if (content === '') {
                    return;
                }

                if (!content.startsWith('[')) {
                    content = "[" + content
                }

                if (!content.endsWith(']')) {
                    content = content + "]"
                }

                console.log(content);
                var obj = JSON.parse(content) as any[];

                obj.forEach((item) => {
                    chat.content += item.content;
                });

                setHistory([...history, chat]);
                // 滚动到底部
                if (chatlayout) {
                    chatlayout.scrollTop = chatlayout.scrollHeight;
                }
            }
        }

        await CreateChatDialogHistory({
            chatDialogId: dialog.id,
            content: data,
            current: true,
            type: 0
        })

        await CreateChatDialogHistory({
            chatDialogId: dialog.id,
            content: chat.content,
            current: false,
            type: 0
        })

        setLoading(false);
    }

    return (
        <div style={{ height: 300 }}>
            <Flexbox style={{ height: 300, position: 'relative', width: '100%' }}>
                <ChatInputArea
                    value={value}
                    onChange={(e: any) => {
                        setValue(e.target.value);
                    }}
                    placeholder="请输入您的消息"
                    onKeyUpCapture={(e: any) => {
                        if (e.key === 'Enter' && !e.shiftKey && value !== '') {
                            sendChat();
                        }
                    }}
                    bottomAddons={<ChatSendButton loading={loading} onSend={() => sendChat()} />}
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
    )
}