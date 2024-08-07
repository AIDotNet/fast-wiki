import { CreateShare } from "@/services/ChatApplicationService";
import { Modal } from "@lobehub/ui";
import { Form, Input, Button, message } from 'antd';

interface CreateApplicationProps {
    visible: boolean;
    id: string;
    onClose: () => void;
    onSuccess: () => void;

}

export default function CreateApplication({ visible, onClose, onSuccess,id }: CreateApplicationProps) {

    return (<Modal
        open={visible}
        onCancel={onClose}
        width={400}
        footer={null}
        title="创建分享应用"
    >
        <Form
            name="basic"
            onFinish={async (values) => {
                if(values.availableToken === undefined) {
                    values.availableToken = -1;
                }
                if(values.availableQuantity === undefined) {
                    values.availableQuantity = -1;
                }
                values.chatApplicationId = id;

                await CreateShare(values);
                message.success('创建成功');
                onSuccess();
            }}
            onFinishFailed={(errorInfo) => {
                console.log('Failed:', errorInfo);
            }}
            autoComplete="off"
        >
            <Form.Item
                label="应用名称"
                name="name"
                rules={[{ required: true, message: '请输入您的应用名称' }]}
            >
                <Input />
            </Form.Item>
            <Form.Item
                label="Token数量"
                name="availableToken"

            >
                <Input defaultValue={-1}  min={-1} type='number' />
            </Form.Item>
            <Form.Item
                label="可用数量"
                name="availableQuantity"
            >
                <Input defaultValue={-1} min={-1} type='number' />
            </Form.Item>
            <Form.Item
                label="过期时间"
                name="expires"
            >
                <Input type='date' />
            </Form.Item>


            <Form.Item>
                <Button block type="primary" htmlType="submit">
                    创建
                </Button>
            </Form.Item>
        </Form>
    </Modal>)
}