import { useState, useEffect } from 'react';
import DesktopPage from './(desktop)';
import MobilePage from './(mobile)';
import { isMobileDevice } from '../../components/ResponsiveIndex';

export default function Login() {
    const [isMobile, setIsMobile] = useState(isMobileDevice());

    useEffect(() => {
        const handleResize = () => {
            setIsMobile(isMobileDevice());
        };

        window.addEventListener('resize', handleResize);

        // 清除事件监听器
        return () => {
            window.removeEventListener('resize', handleResize);
        };
    }, []); // 空数组表示这个useEffect只在组件挂载和卸载时运行

    const PageComponent = isMobile ? MobilePage : DesktopPage;

    return <PageComponent />;
}