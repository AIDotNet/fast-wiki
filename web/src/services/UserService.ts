import { del, get, post, postJson, put } from '../utils/fetch';

const prefix = `/api/v1/Users`;


/**
 * 修改密码
 * @param password 
 * @param newPassword 
 */
export const UpdateChangePassword = (password: string, newPassword: string) => {
    return post(`${prefix}/ChangePassword?password=${password}&newPassword=${newPassword}`, {})
}


/**
 * 获取用户列表
 * @param page 
 * @param pageSize 
 */
export const GetUsers = (keyword: string, page: number, pageSize: number) => {
    return get(`${prefix}/Users?page=${page}&pageSize=${pageSize}&keyword=${keyword}`)
}

/**
 * 删除用户
 * @param id 用户id
 * @returns 
 */
export const DeleteUser = (id: string) => {
    return del(`${prefix}/User/${id}`)
}

/**
 * 禁用用户
 * @param id 用户id
 * @param disable 是否禁用
 */
export const DisableUser = (id: string, disable: boolean) => {
    return post(`${prefix}/DisableUser/${id}?disable=${disable}`)
}

/**
 * 修改用户角色
 * @param id 用户id
 */
export const UpdateUserRole = (id: string, role: any) => {
    return put(`${prefix}/Role/${id}?role=${role}`, {})
}

/**
 * 注册用户
 * @param data 
 * @returns 
 */
export const Create = (data: any) => {
    return postJson(`${prefix}`, data)
}

/**
 * 获取用户信息
 * @returns
 */
export const GetUser = () => {
    return get(`${prefix}`)
}