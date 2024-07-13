import React, { FC, PropsWithChildren, useEffect, useState } from 'react';

// 自定义钩子，用于检测是否为移动设备
const useIsMobile = () => {
  const [isMobile, setIsMobile] = useState(false);

  useEffect(() => {
    const checkIfMobile = () => {
      setIsMobile(window.innerWidth < 768);
    };

    // 监听窗口大小变化
    window.addEventListener('resize', checkIfMobile);
    // 初始化检查
    checkIfMobile();

    // 清理监听器
    return () => window.removeEventListener('resize', checkIfMobile);
  }, []);

  return isMobile;
};

interface ClientResponsiveLayoutProps {
  Desktop: FC<PropsWithChildren<any>>;
  Mobile: FC<PropsWithChildren<any>>;
}

// 响应式布局组件
const ClientResponsiveLayout: FC<ClientResponsiveLayoutProps> = ({ Desktop, Mobile }) => {
  const isMobile = useIsMobile();

  // 根据是否为移动设备渲染对应组件
  return isMobile ? <Mobile /> : <Desktop />;
};

export default ClientResponsiveLayout;