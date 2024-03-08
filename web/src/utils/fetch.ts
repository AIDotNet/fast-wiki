import { message } from 'antd';
import { config } from "../config";

export async function fetch(url: string, options: any) {
  const token = localStorage.getItem('token');
  const headers = {
    ...options.headers,
    Authorization: `Bearer ${token}`
  };
  try {
    // 拼接baseUrl并且处理/重复问题
    const baseUrl = config.FAST_API_URL;
    url = `${baseUrl}${url}`.replace(/([^:]\/)\/+/g, '$1');
    const response = await window.fetch(url, { ...options, headers });
    if (response.status >= 200 && response.status < 300) {
      return response.json();
    }

    // 如果是401，跳转到登录页
    if (response.status === 401) {
      window.location.href = '/login';
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

export const del = (url: string, options?: any) => {
  return fetch(url, {
    method: 'DELETE',
    ...options
  });
};

