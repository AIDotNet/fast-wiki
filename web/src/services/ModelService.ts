import { get, postJson } from '../utils/fetch';

import { config } from '../config';

const prefix = `/api/${config.VITE_VERSIONS}/Models`;

/**
 * 获取模块列表
 * @returns 
 */
export function GetChatTypes() {
    return get(`${prefix}/ChatTypes`);
}

/**
 * 获取模块列表
 * @param keyword 
 * @param page 
 * @param pageSize 
 * @returns 
 */
export function GetModelList(keyword: string, page: number, pageSize: number) {
    return get(`${prefix}/ModelList?keyword=${keyword}&page=${page}&pageSize=${pageSize}`);
}

/**
 * 创建模型
 * @param data 
 */
export function CreateFastModel(data: any) {
    return postJson(`${prefix}/FastModel`, data);
}

/**
 * 删除模型
 * @param id 
 */
export function DeleteFastModel(id: string) {
    return get(`${prefix}/FastModel/${id}`);
}