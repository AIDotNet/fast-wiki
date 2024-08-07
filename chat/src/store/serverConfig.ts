import { create } from "zustand"

interface ServerConfigProps {
    isMobile: boolean
    setIsMobile: (isMobile: boolean) => void
}

export const useServerConfigStore = create<ServerConfigProps>((set) => ({
    isMobile: false,
    setIsMobile: (isMobile) => {
        set({ isMobile })
    }
}))