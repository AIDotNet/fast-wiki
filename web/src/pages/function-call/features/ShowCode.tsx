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
            onOk={() => setVisible(false)}
            onCancel={() => setVisible(false)}
            allowFullscreen={true}
        >
            <Highlighter language="js" showLanguage type='ghost' copyable>
                {record?.content}
            </Highlighter>
        </Modal>
    )
}