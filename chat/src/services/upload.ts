import { DB_File } from '@/database/client/schemas/files';
import compressImage from '@/utils/compressImage';

class UploadService {
  async uploadFile(file: DB_File) {
    // 跳过图片上传测试
    const isTestData = file.size === 1;
    if (this.isImage(file.fileType) && !isTestData) {
      return this.uploadImageFile(file);
    }

    // save to local storage
    // we may want to save to a remote server later
    return file;
  }

  private isImage(fileType: string) {
    const imageRegex = /^image\//;
    return imageRegex.test(fileType);
  }

  private async uploadImageFile(file: any) {
    // 加载图片
    const url = file.url || URL.createObjectURL(new Blob([file.data!]));

    const img = new Image();
    img.src = url;
    await (() =>
      new Promise((resolve) => {
        img.addEventListener('load', resolve);
      }))();

    // 压缩图片
    const base64String = compressImage({ img, type: file.fileType });
    const binaryString = atob(base64String.split('base64,')[1]);
    const uint8Array = Uint8Array.from(binaryString, (char) => char.charCodeAt(0));
    file.data = uint8Array.buffer;

    return file;
  }

}

export const uploadService = new UploadService();
