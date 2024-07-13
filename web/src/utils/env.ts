export const isDev = process.env.NODE_ENV === 'development';

export const isOnServerSide = typeof window === 'undefined';

export const VITE_API_URL = (isDev ? import.meta.env.VITE_API_URL as string : '');