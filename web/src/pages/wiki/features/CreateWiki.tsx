import { Avatar, Modal } from "@lobehub/ui";
import { Form, Input, Button, message, Select } from 'antd';
import { useEffect, useState } from "react";
import { getModels } from "../../../store/Model";
import { CreateWikis } from "../../../services/WikiService";

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

    useEffect(() => {
        getModels()
            .then((models) => {
                setModel(models.chatModel.map((item) => {
                    return { label: item.label, value: item.value }
                }));
                setEmbeddingModel(models.embeddingModel.map((item) => {
                    return { label: item.label, value: item.value }
                }));
            });

    }, []);


    async function onFinish(values: any) {
        try {
            debugger;
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

                <Avatar
                    src='https://registry.npmmirror.com/@lobehub/assets-logo/latest/files/assets/logo-3d.webp'
                    size={64}
                    title="图标"
                    shape='square'
                    style={{
                        marginBottom: 16,
                        marginLeft: 'auto',
                        marginRight: 'auto',
                    }}
                />

                <Form.Item<CreateAppType>
                    label="知识库名称"
                    name="name"
                    rules={[{ required: true, message: '请输入您的知识库名称' }]}>
                    <Input />
                </Form.Item>

                <Form.Item
                    label="模型"
                    name="model"
                    rules={[{ required: true, message: '请选择模型' }]}>
                    <Select
                        options={model}
                    />
                </Form.Item>
                <Form.Item
                    label="量化模型"
                    name="embeddingModel"
                    rules={[{ required: true, message: '请选择模型' }]}>
                    <Select
                        options={embeddingModel}
                    />
                </Form.Item>

                <Form.Item>
                    <Button block  htmlType="submit">
                        创建
                    </Button>
                </Form.Item>
            </Form>
        </Modal>
    )
}