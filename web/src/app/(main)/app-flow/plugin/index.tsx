
import AIChat from "./ai-chat";
import Star from "./star";
import WikiSearch from "./wiki-search";
import FunctionCall from "./function-call";
import HttpRequest from "./httpRequest";
const nodeTypes = {
    star: Star,
    wikiSearch: WikiSearch,
    aiChat: AIChat,
    functionCall: FunctionCall,
    httpRequest: HttpRequest
}
export default nodeTypes;


function getList() {
    return [
        {
            label: "文本输出",
            children: [
                {
                    label: "AI 对话",
                    type: "aiChat",
                },
                {
                    label: "指定回复",
                    type: "assigned-reply"
                }
            ]
        }, {
            label: "功能调用",
            children: [
                {
                    label: "知识库搜索",
                    type: "wikiSearch"
                },
                {
                    label: "工具调用",
                    type: "functionCall"
                },
                {
                    label: "文本提取",
                    type: "textExtract"
                }
            ]
        },
        {
            label: "外部调用",
            children: [
                {
                    label: "HTTP 请求",
                    type: "httpRequest"
                }
            ]
        }
    ]
}

export {
    getList
}