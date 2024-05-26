import { t } from 'i18next';

import { LOBE_CHAT_OBSERVATION_ID, LOBE_CHAT_TRACE_ID,FAST_API_URL } from '@/const/trace';
import { ErrorResponse, ErrorType } from '@/types/fetch';
import { ChatMessageError } from '@/types/message';
import { message } from 'antd';

export const getMessageError = async (response: Response) => {
  let chatMessageError: ChatMessageError;

  // 尝试取一波业务错误语义
  try {
    const data = (await response.json()) as ErrorResponse;
    chatMessageError = {
      body: data.body,
      message: t(`response.${data.errorType}` as any, { ns: 'error' }),
      type: data.errorType,
    };
  } catch {
    // 如果无法正常返回，说明是常规报错
    chatMessageError = {
      message: t(`response.${response.status}` as any, { ns: 'error' }),
      type: response.status as ErrorType,
    };
  }

  return chatMessageError;
};

type SSEFinishType = 'done' | 'error' | 'abort';

export type OnFinishHandler = (
  text: string,
  context: {
    observationId?: string | null;
    traceId?: string | null;
    type?: SSEFinishType;
  },
) => Promise<void>;

export interface FetchSSEOptions {
  onAbort?: (text: string) => Promise<void>;
  onErrorHandle?: (error: ChatMessageError) => void;
  onFinish?: OnFinishHandler;
  onMessageHandle?: (text: string) => void;
}

/**
 * Fetch data using stream method
 */
export const fetchSSE = async (fetchFn: () => Promise<Response>, options: FetchSSEOptions = {}) => {
  const response = await fetchFn();

  // 如果不 ok 说明有请求错误
  if (!response.ok) {
    const chatMessageError = await getMessageError(response);

    options.onErrorHandle?.(chatMessageError);
    return;
  }

  const returnRes = response.clone();

  const data = response.body;

  if (!data) return;
  let output = '';
  const reader = data.getReader();
  const decoder = new TextDecoder();

  let done = false;
  let finishedType: SSEFinishType = 'done';

  while (!done) {
    try {
      const { value, done: doneReading } = await reader.read();
      done = doneReading;
      const chunkValue = decoder.decode(value, { stream: true });

      output += chunkValue;
      options.onMessageHandle?.(chunkValue);
    } catch (error) {
      done = true;

      if ((error as TypeError).name === 'AbortError') {
        finishedType = 'abort';
        options?.onAbort?.(output);
      } else {
        finishedType = 'error';
        console.error(error);
      }
    }
  }

  const traceId = response.headers.get(LOBE_CHAT_TRACE_ID);
  const observationId = response.headers.get(LOBE_CHAT_OBSERVATION_ID);
  await options?.onFinish?.(output, { observationId, traceId, type: finishedType });

  return returnRes;
};



export async function fetch(url: string, options: any) {
  const token = localStorage.getItem('token');
  const headers = {
    ...options.headers,
    Authorization: `Bearer ${token}`
  };
  try {
    url = `${FAST_API_URL}${url}`.replace(/([^:]\/)\/+/g, '$1');
    const response = await window.fetch(url, { ...options, headers });
    if (response.status >= 200 && response.status < 300) {
      const data = await response.text();
      if (data === '' || data === null) {
        return null;
      }
      return JSON.parse(data);
    }

    if (response.status === 204) {
      return null;
    }

    // 如果是401，跳转到登录页
    if (response.status === 401) {
      if (typeof window === 'undefined') return;
      window.location.href = '/auth-login';
    }

    if (response.status === 400) {
      // 读取body内容
      const data = await response.json();
      message.error(data.message);
      throw new Error(data);
    } else if (response.status === 404) {
      message.error('请求的资源不存在');
      const data = await response.json();
      message.error(data.message);
      throw new Error(data);
    } else if (response.status === 500) {
      message.error('服务器错误');
      const data = await response.json();
      message.error(data.message);
      throw new Error(data);
    }

    const error = new Error();
    throw error;
  } catch (error: any) {
    throw error;
  }
}

export async function fetchRaw(url: string, data:any) {
  const token = localStorage.getItem('token');
  const headers = {
    Authorization: `Bearer ${token}`,
    'Content-Type': 'application/json',
    
  };
  try {
    // 拼接baseUrl并且处理/重复问题
    url = `${FAST_API_URL}${url}`.replace(/([^:]\/)\/+/g, '$1');
    const response = await window.fetch(url, { 
      headers,
      method: 'POST',
      body: JSON.stringify(data)
     });

    if (response.ok === false) {
      const reader = await response.text();
      throw new Error(reader);
    }

    const reader = response.body!.getReader();
    return {
      [Symbol.asyncIterator]() {
        return {
          async next() {
            const { done, value } = await reader.read();
            if (done) {
              return { done: true, value: null };
            }
            return {
              done: false,
              value: new TextDecoder("utf-8").decode(value),
            };
          },
        };
      },
    };
  } catch (error: any) {
    throw error;
  }
}

export const get = (url: string, options?: any) => {
  return fetch(url, {
    method: 'GET',
    ...options
  });
};

export const post = (url: string, options?: any) => {
  return fetch(url, {
    method: 'POST',
    ...options
  });
};

export const postJson = (url: string, data: any) => {
  return post(url, {
    headers: {
      'Content-Type': 'application/json'
    },
    body: JSON.stringify(data)
  });
}

export const put = (url: string, options?: any) => {
  return fetch(url, {
    method: 'PUT',
    ...options
  });
};

export const putJson = (url: string, data: any) => {
  return put(url, {
    headers: {
      'Content-Type': 'application/json'
    },
    body: JSON.stringify(data)
  });

}

export const del = (url: string, options?: any) => {
  return fetch(url, {
    method: 'DELETE',
    ...options
  });
};

