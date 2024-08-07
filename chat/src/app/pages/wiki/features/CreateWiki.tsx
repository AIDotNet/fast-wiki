import { Modal } from "@lobehub/ui";
import { Form, Input, Button, message, AutoComplete, Upload } from 'antd';
import { useEffect, useState } from "react";
import { PlusOutlined } from '@ant-design/icons';
import { CreateWikis } from "@/services/WikiService";
import { UploadFile } from "@/services/StorageService";
import { getModels } from "@/utils/model";

interface ICreateAppProps {
    visible: boolean;
    onClose: () => void;
    onSuccess: () => void;
}

type CreateAppType = {
    name?: string;
};

export function CreateApp(props: ICreateAppProps) {

    const [model, setModel] = useState([] as any[]);
    const [embeddingModel, setEmbeddingModel] = useState([] as any[]);

    const [previewVisible, setPreviewVisible] = useState(false);
    const [previewImage, setPreviewImage] = useState("");
    const [previewTitle, setPreviewTitle] = useState("");
    const [fileList, setFileList] = useState([] as any[]);

    const handleCancel = () => setPreviewVisible(false);

    const handlePreview = async (file: any) => {
        if (!file.url && !file.preview) {
            file.preview = await getBase64(file.originFileObj);
        }

        setPreviewImage(file.url || file.preview);
        setPreviewVisible(true);
        setPreviewTitle(
            file.name || file.url.substring(file.url.lastIndexOf("/") + 1)
        );
    };

    const handleChange = ({ fileList }: any) => setFileList(fileList);

    const getBase64 = (file: any) => {
        return new Promise((resolve, reject) => {
            const reader = new FileReader();
            reader.readAsDataURL(file);
            reader.onload = () => resolve(reader.result);
            reader.onerror = (error) => reject(error);
        });
    };


    useEffect(() => {
        const models = getModels()

        setModel(models.chatModel.map((item) => {
            return { label: item.label, value: item.value }
        }));
        setEmbeddingModel(models.embeddingModel.map((item) => {
            return { label: item.label, value: item.value }
        }));

    }, []);


    async function onFinish(values: any) {
        try {
            if (fileList.length === 0) {
                return message.error('请上传头像');
            }
            const resultFile = await UploadFile(fileList[0].originFileObj)
            values.icon = resultFile.path;
            await CreateWikis(values);
            message.success('创建成功');
            props.onSuccess();
        } catch (e) {
            message.error('创建失败');
        }
    }

    function onFinishFailed(errorInfo: any) {
        console.log('Failed:', errorInfo);
    }

    return (
        <Modal
            title="创建知识库"
            open={props.visible}
            onCancel={props.onClose}
            width={400}
            footer={null}
        >
            <Form
                name="basic"
                onFinish={onFinish}
                onFinishFailed={onFinishFailed}
                autoComplete="off"
            >
                <Form.Item style={{
                    display: "flex",
                    justifyContent: "center",
                    alignItems: "center",
                    marginBottom: 0
                }}>
                    <Upload
                        listType="picture-card"
                        fileList={fileList}
                        onPreview={handlePreview}
                        onChange={handleChange}
                    >
                        {fileList.length >= 1 ? null : (
                            <div>
                                <PlusOutlined />
                                <div style={{ marginTop: 8 }}>上传</div>
                            </div>
                        )}
                    </Upload>
                    <Modal
                        visible={previewVisible}
                        title={previewTitle}
                        footer={null}
                        onCancel={handleCancel}
                    >
                        <img alt="预览" style={{ width: "100%" }} src={previewImage} />
                    </Modal>
                </Form.Item>

                <Form.Item<CreateAppType>
                    label="知识库名称"
                    name="name"
                    style={{
                        marginTop:8,
                    }}
                    rules={[{ required: true, message: '请输入您的知识库名称' }]}>
                    <Input />
                </Form.Item>

                <Form.Item
                    label="模型"
                    name="model"

                    rules={[{ required: true, message: '请选择模型' }]}>
                    <AutoComplete
                        options={model}
                    />
                </Form.Item>
                <Form.Item
                    label="嵌入模型"
                    name="embeddingModel"
                    rules={[{ required: true, message: '请选择模型' }]}>
                    <AutoComplete
                        options={embeddingModel}
                    />
                </Form.Item>

                <Form.Item>
                    <Button block htmlType="submit">
                        创建
                    </Button>
                </Form.Item>
            </Form>
        </Modal>
    )
}