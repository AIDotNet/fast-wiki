
import { LobeUser } from '@/types/user';

export interface UserAuthState {
  clerkOpenUserProfile?: (props?: any) => void;

  clerkSession?: any;
  clerkSignIn?: (props?: any) => void;
  clerkSignOut?: any;
  clerkUser?: any;
  enabledNextAuth?: boolean;

  isLoaded?: boolean;
  isSignedIn?: boolean;
  nextSession?: any;
  nextUser?: any;
  user?: LobeUser;
}

export const initialAuthState: UserAuthState = {};
