import { Modal } from "@lobehub/ui";
import { Form, Input, Button, message, Select } from 'antd';
import { useEffect, useState } from "react";
import { CreateFastModel, GetChatTypes } from "../../../services/ModelService";

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

    useEffect(() => {

        GetChatTypes()
            .then((chatModul) => {
                const items = chatModul.map((item: string) => {
                    return { label: item, value: item }
                });
                console.log(items);
                
                setChatModul(items);
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
                    label="请选择您的模型类型"
                    name="type"
                >
                    <Select
                        title="请选择您的模型类型"
                        rules={[{ required: true, message: '模型类型是必须的' }]}
                        style={{
                            width: '100%',
                        }}
                        options={chatModul}
                    />
                </Form.Item>
                <Form.Item
                    label="模型地址"
                    name="url"
                    placeholder="请输入您的模型地址|为空则默认"
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
                    label="描述"
                    name="description"
                >
                    <Input />
                </Form.Item>
                <Form.Item
                    label="优先级"
                    name="order"
                    defaultValue={-1}
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