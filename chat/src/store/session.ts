// 
//   const [init, isInbox, title, description, avatar, backgroundColor] = useSessionStore((s) => [
//     sessionSelectors.isSomeSessionActive(s),
//     sessionSelectors.isInboxSession(s),
//     sessionMetaSelectors.currentAgentTitle(s),
//     sessionMetaSelectors.currentAgentDescription(s),
//     sessionMetaSelectors.currentAgentAvatar(s),
//     sessionMetaSelectors.currentAgentBackgroundColor(s),
//   ]);

import { create } from "zustand"

interface SessionStore {
    init: boolean
    isInbox: boolean
    title: string
    description: string
    avatar: string
    backgroundColor: string
    sessionId: string
}

export const useSessionStore = create<SessionStore>(() => ({
    init: false,
    isInbox: false,
    title: '默认标题',
    description: '',
    avatar: '🤖',
    backgroundColor: '',
    sessionId: ''
}))