import { Modal } from "@lobehub/ui";
import { Form, Input, Button, message, Select } from 'antd';
import { useEffect, useState } from "react";
import { GetChatTypes, UpdateFastModel } from "../../../services/ModelService";
import { getModels } from "../../../store/Model";

interface IUpdateModelProps {
    visible: boolean;
    onSuccess: any;
    onCancel: any;
    value?: any;
}

export default function UpdateModel({
    visible,
    onSuccess,
    onCancel,
    value
}: IUpdateModelProps) {

    const [chatModule, setChatModule] = useState([] as any[]);
    const [models, setModels] = useState([] as any[]);

    useEffect(() => {

        GetChatTypes()
            .then((chatModul) => {
                const items = chatModul.map((item: string) => {
                    return { label: item, value: item }
                });
                setChatModule(items);
            });

        getModels()
            .then((models: any) => {
                const items = models.chatModel.map((item: any) => {
                    return { label: item.label, value: item.value }
                });

                models.embeddingModel.map((item: any) => {
                    items.push({ label: item.label, value: item.value })
                });

                setModels(items);
            });
    }, []);


    return (
        <Modal open={visible} title="修改模型"
            footer={null}
            onCancel={onCancel}
        >
            <Form
                name="basic"
                onFinish={async (values: any) => {
                    console.log(values);
                    if (values.type === undefined || values.type === '') {
                        message.error('模型类型是必须的');
                        return;
                    }
                    try {
                        values.id = value.id;
                        await UpdateFastModel(values);
                        message.success('编辑成功');
                        onSuccess();
                    } catch (error) {
                        message.error('编辑失败');
                    }
                }}
                onFinishFailed={(errorInfo: any) => {
                    console.log('Failed:', errorInfo);
                }}
                autoComplete="off"
            >
                <Form.Item
                    label="模型名称"
                    name="name"
                    initialValue={value?.name}
                    rules={[{ required: true, message: '请输入您的模型名称' }]}
                >
                    <Input />
                </Form.Item>
                <Form.Item
                    label="模型类型"
                    name="type"
                    initialValue={value?.type}
                    rules={[{ required: true, message: '模型类型是必须的' }]}
                >
                    <Select
                        title="请选择您的模型类型"
                        style={{
                            width: '100%',
                        }}
                        options={chatModule}
                    />
                </Form.Item>
                <Form.Item
                    label="模型地址"
                    initialValue={value?.url}
                    name="url"
                >
                    <Input />
                </Form.Item>
                <Form.Item
                    label="请求密钥"
                    name="apiKey"
                >
                    <Input />
                </Form.Item>
                <Form.Item
                    label="模型"
                    name="models"
                >
                    <Select mode="tags" options={models} placeholder="渠道支持的模型" />
                </Form.Item>
                <Form.Item
                    label="模型描述"
                    initialValue={value?.description}
                    name="description"
                    rules={[{ required: true, message: '描述是必须的' }]}
                >
                    <Input />
                </Form.Item>
                <Form.Item
                    label="模型优先级"
                    name="order"
                    initialValue={value?.order}
                >
                    <Input
                        type='number' />
                </Form.Item>

                <Form.Item>
                    <Button block type="primary" htmlType="submit">
                        保存编辑
                    </Button>
                </Form.Item>
            </Form>
        </Modal>
    );
}