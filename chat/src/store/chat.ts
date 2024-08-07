import { create } from "zustand"


interface useChatStoreProps {
    id: string
    isHasMessageLoading: boolean
    isAIGenerating: boolean

    stopGenerateMessage: () => void
    clearMessage: () => void
    activeTopicId: string
    useFetchMessages: any
    isFirstLoading: boolean
    isCurrentChatLoaded: boolean
    data: any[]
    sendMessage: (message: any) => void
    inputMessage: string
    updateInputMessage: (message: string) => void
    // loading, value, onInput, onStop
    loading: boolean
    value: any
    onInput: () => void
    onStop: () => void
    isMessageLoading: boolean
    generating: boolean
    editing: boolean
    toggleMessageEditing: (id: string, edit: boolean) => void
    updateMessageContent: (id: string, value: any) => void
}

export const useChatStore = create<useChatStoreProps>((set) => ({
    id: '',
    generating: false,
    editing: false,
    // @ts-ignore
    toggleMessageEditing: (id, edit) => {
        set((state) => {
            return {
                ...state,
                editing: edit
            }
        })
    },
    // @ts-ignore
    updateMessageContent: (id, value) => {
        set((state) => {
            return {
                ...state,
                value: value
            }
        })
    },
    isMessageLoading: false,
    isHasMessageLoading: false,
    isAIGenerating: false,
    stopGenerateMessage: () => {
        set((state) => {
            return {
                ...state,
                isHasMessageLoading: false
            }
        })
    },
    activeTopicId: '',
    clearMessage: () => {
        set((state) => {
            return {
                ...state,
                data: []
            }
        })
    },
    useFetchMessages: '',
    isFirstLoading: false,
    isCurrentChatLoaded: false,
    data: [],
    sendMessage: (message) => {
        set((state) => {
            return {
                ...state,
                data: [...state.data, message],
            }
        })
    },
    inputMessage: '',
    updateInputMessage: (message) => {
        set((state) => {
            return {
                ...state,
                inputMessage: message,
            }
        })
    },
    loading: false,
    value: '',
    onInput: () => {

    },
    onStop: () => {

    }
}))