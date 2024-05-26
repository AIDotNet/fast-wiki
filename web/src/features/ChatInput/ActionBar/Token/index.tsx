import dynamic from 'next/dynamic';
import { memo } from 'react';
const LargeTokenContent = dynamic(() => import('./TokenTag'), { ssr: false });

const Token = memo(() => {

  return <LargeTokenContent />;
});

export default Token;
