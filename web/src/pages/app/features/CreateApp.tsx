import { Modal } from "@lobehub/ui";
import { Form, Input, Button, message } from 'antd';
import { CreateChatApplications } from "../../../services/ChatApplicationService";

interface ICreateAppProps {
    visible: boolean;
    onClose: () => void;
    onSuccess: () => void;
}

type CreateAppType = {
    name?: string;
};

export function CreateApp(props: ICreateAppProps) {
    async function onFinish(values: any) {
        try {
            await CreateChatApplications(values);
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
            title="创建应用"
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
                <Form.Item<CreateAppType>
                    label="应用名称"
                    name="name"
                    rules={[{ required: true, message: '请输入您的应用名称' }]}
                >
                    <Input />
                </Form.Item>

                <Form.Item>
                    <Button block type="primary" htmlType="submit">
                        创建
                    </Button>
                </Form.Item>
            </Form>
        </Modal>
    )
}