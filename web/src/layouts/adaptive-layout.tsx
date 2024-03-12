import { useState, useEffect } from 'react';
import { isMobileDevice } from '../components/ResponsiveIndex';

interface IAdaptiveLayoutProps {
    DesktopPage: React.ComponentType;
    MobilePage: React.ComponentType;
}

export default function AdaptiveLayout({ DesktopPage, MobilePage }: IAdaptiveLayoutProps) {
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


interface INodeAdaptiveLayoutProps {
    DesktopPage: React.ReactNode;
    MobilePage: React.ReactNode;
}


export function NodeAdaptiveLayout({ DesktopPage, MobilePage }: INodeAdaptiveLayoutProps) {
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

    return PageComponent;
}