import { getModels } from '@/utils/model';
import { AutoComplete, Card, Divider, InputNumber, Select, Tag } from 'antd';
import { useCallback, useEffect, useState } from 'react';
import { Handle, Position } from 'reactflow';
import styled from 'styled-components';

const Container = styled(Card)`
    box-shadow: 0 0 5px rgba(0, 0, 0, 0.1);
    border-radius: 5px;
`;

const ListItem = styled.div`
    display: flex;
    justify-content: space-between;
    padding: 20px;
    width: 100%;
`;

interface StarProps {
    data: any;
}

export default function AIChat({ data }: StarProps) {

    const [selectChatModel, setSelectChatModel] = useState([] as any[]);

    const onChange = useCallback((evt) => {
        console.log(evt.target.value);
    }, []);

    useEffect(() => {
        const models = getModels();
        setSelectChatModel(models.chatModel.map((item) => {
            return { label: item.label, value: item.value }
        }))
    }, []);

    return (
        <Container title="AI对话" bordered={false}>
            <Handle type="target" position={Position.Top} />
            <div style={{
                fontSize: 10,
                marginBottom: 8,

            }}>
                AI大模型对话
            </div>
            <Divider />
            <Card>
                <ListItem>
                    <span style={{
                        fontSize: 20,
                        marginRight: 20
                    }}>对话模型</span>
                    <AutoComplete
                        defaultValue={data.application.chatModel}
                        value={data.application.chatModel}
                        style={{ width: 380 }}
                        onChange={(v: any) => {
                            data.application.chatModel = v;
                            data.onApplication(data.application);
                        }}
                        options={selectChatModel}
                    />
                </ListItem>
                <ListItem>
                    <span style={{
                        fontSize: 20,
                        marginRight: 20
                    }}>提示词</span>
                    <textarea value={data.application.prompt}
                        defaultValue={data.application.prompt}
                        onChange={(e: any) => {
                            data.application.prompt = e.target.value;
                            data.onApplication(data.application);
                        }}
                        style={{ width: 380, resize: "none", height: '200px' }}>
                    </textarea>
                </ListItem>
                <ListItem>
                    <span style={{
                        fontSize: 20,
                        marginRight: 20
                    }}>引用模板提示词
                    </span>
                    <textarea value={data.application.template}
                        defaultValue={data.application.template}
                        onChange={(e: any) => {
                            data.application.template = e.target.value;
                            data.onApplication(data.application);
                        }}
                        style={{ width: 380, resize: "none", height: '200px' }}>
                    </textarea>
                </ListItem>
                <ListItem>
                    <span style={{
                        fontSize: 20,
                        marginRight: 20
                    }}>引用令牌上限</span>
                    <InputNumber
                        style={{
                        }}
                        min={100}
                        max={128000}
                        onChange={(v) => {
                            data.application.referenceUpperLimit = v;
                            data.onApplication(data.application);
                        }}
                        value={data?.application.referenceUpperLimit} />
                </ListItem>
            </Card>
            <Handle type="source" position={Position.Bottom} />
        </Container>)
}