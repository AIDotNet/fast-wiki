import { Highlighter, Modal } from "@lobehub/ui";

interface IShowCodeProps {
    visible: any;
    setVisible: any;
    record: any;
}

export default function ShowCode({
    visible,
    setVisible,
    record
}: IShowCodeProps) {
    return (
        <Modal
            title="查看源码"
            open={visible}
            width={'100%'}
            onOk={() => setVisible(false)}
            onCancel={() => setVisible(false)}
        >
            <Highlighter style={{
                height:'60vh'
            }} language="js" showLanguage type='ghost' copyable>
                {record?.content}
            </Highlighter>
        </Modal>
    )
}