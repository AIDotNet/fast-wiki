import { useEffect } from "react";
export default function Auth() {
    useEffect(() => {
        const params = new URLSearchParams(window.location.search);
        const code = params.get('token');
        localStorage.setItem('token', code || '');
        if (code) {
            window.location.href = '/chat';
        }
    }, [])
    return (<div style={{
        background: 'linear-gradient(45deg, #FE6B8B 30%, #FF8E53 90%)',
        height: '100vh',
        width: '100vw',
        position: 'absolute',
        top: 0,
        left: 0,
        paddingTop: '100px',

    }}>
        <h2 style={{
            textAlign: 'center',
            marginTop: '100px',
            margin: 'auto',
        }}>第三方登录授权</h2>
        <div>
            <h3 style={{
                textAlign: 'center',
                marginTop: '100px',
            }}>正在登录中...</h3>
        </div>
    </div>)
}

