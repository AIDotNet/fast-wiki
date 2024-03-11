// 获取环境变量
export const config = {
  FAST_API_URL: import.meta.env.VITE_FAST_API_URL ?? "",
  NODE_ENV: import.meta.env.MODE,
  DEV: import.meta.env.DEV,
  VITE_VERSIONS: import.meta.env.VITE_VERSIONS ?? "v1",
};
