import { del, get, post, postJson, putJson } from '../utils/fetch';

import { config } from '../config';
import { PaginatedListBase, WikiDto, WikiQuantizationState } from '../models/index.d';

const prefix = `/api/${config.VITE_VERSIONS}/Wikis`;


/**
 * 创建Wiki。
 * @param data - Wiki的数据。
 */
export function CreateWikis(data: any) {
    return postJson(`${prefix}`, data);
}

/**
 * 更新Wiki。
 * @param data - Wiki的数据。
 */
export function PutWikis(data: any) {
    return putJson(`${prefix}`, data);
}

/**
 * 根据Wiki的ID删除Wiki。
 */
export function DeleteWikis(id: number) {
    return del(`${prefix}/${id}`);
}

/**
 * 通过ID获取Wiki。
 * @param id - Wiki的ID。
 */
export function GetWikis(id: string) {
    return get(`${prefix}/${id}`);
}


/**
 * 获取Wiki列表。
 */
export function GetWikisList(keyword: string, page: number, pageSize: number): Promise<PaginatedListBase<WikiDto>> {
    return get(`${prefix}/WikiList?keyword=${keyword}&page=${page}&pageSize=${pageSize}`);
}

/**
 * 提交Wiki知识库详情
 */
export function CreateWikiDetails(data: any) {
    return postJson(`${prefix}/WikiDetails`, data);
}

/**
 * 获取Wiki知识库详情列表
 */
export function GetWikiDetailsList(wikiId: string, keyword: string, page: number, pageSize: number, state?: WikiQuantizationState | null) {
    return get(`${prefix}/WikiDetails?wikiId=${wikiId}&page=${page}&pageSize=${pageSize}&keyword=${keyword}` + (state === null ? `` : "&state=" + state));
}

/**
 * 提交Wiki知识库详情WebPage
 */
export function CreateWikiDetailWebPage(data: any) {
    return postJson(`${prefix}/WikiDetailWebPageInput`, data);
}

/**
 * 提交Wiki知识库详情Data
 * @param data Data
 * @returns
 * */
export function CreateWikiDetailData(data: any) {
    return postJson(`${prefix}/WikiDetailData`, data);
}

/**
 * 删除指定的Wiki知识库详情
 */
export function DeleteWikiDetails(id: string) {
    return del(`${prefix}/Details/${id}`)
}

/**
 * 获取指定知识库详情的向量数据
 */
export function GetWikiDetailVectorQuantity(wikiDetailId: string, page: number, pageSize: number) {
    return get(`${prefix}/WikiDetailVectorQuantity?wikiDetailId=${wikiDetailId}&page=${page}&pageSize=${pageSize}`)
}

/**
 * 删除指定的Wiki知识库详情向量数据
 */
export function DelDetailVectoryQuantity(id: string) {
    return del(`${prefix}/DetailVectorQuantity?documentId=${id}`)
}

/**
 * 获取搜索指定知识库的向量数据
 */
export function GetSearchVectorQuantity(wikiId: string, search: string, minRelevance: number) {
    return get(`${prefix}/SearchVectorQuantity?wikiId=${wikiId}&search=${search}&minRelevance=${minRelevance}`)
}

/**
 * 删除指定的Wiki知识库详情向量数据
 */
export function DelDetailsVector(id: string) {
    // 将id进行url编码
    id = encodeURIComponent(id);

    return del(`${prefix}/DetailsVector/${id}`)
}

/**
 * 重试量化
 * @param id 
 * @returns 
 */
export function RetryVectorDetail(id:number){
    return post(`${prefix}/RetryVectorDetail/${id}`)
}