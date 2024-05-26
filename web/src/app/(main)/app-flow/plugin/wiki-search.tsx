import { Card, Divider, Select, Tag } from 'antd';
import { useCallback } from 'react';
import { Handle, Position } from 'reactflow';
import styled from 'styled-components';

const Container = styled(Card)`
    box-shadow: 0 0 5px rgba(0, 0, 0, 0.1);
    border-radius: 5px;
    background: white;
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

export default function WikiSearch({ data }: StarProps) {

    const onChange = useCallback((evt) => {
        console.log(evt.target.value);
    }, []);

    return (
        <Container
            title="知识库搜索"
            bordered={false}>
            <div style={{
                fontSize: 10,
                marginBottom: 8,

            }}>
                调用“语义检索”和“全文检索”能力，从“知识库”中查找可能与问题相关的参考内容
            </div>
            <Divider />
            <div>
                <Select
                    mode="multiple"
                    allowClear
                    style={{
                        width: '100%',
                        marginTop: 20,
                        marginBottom: 20
                    }}
                    placeholder="绑定知识库"
                    defaultValue={application.wikiIds}
                    value={application.wikiIds}
                    onChange={(v: any) => {
                        setApplication({
                            ...application,
                            wikiIds: v
                        });
                    }}
                    options={wiki.map((item) => {
                        return {
                            label: item.name,
                            value: item.id
                        }
                    })}
                />
            </div>
            <Handle type="source" position={Position.Bottom} />
        </Container>
    )
}