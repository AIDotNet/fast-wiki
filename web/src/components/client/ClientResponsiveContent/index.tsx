

import { useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { useIsMobile } from '@/hooks/useIsMobile';

interface ClientResponsiveContentProps {
  DesktopRoute: string;
  MobileRoute: string;
}

const ClientResponsiveContent = ({ MobileRoute, DesktopRoute }: ClientResponsiveContentProps) => {
  const navigate = useNavigate();
  const mobile = useIsMobile();

  useEffect(() => {
    const targetRoute = mobile ? MobileRoute : DesktopRoute;
    navigate(targetRoute);
  }, [mobile, navigate, MobileRoute, DesktopRoute]);

  return null; // 或者根据需要渲染一些通用的UI元素
};

export default ClientResponsiveContent;
