import { Icon, } from '@lobehub/ui';
import { Loader2 } from 'lucide-react';
import { memo } from 'react';
import { Center, Flexbox } from 'react-layout-kit';

const Loading = memo(() => {
    return <Flexbox height={'100%'} style={{ userSelect: 'none' }} width={'100%'}>
        <Center flex={1} gap={12} width={'100%'}>
            <span style={{
                fontSize: '40px',
                fontWeight: 'bold',
                fontFamily: 'Arial, sans-serif',
                userSelect: 'none',
            }}>
                Fast Wiki
            </span>
            <Center gap={16} horizontal>
                <Icon icon={Loader2} spin />
                加载中...
            </Center>
        </Center>
    </Flexbox>;
})

export default Loading;