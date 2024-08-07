import { useSearchParams } from 'react-router-dom';
export const useQuery = () => {
  const [searchParams] = useSearchParams();
  return {
    showMobileWorkspace : searchParams.get('session'),
    tab : searchParams.get('tab'),
  };
};
