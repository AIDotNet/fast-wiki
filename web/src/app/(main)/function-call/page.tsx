import { Input } from "@lobehub/ui"
import styled from "styled-components"
import { Button } from 'antd'
import { useState } from "react";
import FunctionCallList from "./features/FunctionCallList";
import UpdateFunctionCall from "./features/UpdateFunctionCall";
import { useNavigate } from "react-router-dom";
const FunctionCallWrapper = styled.div`
    padding: 20px;
    width: 100%;
`

export default function DesktopLayout() {
    const [keyword, setKeyword] = useState('');
    const navigate = useNavigate();
    const [input, setInput] = useState({
        page: 1,
        pageSize: 10,
        search: '',
    });
    const [updateData, setUpdateData] = useState({
        visible: false,
        value: {}
    } as any);


    return (<FunctionCallWrapper>
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
                    onChange={(e: any) => setKeyword(e.target.value)}
                    style={{
                        marginRight: '20px',
                        marginTop: '5px'
                    }} placeholder="请输入名称或描述的内容搜索" />
                <Button onClick={() => 
                    navigate('/function-call/create')
                } style={{
                    marginRight: '20px',
                    marginTop: '5px'
                }} >
                    新建Function
                </Button>
            </div>
        </div>
        <FunctionCallList input={input} setInput={(i: any) => {
            setInput(i);
        }} updateFunctionCall={(v: any) => {
            setUpdateData({
                visible: true,
                value: v
            });
        }} />
        <UpdateFunctionCall
            visible={updateData.visible}
            value={updateData.value}
            onCancel={() => {
                setUpdateData({
                    visible: false,
                    value: {}
                });
            }}
            onSuccess={() => {
                setUpdateData({
                    visible: false,
                    value: {}
                });
            }}
        />
    </FunctionCallWrapper>)
}