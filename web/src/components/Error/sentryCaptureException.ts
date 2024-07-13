import { appEnv } from '@/config/app';

export type ErrorType = Error & { digest?: string };

export const sentryCaptureException = async (error: Error & { digest?: string }) => {
  if (!appEnv.NEXT_PUBLIC_ENABLE_SENTRY) return;
  return error;
};
