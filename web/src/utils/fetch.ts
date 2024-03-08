export default async function fetch(url: string, options: any) {
    const token = localStorage.getItem('token');
    const headers = {
        ...options.headers,
        Authorization: `Bearer ${token}`
    };
    try {
        const response = await window.fetch(url, { ...options, headers });
        if (response.status >= 200 && response.status < 300) {
            return response.json();
        }

        // 如果是401，跳转到登录页
        if (response.status === 401) {
            window.location.href = '/login';
        }

        const error = new Error(response.statusText);
        throw error;
    } catch (error) {
        console.error('Request failed', error);
        throw error;
    }
}

export const get = (url: string, options: any) => {
  return fetch(url, {
    method: 'GET',
    ...options
  });
};

export const post = (url: string, options: any) => {
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

export const put = (url: string, options: any) => {
  return fetch(url, {
    method: 'PUT',
    ...options
  });
};

export const del = (url: string, options: any) => {
  return fetch(url, {
    method: 'DELETE',
    ...options
  });
};