import { GetWikisList } from '@/services/WikiService';
import { Input } from '@lobehub/ui';
import { Card, Divider, InputNumber, Select, Slider, Tag } from 'antd';
import { useCallback, useEffect, useState } from 'react';
import { Handle, NodeResizer, Position } from 'reactflow';
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

export default function WikiSearch({ data }: StarProps) {

    const [wiki, setWiki] = useState([] as any[]);

    function loadingWiki() {
        GetWikisList('', 1, 1000)
            .then((wiki) => {
                setWiki(wiki.result);
                console.log(wiki.result);

            });
    }

    useEffect(() => {
        loadingWiki();
    }, []);

    const onChange = useCallback((evt) => {
        console.log(evt.target.value);
    }, []);

    return (
        <Container
            title="知识库搜索"
            bordered={false}>
            <NodeResizer minWidth={100} minHeight={30} />
            <Handle type="target" position={Position.Top} />
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
                    style={{
                        width: '100%',
                        marginTop: 8,
                        marginBottom: 8
                    }}
                    placeholder="绑定知识库"
                    defaultValue={data?.application?.wikiIds}
                    value={data?.application?.wikiIds}
                    onChange={(v: any) => {
                        data.application.wikiIds = v;
                        data.onApplication(data.application);
                    }}
                    options={wiki?.map((item) => {
                        return {
                            label: item.name,
                            value: item.id
                        }
                    })}
                />
                <Card title='搜索参数设置'>
                    <span style={{
                        marginRight: 8
                    }}>向量匹配相似</span>
                    <InputNumber
                        style={{
                            width: '100%'
                        }}
                        min={0}
                        max={1}
                        onChange={(v) => {
                            data.application.relevancy = v;
                            data.onApplication(data.application);
                        }}
                        value={data?.application.relevancy} />
                </Card>
            </div>
            <Handle type="source" position={Position.Bottom} />
        </Container>
    )
}