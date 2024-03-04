/**
 * 跳转到指定页面
 * @param url 
 * @param type - 跳转类型
 */
export function openUrl(url, type) {
    if (type === '_blank') {
        window.open(url, '_blank');
    } else {
        window.location.href
    }
}


/**
 * 滚动到最底部
 * @param id 
 */
export function scrollToBottom(id) {
    const element = document.getElementById(id);
    if (element) {
        element.scrollTop = element.scrollHeight;
    }
}