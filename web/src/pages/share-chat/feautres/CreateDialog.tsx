import { useState } from "react";
import { Modal } from "antd";
import { Input } from "@lobehub/ui";
import { CreateChatDialog } from "../../../services/ChatApplicationService";

interface IAppHeaderProps {
    id: string; // 应用ID
    onSucess: () => void; // 成功回调
    visible: boolean; // 是否显示
    onClose: () => void; // 关闭回调
    type: number; // 类型
}

export default function CreateDialog({
    id,
    onSucess,
    visible,
    onClose,
    type,
}: IAppHeaderProps) {

    const [data, setData] = useState({
        name:"",
        description:'',
        type: type,
    });
    console.log(id);
    

    function createDialog() {

        CreateChatDialog({
            ...data,
            applicationId: id
        })
            .then(() => {
                onSucess();
            })
    }

    return (
        <Modal title="创建对话" onOk={createDialog} open={visible} onCancel={onClose} >
            <div>
                <Input value={data.name}
                    onChange={(e) => {
                        setData({
                            ...data,
                            name: e.target.value
                        })
                    }}
                    placeholder="对话名称"/>
            </div>
            <div style={{
                marginTop: 16
            }}>
                <Input value={data.description} 
                    onChange={(e) => {
                        setData({
                            ...data,
                            description: e.target.value
                        })
                    }}
                    placeholder="对话描述"/>
            </div>
        </Modal>
    )
}