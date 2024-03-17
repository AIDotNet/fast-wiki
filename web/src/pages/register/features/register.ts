import { message } from "antd";
import { Create } from "../../../services/UserService";

export function handleRegister(account: string, password: string, name: string, email: string, phone: string, onSuccess: () => void) {
    // 校验账号密码
    if (!account) {
        message.error('请输入账号');
        return;
    }
    if (!password) {
        message.error('请输入密码');
        return;
    }
    if (!name) {
        message.error('请输入昵称');
        return;
    }
    if (!email) {
        message.error('请输入邮箱');
        return;
    }
    // 校验邮箱 
    if (!(/^([a-zA-Z0-9_-])+@([a-zA-Z0-9_-])+(.[a-zA-Z0-9_-])+/).test(email)) {
        message.error('请输入正确的邮箱');
        return;
    }

    // 校验账号密码长度是否符合要求
    if (account.length < 6 || account.length > 20) {
        message.error('账号长度为6-20位');
        return;
    }

    if (password.length < 6 || password.length > 20) {
        message.error('密码长度为6-20位');
        return;
    }

    Create({
        account,
        password,
        name,
        email,
        phone
    }).then(() => {
        message.success('注册成功');
        onSuccess();
    })
}