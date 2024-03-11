import { post } from '../utils/fetch';
import { config } from '../config';

/**
 * 账号登录
 * @param account 
 * @param password 
 * @returns 
 */
export const login = (account: string, password: string) => {
    return post(`/api/${config.VITE_VERSIONS}/Authorizes/Token?account=${account}&pass=${password}`);
};