import { create } from 'zustand'

interface GlobalStore {
    showAgentSettings: boolean,
    inputHeight?: number,
    updatePreference: (height: number) => void,
    toggleConfig: (value?: boolean) => void
}



export const useGlobalStore = create<GlobalStore>((set) => ({
    showAgentSettings: false,
    inputHeight: 0,
    updatePreference: (height) => {
        set({ inputHeight: height
        })
    },
    toggleConfig: (value) => {
        if (value) {
            set({ showAgentSettings: value })
        } else {
            set((state) => ({ showAgentSettings: !state.showAgentSettings }))
        }
    }
}))
