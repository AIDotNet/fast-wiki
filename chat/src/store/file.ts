import { FilePreview } from "@/types/files";
import useSWR, { SWRResponse } from 'swr';
import { produce } from 'immer';

import { notification } from '@/components/AntdStaticMethods';
import { create } from "zustand";
import { uploadService } from "@/services/upload";
import { fileService } from "@/services/file";


export interface FileAction {
    imagesMap: Record<string, FilePreview>;
    inputFilesList: string[];
    uploadingIds: string[];
    clearImageList: () => void;

    internal_addFile: (id: string) => void;
    internal_removeFile: (id: string) => void;
    internal_toggleLoading: (id: string, loading: boolean) => void;
    loadFileDetail: (id: string) => Promise<void>;
    removeAllFiles: () => Promise<void>;
    removeFile: (id: string) => Promise<void>;

    setImageMapItem: (id: string, item: FilePreview) => void;
    uploadFile: (file: File) => Promise<void>;
    useFetchFile: (id: string) => SWRResponse<FilePreview>;
    useFetchFiles: (ids: string[]) => SWRResponse<void>;
}

export const createFileSlice = create<FileAction>((set, get) => ({
    imagesMap: {},
    inputFilesList: [],
    uploadingIds: [],
    clearImageList: () => {
        set({ inputFilesList: [] }, false);
    },
    internal_addFile: (id: string) => {
        set(({ inputFilesList }) => ({ inputFilesList: [...inputFilesList, id] }), false);
    },
    internal_removeFile: (id: string) => {
        set(
            ({ inputFilesList }) => ({ inputFilesList: inputFilesList.filter((i) => i !== id) }),
            false,
        );
    },
    internal_toggleLoading: (id: string, loading) => {
        if (loading) {
            set(
                ({ uploadingIds }) => ({ uploadingIds: [...uploadingIds, id] }),
                false,
            );
        } else {
            set(
                ({ uploadingIds }) => ({ uploadingIds: uploadingIds.filter((i) => i !== id) }),
                false,
            );
        }
    },

    loadFileDetail: async (id) => {
        // initFile preview
        const item = await fileService.getFile(id);
        get().setImageMapItem(item.id, item);
    },

    removeAllFiles: async () => {
        await fileService.removeAllFiles();
    },
    removeFile: async (id) => {
        get().internal_removeFile(id);

        await fileService.removeFile(id);
    },

    setImageMapItem: (id, item) => {
        set(
            produce((draft) => {
                if (draft.imagesMap[id]) return;

                draft.imagesMap[id] = item;
            }),
            false
        );
    },

    uploadFile: async (file) => {
        const fileItem = {
            createdAt: file.lastModified,
            data: await file.arrayBuffer(),
            fileType: file.type,
            name: file.name,
            saveMode: 'local' as const,
            size: file.size,
        };

        // at first create a temp id for the file for optimistic rendering
        const tempId = Date.now().toString();
        get().internal_addFile(tempId);

        get().internal_toggleLoading(tempId, true);

        // create a local Url for display
        const url = URL.createObjectURL(new Blob([fileItem.data!], { type: fileItem.fileType }));
        get().setImageMapItem(tempId, { ...fileItem, id: tempId, url });

        try {
            // after finish upload, mark the `loading=false` to show the uploaded item
            const result = await uploadService.uploadFile(fileItem);
            const data = await fileService.createFile(result);
            get().internal_toggleLoading(tempId, false);

            // after finish upload, remove the temp id and add the final one
            get().internal_removeFile(tempId);
            get().internal_addFile(data.id);

            // initFile preview
            await get().loadFileDetail(data.id);
        } catch (error) {
            get().internal_removeFile(tempId);
            get().internal_toggleLoading(tempId, false);

            // show error message
            notification.error({
                description: "Upload failed",
                message: "Error",
            });
        }
    },

    useFetchFile: (id) =>
        useSWR(id, async (id) => {
            const item = await fileService.getFile(id);

            get().setImageMapItem(id, item);

            return item;
        }),
    useFetchFiles: (ids) =>
        useSWR(ids, async (ids) => {
            const pools = ids.map(async (id) => get().loadFileDetail(id));

            await Promise.all(pools);
        }),
}));
