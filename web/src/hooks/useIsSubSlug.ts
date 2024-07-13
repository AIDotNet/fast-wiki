import { useLocation } from 'react-router-dom';

/**
 * Returns true if the current path has a sub slug (`/chat/mobile` or `/chat/settings`)
 */
export const useIsSubSlug = () => {
  const location = useLocation();
  const pathname = location.pathname;

  const slugs = pathname.split('/').filter(Boolean);

  return slugs.length > 1;
};