import {fetch} from '../utils/fetch';

import { config } from '../config';

const prefix = `/api/${config.VITE_VERSIONS}/Storages`;

/**
 * 上传文件
 * @param file 
 * @returns 
 */
export function UploadFile(file:File){
    const formData = new FormData();
    formData.append('file', file);
    return fetch(`${prefix}/UploadFile`, {
        method: 'POST',
        body: formData
    });
}