import { postJson } from '../utils/fetch';

/**
 * 账号登录
 * @param account 
 * @param password 
 * @returns 
 */
export const login = (data: any) => {
    return postJson(`/api/v1/Authorizes/Token`, data);
};