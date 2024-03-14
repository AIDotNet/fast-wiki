import { fetch, post } from '../utils/fetch';

import { config } from '../config';

const prefix = `/api/${config.VITE_VERSIONS}/Users`;


/**
 * 修改密码
 * @param password 
 * @param newPassword 
 */
export const UpdateChangePassword = (password: string, newPassword: string) => {
    return post(`${prefix}/ChangePassword?password=${password}&newPassword=${newPassword}`, {})
}