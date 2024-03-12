import { ChatList, ChatListProps } from "@lobehub/ui";
import { DeleteDialogHistory, PutChatHistory } from "../../../services/ChatApplicationService";
import { message } from "antd";

import {
    ActionsBar,
} from '@lobehub/ui';

interface IChatAppListProps {
    application: any;
    history: any[];
    setHistory: any;
    id: string;
}

export default function ChatAppList({
    application,
    history,
    setHistory,
    id,
}: IChatAppListProps) {

    async function ActionsClick(e: any, item: any) {
        if (e.key === 'del') {
            await DeleteDialogHistory(item.id)
            message.success('删除成功');

            const index = history.findIndex((i) => i.id === item.id);
            history.splice(index, 1);
            setHistory([...history]);

        } else if (e.key === 'regenerate') {
            message.error('暂时并未支持重置!');
        }
    }

    const control: ChatListProps | any =
    {
        showTitle: false,
    }

    return (
        <div id='chat-layout' style={{ overflow: 'auto', flex: 1 }}>
            <ChatList
                data={(history.length === 0 || history === null) ? [{
                    content: application?.opener ?? "",
                    createAt: new Date().toISOString(),
                    extra: {},
                    id: 0,
                    meta: {
                        avatar: "https://blog-simple.oss-cn-shenzhen.aliyuncs.com/chatgpt.png",
                        title: "AI助手",
                    },
                    role: 'assistant',
                }] : history}
                renderActions={ActionsBar}
                onActionsClick={ActionsClick}
                onMessageChange={async (e: any, message) => {
                    if (e === 0) {
                        return
                    }
                    await PutChatHistory({
                        id: e,
                        content: message,
                        chatShareId: id
                    })
                    history.forEach((item) => {
                        if (item.id === e) {
                            item.content = message;
                        }
                    });
                    setHistory([...history]);
                }}
                renderMessages={{
                    default: ({ id, editableContent }) => <div id={id}>{editableContent}</div>,
                }}
                style={{ width: '100%' }}
                {...control}
            />
        </div>
    )
}