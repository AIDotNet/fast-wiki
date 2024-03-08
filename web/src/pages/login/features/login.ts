import { login } from "../../../services/AuthorizeService";
import { message } from "antd";

export function handleLogin(user: string, password: string, onSuccess: () => void) {
    login(user, password).then((value) => {
        message.success('登录成功');
        if (value.token) {
            localStorage.setItem('token', value.token);
            onSuccess()
        }
    }).catch(() => {
    });
}