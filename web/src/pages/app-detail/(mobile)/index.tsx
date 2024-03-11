import { Tabs } from 'antd';
import { memo } from 'react';
// import { useParams } from 'react-router-dom';


export default memo(() => {
    // const { id } = useParams<{ id: string }>();

    return (
        <div style={{ display: 'flex', margin: 20 }}>
            <div style={{ width: '120px' }}>
                <Tabs
                    tabPosition={'left'}
                    items={[
                        {
                            label: '应用配置',
                            key: '1',
                            children: <div>基本信息</div>
                        },
                        {
                            label: '对话记录',
                            key: '1',
                            children: <div>基本信息</div>
                        },
                        {
                            label: '发布应用',
                            key: '1',
                            children: <div>基本信息</div>
                        }
                    ]}
                />
            </div>
        </div>
    );
})