import create from 'zustand'

export type Theme = 'light' | 'dark' | 'auto'

interface UserStore {
    theme: Theme
    canUpload: boolean
    setTheme: (theme: Theme) => void
    hideSettingsMoveGuide: boolean
    setHideSettingsMoveGuide: (hideSettingsMoveGuide: boolean) => void
}

export const useUserStore = create<UserStore>((set) => ({
    theme: localStorage.getItem('theme') as Theme || 'light',
    setTheme: (theme) => set({ theme }),
    hideSettingsMoveGuide: false,
    canUpload: false,
    setHideSettingsMoveGuide: (hideSettingsMoveGuide) => set({ hideSettingsMoveGuide })
}))