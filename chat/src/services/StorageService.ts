import { fetch } from '../utils/fetch';


const prefix = `/api/v1/Storages`;

/**
 * 上传文件
 * @param file 
 * @returns 
 */
export function UploadFile(file: File) {
    const formData = new FormData();
    formData.append('file', file);
    return fetch(`${prefix}/UploadFile`, {
        method: 'POST',
        body: formData
    });
}