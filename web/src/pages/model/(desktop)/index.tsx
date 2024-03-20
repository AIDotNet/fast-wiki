import { useState } from "react";
import ModelList from "../features/ModelList";
import { Input } from "@lobehub/ui";
import { Button } from 'antd';

import styled from "styled-components";
import CreateModel from "../features/CreateModel";

const UserWrapper = styled.div`
    padding: 20px;
    width: 100%;
`

export default function DesktopPage() {
    const [keyword, setKeyword] = useState('');
    const [createModelVisible, setCreateModelVisible] = useState(false);
    const [updateData, setUpdateData] = useState({} as any);

    return (
        <UserWrapper>
            <div style={{
                display: 'flex',
                justifyContent: 'space-between',
                marginBottom: '20px',

            }}>
                <div style={{
                    fontSize: '30px',
                    fontWeight: 'bold',
                    width: '200px',
                    textAlign: 'center',

                }}>模型类型管理</div>
                <div style={{
                    display: 'flex',
                    justifyContent: 'space-between',
                    width: '260px'
                }}>
                    <Input value={keyword}
                        onChange={(e) => setKeyword(e.target.value)}
                        style={{
                            marginRight: '20px',
                            marginTop: '5px'
                        }} placeholder="请输入名称或描述的内容搜索" />
                    <Button onClick={() => { setCreateModelVisible(true) }} style={{
                        marginRight: '20px',
                        marginTop: '5px'
                    }} >
                        新建模型
                    </Button>
                </div>
            </div>
            <ModelList updateData={updateData} />
            <CreateModel visible={createModelVisible} onCancel={() => {
                setCreateModelVisible(false);
                setUpdateData({
                    ...updateData,
                    key: new Date().getTime()
                });
            }} onSuccess={() => {
                setCreateModelVisible(false);
            }} />
        </UserWrapper>
    );
}