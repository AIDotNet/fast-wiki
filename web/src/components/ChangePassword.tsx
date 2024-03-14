import { Modal } from "@lobehub/ui";
import Divider from "@lobehub/ui/es/Form/components/FormDivider";
import { Input,message } from 'antd'
import { UpdateChangePassword } from "../services/UserService";
import { useState } from "react";

interface IChangePasswordProps {
    visible: boolean;
    onClose: () => void;
    onSuccess: () => void;
}

export default function ChangePassword({
    visible,
    onClose,
    onSuccess
}: IChangePasswordProps) {

    const [password, setPassword] = useState<string>('')
    const [newPassword, setNewPassword] = useState<string>('')
    const [confirmPassword, setConfirmPassword] = useState<string>('')

    async function ChangePasswordHandler(){
        
        if(password === '' || newPassword === '' || confirmPassword === ''){
            message.error('密码不能为空')
            return
        }

        // 判断密码是否复杂
        if (newPassword.length < 6) {
            message.error('密码长度不能小于6位')
            return
        }

        if (newPassword.length > 40) {
            message.error('密码长度不能大于40位')
            return
        }
        
        if (newPassword !== confirmPassword) {
            message.error('两次密码不一致')
            return
        }
        
        try {
            await UpdateChangePassword(password, newPassword)
            message.success('修改成功')
            onSuccess()
        } catch (error) {
        }


    }

    return (<Modal title='修改密码' width={400} open={visible} onOk={()=>ChangePasswordHandler()} onCancel={() => onClose}>
        <Divider />
        <Input style={{
            marginBottom: 20
        }}  type='password' 
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            placeholder="原密码"/>
        <Input style={{
            marginBottom: 20
        }}  type='password' 
            value={newPassword}
            onChange={(e) => setNewPassword(e.target.value)}
            placeholder="新密码"/>
        <Input style={{
            marginBottom: 20
        }}  type='password' 
            value={confirmPassword}
            onChange={(e) => setConfirmPassword(e.target.value)}
            placeholder="确认新密码"/>
    </Modal>)
}