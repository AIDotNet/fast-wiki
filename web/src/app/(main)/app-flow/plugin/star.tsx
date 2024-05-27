import { Card, Divider, Tag } from 'antd';
import { useCallback } from 'react';
import { Handle, Position } from 'reactflow';
import styled from 'styled-components';

const Container = styled(Card)`
    box-shadow: 0 0 5px rgba(0, 0, 0, 0.1);
    border-radius: 5px;
`;

const PTag = styled(Tag)`
    /* 靠右对齐 */
    float: right;
    margin-right: 8px;
`;

const List = styled.div`
    font-size: 10;
    margin-bottom: 8px;
`;

interface StarProps {
    data: any;
}

export default function Star({ data }: StarProps) {

    const onChange = useCallback((evt) => {
        console.log(evt.target.value);
    }, []);

    return (
        <Container title="流程开始" bordered={false}>
            <div>
                <span>
                    用户输入：
                </span>
                <PTag>
                    string
                </PTag>
            </div>
            <Divider />
            <div>
                <div style={{
                    fontSize: 14,
                    fontWeight: 600,
                    marginBottom: 8
                }}>
                    全局变量：
                </div>
                <List>
                    <span>应用ID：</span>
                    <PTag >string</PTag>
                </List>
                <List>
                    <span>当前时间：</span>
                    <PTag>string</PTag>
                </List>
            </div>
            <Handle type="source" position={Position.Bottom} />
        </Container>)
}