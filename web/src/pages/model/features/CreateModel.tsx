import { Modal } from "@lobehub/ui";
import { Form, Input, Button, message, Select } from 'antd';
import { useEffect, useState } from "react";
import { CreateFastModel, GetChatTypes } from "../../../services/ModelService";
import { getModels } from "../../../store/Model";

interface ICreateModelProps {
    visible: boolean;
    onSuccess: any;
    onCancel: any;
}

export default function CreateModel({
    visible,
    onSuccess,
    onCancel,
}: ICreateModelProps) {

    const [chatModul, setChatModul] = useState([] as any[]);
    const [models, setModels] = useState([] as any[]);

    useEffect(() => {
        GetChatTypes()
            .then((chatModul) => {
                const items = chatModul.map((item: string) => {
                    return { label: item, value: item }
                });
                setChatModul(items);
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
        <Modal open={visible} title="创建模型" 
            footer={null}
            onCancel={onCancel}
            >
            <Form
                name="basic"
                onFinish={async (values: any) => {
                    console.log(values);
                    if(values.type === undefined || values.type === '') {
                        message.error('模型类型是必须的');
                        return;
                    }
                    try {
                        await CreateFastModel(values)
                        message.success('创建成功');
                        onSuccess();
                    } catch (error) {
                        message.error('创建失败');
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
                    rules={[{ required: true, message: '请输入您的模型名称' }]}
                >
                    <Input />
                </Form.Item>
                <Form.Item
                    label="模型类型"
                    rules={[{ required: true, message: '模型类型是必须的' }]}
                    name="type"
                >
                    <Select
                        title="请选择您的模型类型"
                        style={{
                            width: '100%',
                        }}
                        options={chatModul}
                    />
                </Form.Item>
                <Form.Item
                    label="模型地址"
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
                    name="description"
                    rules={[{ required: true, message: '描述是必须的' }]}
                >
                    <Input />
                </Form.Item>
                <Form.Item
                    label="模型优先级"
                    name="order"
                    initialValue={-1}
                >
                    <Input 
                        type='number' />
                </Form.Item>

                <Form.Item>
                    <Button block type="primary" htmlType="submit">
                        创建
                    </Button>
                </Form.Item>
            </Form>
        </Modal>
    );
}