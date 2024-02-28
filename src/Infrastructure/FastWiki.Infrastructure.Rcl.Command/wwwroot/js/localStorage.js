/**
* 设置 localStorage
* @param {string} key - 存储的键名
* @param {any} value - 存储的值
*/
export function setLocalStorage(key, value) {
    try {
        localStorage.setItem(key, JSON.stringify(value));
    } catch (error) {
        console.error(`Error setting localStorage for key '${key}':`, error);
    }
}

/**
 * 获取 localStorage
 * @param {string} key - 要检索的键名
 * @returns {any} - 存储的值，如果不存在则返回 null
 */
export function getLocalStorage(key) {
    try {
        const item = localStorage.getItem(key);
        return item ? JSON.parse(item) : null;
    } catch (error) {
        console.error(`Error getting localStorage for key '${key}':`, error);
        return null;
    }
}

/**
 * 移除 localStorage
 * @param {string} key - 要移除的键名
 */
export function removeLocalStorage(key) {
    try {
        localStorage.removeItem(key);
    } catch (error) {
        console.error(`Error removing from localStorage for key '${key}':`, error);
    }
}

/**
 * 批量移除 localStorage
 * @param {string[]} keys - 要移除的键名数组
 */
export function removesLocalStorage(keys) {
    try {
        for (let i = 0; i < keys.length; i++) {
            localStorage.removeItem(keys[i]);
        }
    } catch (error) {
        console.error('Error removing from localStorage:', error);
    }
}

/**
 * 清空 localStorage
 */
export function clearLocalStorage() {
    try {
        localStorage.clear();
    } catch (error) {
        console.error('Error clearing localStorage:', error);
    }
}

/**
 * 检查是否支持 localStorage
 * @returns {boolean} - 返回是否支持 localStorage
 */
export function isLocalStorageSupported() {
    try {
        const testKey = '__test__';
        localStorage.setItem(testKey, testKey);
        localStorage.removeItem(testKey);
        return true;
    } catch (error) {
        return false;
    }
}

/**
 * 获取 localStorage 中的键名
 * @returns {string[]} - 存储的所有键名
 */
export function getLocalStorageKeys() {
    try {
        return Object.keys(localStorage);
    } catch (error) {
        console.error('Error getting localStorage keys:', error);
        return [];
    }
}

/***
 * 判断sessionStorage中是否存在某个key
 */
export function containKey(key) {
    try {
        if (localStorage.getItem(key)) {
            return true;
        } else {
            return false;
        }
    } catch (e) {
        console.error('Error getting localStorage length:', e);
        return false;
    }
} 