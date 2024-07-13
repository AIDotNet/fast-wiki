import { StateCreator } from 'zustand/vanilla';

import { enableClerk } from '@/const/auth';

import { UserStore } from '../../store';

export interface UserAuthAction {
  enableAuth: () => boolean;
  /**
   * universal logout method
   */
  logout: () => Promise<void>;
  /**
   * universal login method
   */
  openLogin: () => Promise<void>;
  openUserProfile: () => Promise<void>;
}

export const createAuthSlice: StateCreator<
  UserStore,
  [['zustand/devtools', never]],
  [],
  UserAuthAction
> = (set, get) => ({
  enableAuth: () => {
    return enableClerk || get()?.enabledNextAuth || false;
  },
  logout: async () => {
    if (enableClerk) {
      get().clerkSignOut?.({ redirectUrl: location.toString() });

      return;
    }
  },
  openLogin: async () => {
    if (enableClerk) {
      get().clerkSignIn?.({ fallbackRedirectUrl: location.toString() });

      return;
    }
  },

  openUserProfile: async () => {
    if (enableClerk) {
      get().clerkOpenUserProfile?.();

      return;
    }
  },
});
