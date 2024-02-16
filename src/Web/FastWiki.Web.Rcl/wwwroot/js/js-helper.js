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

export {
    click, 
    previewImage,
    clickElement
};