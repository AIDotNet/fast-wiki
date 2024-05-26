import { Button } from 'antd';

import { Header } from '@lobehub/ui';
import { CreateApp } from './CreateApp';
import { useState } from 'react';

interface IAppHeaderProps {
    onSucess: () => void;
}

export default function AppHeader(props:IAppHeaderProps) {
    const [visible, setVisible] = useState(false);
    const onClose = () => setVisible(false);
    return (
        <>
            <Header actions={<Button onClick={()=>setVisible(true)}>新增</Button>}  nav={'应用管理'} />
            <CreateApp visible={visible} onClose={onClose} onSuccess={(()=>{
                props.onSucess();
                onClose();
            })}/>
        </>
    )
}