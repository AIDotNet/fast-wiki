import { FileModel } from '@/database/client/models/file';
import { DB_File } from '@/database/client/schemas/files';
import { serverConfigSelectors } from '@/store/serverConfig/selectors';
import { FilePreview } from '@/types/files';

import { IFileService } from './type';

export class ClientService implements IFileService {
  async createFile(file: DB_File) {
    // save to local storage
    // we may want to save to a remote server later
    return FileModel.create(file);
  }

  async getFile(id: string): Promise<FilePreview> {
    const item = await FileModel.findById(id);
    if (!item) {
      throw new Error('file not found');
    }
    // arrayBuffer to url
    const url = URL.createObjectURL(new Blob([item.data!], { type: item.fileType }));
    const base64 = this.arrayBufferToBase64(item.data!);

    return {
      base64Url: `data:${item.fileType};base64,${base64}`,
      fileType: item.fileType,
      id,
      name: item.name,
      saveMode: 'local',
      url,
    };
  }
  arrayBufferToBase64(buffer: ArrayBuffer) {
    let binary = '';
    const bytes = new Uint8Array(buffer);
    const len = bytes.byteLength;
    for (let i = 0; i < len; i++) {
      binary += String.fromCharCode(bytes[i]);
    }
    return window.btoa(binary);
  }
  async removeFile(id: string) {
    return FileModel.delete(id);
  }

  async removeAllFiles() {
    return FileModel.clear();
  }

  private get enableServer() {
    return serverConfigSelectors.enableUploadFileToServer(
      window.global_serverConfigStore.getState(),
    );
  }
}
