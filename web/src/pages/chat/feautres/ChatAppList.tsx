import {
    ChatList,
    ActionsBar, ChatListProps
} from "@lobehub/ui";
import { DeleteDialogHistory, PutChatHistory } from "../../../services/ChatApplicationService";
import { message } from "antd";

interface IChatAppListProps {
    application: any;
    history: any[];
    setHistory: any;
}

export default function ChatAppList({
    application,
    history,
    setHistory
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
        <div id='chat-layout' style={{
            height: 'calc(100vh - 362px)',
            overflow: 'auto',
        }}>
            <ChatList
                onMessageChange={async (e: any, message) => {
                    if (e === 0) {
                        return
                    }
                    await PutChatHistory({
                        id: e,
                        content: message
                    })
                    // 修改history
                    history.forEach((item) => {
                        if (item.id === e) {
                            item.content = message;
                        }
                    });
                    setHistory([...history]);
                }}
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
                onActionsClick={ActionsClick}
                renderActions={ActionsBar}
                renderMessages={{
                    default: ({ id, editableContent }) => <div id={id}>{editableContent}</div>,
                }}
                style={{ width: '100%' }}
                {...control}
            />
        </div>
    )
}