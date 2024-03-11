import { useState } from "react";
import styled from 'styled-components';
import { Button } from 'antd';
import { GetSearchVectorQuantity } from "../../../services/WikiService";


const Textarea = styled.textarea`
    width: 100%;
    height: 300px;
    resize: none;
    border: none;
    border-radius: 8px;
    outline: none;
    padding: 6px;
    font-size: 16px;
    line-height: 1.5;
`;

const DataItem = styled.div`
    padding: 16px;
    border: 1px solid #d9d9d9;
    border-radius: 8px;
    cursor: pointer;
    margin-bottom: 16px;
    transition: border-color 0.3s linear;
    &:hover {
        // 边框颜色发生变化
        border-color: #1890ff;
    }
`;

interface ISearchWikiDetailProps {
    id: string;
    onChagePath(key: string): void;
}



export default function SearchWikiDetail({
    id,
}: ISearchWikiDetailProps) {

    const [value, setValue] = useState('')

    async function SearchVectorQuantity() {
        try {
            if (value === '') return;
            const result = await GetSearchVectorQuantity(id, value, 0.4)
            setData(result.result)
            setElapsedTime(result.elapsedTime)
        } catch (error) {

        }
    }

    const [data, setData] = useState<any[]>([]);
    const [elapsedTime, setElapsedTime] = useState(null);

    return (
        <>
            <div style={{ display: "flex" }}>
                <div style={{ width: "300px" }}>
                    <div style={{
                        fontSize: 30,
                        fontWeight: 600,
                        marginBottom: 16
                    }}>
                        搜索测试
                    </div>
                    <Textarea value={value} onChange={(v) => {
                        setValue(v.target.value)
                    }}>
                    </Textarea>
                    <Button disabled={
                        value === ''
                    } onClick={SearchVectorQuantity} block>
                        搜索
                    </Button>
                </div>
                <div style={{
                    flex: 1,
                    padding: 8,

                }}>
                    {
                        data.length === 0 && <div style={{
                            fontSize: 30,
                            fontWeight: 600,
                            marginBottom: 16,
                            textAlign: 'center'
                        }}>
                            暂无数据
                        </div>
                    }
                    {
                        elapsedTime !== null && <div style={{
                            fontSize: 16,
                            opacity: 0.6,
                            marginBottom: 16
                        }}>
                            耗时: {elapsedTime}ms
                        </div>
                    }
                    <div style={{
                        display: 'flex',
                        flexDirection: 'column',
                        gap: 16,
                        overflow: 'auto',

                        height: 'calc(100vh - 100px)',
                        scrollbarWidth: 'none',
                        msOverflowStyle: 'none',

                    }}>
                        {
                            data.map((item, index) => {
                                return <DataItem key={index}>
                                    <div style={{
                                        fontSize: 15,
                                        fontWeight: 600
                                    }}>
                                        {item.fileName}
                                        <span style={{
                                            color: '#1890ff',
                                            marginLeft: 8
                                        }}>
                                            #(相似度{item.relevance.toFixed(4)})
                                        </span>
                                    </div>
                                    <div style={{
                                        opacity: 0.6,
                                        fontSize: 12
                                    }}>
                                        {item.content}
                                    </div>
                                </DataItem>
                            })
                        }
                    </div>
                </div>
            </div>
        </>
    )
}