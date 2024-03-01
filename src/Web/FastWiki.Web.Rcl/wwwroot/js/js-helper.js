/**
 * 点击元素
 * @param id
 */
function click(id){
    const element = document.getElementById(id);
    
    if(!element)
        element.click();
}

function clickElement(element) {
    if(element)
        element.click();
}

/**
 * 显示图片预览
 * @param inputElement
 * @param imageElement
 */
function previewImage(inputElement, imageElement) {
    const url = URL.createObjectURL(inputElement.files[0]);
    imageElement.addEventListener('load', () => {
        URL.revokeObjectURL(url);
    });
    imageElement.src = url;
}

function copyText(text) {
    /* 获取要复制的内容 */
    /* 创建一个临时的textarea元素 */
    var tempInput = document.createElement("textarea");
    tempInput.value = text;
    document.body.appendChild(tempInput);

    /* 选择并复制文本 */
    tempInput.select();
    document.execCommand("copy");

    /* 删除临时元素 */
    document.body.removeChild(tempInput);

}

export {
    click, 
    previewImage,
    clickElement,
    copyText
};