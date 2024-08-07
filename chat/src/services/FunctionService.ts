import { del, get, post, postJson, putJson } from "../utils/fetch";

export function CreateFunction(input: any) {
    return postJson('/api/v1/Functions/Function', input)
}

export function PutFunction(input: any) {
    return putJson('/api/v1/Functions/Function', input)
}

export function DeleteFunction(id: string) {
    return del(`/api/v1/Functions/Function/${id}`)
}

export function GetFunctionList(input: any) {
    return get('/api/v1/Functions/FunctionList?page=' + input.page + '&pageSize=' + input.pageSize)
}

/**
 * 禁用/启用 FunctionCall
 * @param id 
 * @param enable 
 * @returns 
 */
export function EnableFunctionCall(id:number, enable:boolean) {
    return post(`/api/v1/Functions/EnableFunctionCall/${id}?enable=${enable}`)
}

/**
 * 获取 FunctionCall 列表
 * @returns 
 */
export function FunctionCallSelect() {
    return get('/api/v1/Functions/FunctionCallSelect')
}