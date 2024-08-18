
import { Button, Steps, Radio, Input, message, } from 'antd';
import { useState } from 'react';
import styled from 'styled-components';
import { TextArea } from '@lobehub/ui';
import { ProcessMode, TrainingPattern }from './index.d';
import { CreateWikiDetailWebPage } from '@/services/WikiService';

const FileItem = styled.div`
    transition: border-color 0.3s linear;
    border: 1px solid #d9d9d9;
    border-radius: 8px;
    padding: 10px;
    cursor: pointer;
    margin-bottom: 10px;
    &:hover {
        border-color: #1890ff;
        transition: border-color 0.3s linear;
        box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
    }
`;

interface IUploadWikiFileProps {
    id: string;
    onChagePath(key: any): void;
}

export default function UploadWikiWeb({ id, onChagePath }: IUploadWikiFileProps) {
    const [current, setCurrent] = useState(0);
    // const [uploading, setUploading] = useState(false);
    const [webs, setWebs] = useState<any[]>([]);
    const [processMode, setProcessMode] = useState(ProcessMode.Auto);
    const [trainingPattern, setTrainingPattern] = useState(TrainingPattern.Subsection);
    const [maxTokensPerParagraph, setMaxTokensPerParagraph] = useState(1000); // 每个段落标记的最大数量。当对文档进行分区时，每个分区通常包含一个段落。
    const [maxTokensPerLine, setMaxTokensPerLine] = useState(300); // 每行，也就是每个句子的最大标记数。当分割一个文本块时，文本将被分割成句子，然后被分组成段落。注意，这适用于任何文本格式，包括表格、代码、聊天记录、日志文件等。
    const [overlappingTokens, setOverlappingTokens] = useState(100); // 重叠标记数。当对文档进行分区时，每个分区的开始和结束部分将重叠。这有助于确保模型在分区之间保持上下文一致性。
    const [value, setValue] = useState('');
    const [qAPromptTemplate, setQAPromptTemplate] = useState(`
    我会给你一段文本，学习它们，并整理学习成果，要求为：
    1. 提出最多 20 个问题。
    2. 给出每个问题的答案。
    3. 答案要详细完整，答案可以包含普通文字、链接、代码、表格、公示、媒体链接等 markdown 元素。
    4. 按格式返回多个问题和答案:

    Q1: 问题。
    A1: 答案。
    Q2:
    A2:
    ……

    我的文本："""{{$input}}"""`); // QA问答模板

    async function upload(){
        for (let i = 0; i < webs.length; i++) {
            const item = webs[i];
            const input: any = {
                wikiId: id as any,
                name: webs[i],
                trainingPattern: trainingPattern,
                maxTokensPerParagraph: maxTokensPerParagraph,
                maxTokensPerLine: maxTokensPerLine,
                overlappingTokens: overlappingTokens,
                qAPromptTemplate: qAPromptTemplate,
                mode: processMode,
                state: '',
                path: item,
            }
            await CreateWikiDetailWebPage(input)
        }
        message.success('上传成功');
    }

    return (<>
        <div >
            <Button onClick={() => {
                onChagePath('data-item')
            }}>返回</Button>
        </div>

        <Steps
            style={{
                marginTop: '20px',
            }}
            size="small"
            current={current}
            items={[
                {
                    title: '上传文件'
                },
                {
                    title: '数据处理'
                },
            ]}
        />

        {
            current === 0 && <>
                <textarea value={value} onChange={(e) => {
                    setValue(e.target.value);
                    // 换行符
                    let webs = e.target.value.split('\n');
                    // 过滤空字符串的和重复的
                    webs = Array.from(new Set(webs.filter(item => item !== '')));
                    webs = webs.map(item => {
                        if (item.includes('http')) {
                            return item;
                        } else {
                            return `http://${item}`;
                        }
                    });
                    setWebs(webs);
                }} style={{
                    width: '100%',
                    border: '1px solid #d9d9d9',
                    borderRadius: 8,
                    padding: 10,
                    marginBottom: 20,
                    height: 200,
                    overflow: 'auto',
                    resize: 'none',
                    fontSize: 14,
                    color: '#666',
                }}
                    placeholder="请输入网页链接"
                ></textarea>
                <div style={{
                    height: 'calc(100vh - 450px)',
                    overflow: 'auto',
                }}>
                    {webs.map((item, index) => {
                        return <FileItem key={index}>
                            {item}
                            <Button size='small' style={{
                                float: 'right'
                            }} onClick={() => {
                                const newWebs = webs.filter((_, i) => i !== index);
                                setWebs(newWebs);
                            }}>删除</Button>
                        </FileItem>
                    })}
                </div>

                <Button onClick={() => {
                    setCurrent(1);
                }} style={{
                    float: 'right',
                    marginTop: 20,
                }}>下一步</Button>
            </>
        }
        {
            current === 1 && <>
                <div style={{
                    height: 140,
                }}>
                    <Radio.Group style={{
                        marginBottom: 20
                    }} onChange={(v: any) => {
                        const value = Number(v.target.value);
                        setTrainingPattern(value as TrainingPattern);
                    }} value={trainingPattern}>
                        <Radio style={{
                            border: '1px solid #d9d9d9',
                            borderRadius: 8,
                            padding: 10,
                            marginRight: 10
                        }} value={TrainingPattern.Subsection}>文本拆分</Radio>
                        <Radio style={{
                            border: '1px solid #d9d9d9',
                            borderRadius: 8,
                            padding: 10,
                            marginRight: 10
                        }} value={TrainingPattern.QA}>AI文本拆分</Radio>
                    </Radio.Group>
                    {
                        trainingPattern === TrainingPattern.Subsection && <div>
                            <span>处理模式：</span>
                            <Radio.Group onChange={(v: any) => {
                                const value = Number(v.target.value);
                                setProcessMode(value as ProcessMode);
                            }} value={processMode}>
                                <Radio value={ProcessMode.Auto}>自动</Radio>
                                <Radio value={ProcessMode.Custom}>自定义</Radio>
                            </Radio.Group>
                            {
                                processMode === ProcessMode.Custom && <>
                                <div style={{
                                    marginTop: 10
                                }}>
                                    <span>段落最大Token：</span>
                                    <Input
                                        value={maxTokensPerParagraph}
                                        onChange={(e: any) => {
                                            setMaxTokensPerParagraph(Number(e.target.value));
                                        }}
                                        style={{
                                            width: 200,
                                            marginRight: 10
                                        }} />

                                    <span>每行最大Tokens：</span>
                                    <Input
                                        value={maxTokensPerLine}
                                        onChange={(e: any) => {
                                            setMaxTokensPerLine(Number(e.target.value));
                                        }}
                                        style={{
                                            width: 200,
                                            marginRight: 10
                                        }} />
                                    <span>段落之间重叠标记的数目：</span>
                                    <Input
                                        value={overlappingTokens}
                                        onChange={(e: any) => {
                                            setOverlappingTokens(Number(e.target.value));
                                        }}
                                        style={{
                                            width: 200,
                                            marginRight: 10
                                        }} />
                                </div>
                            </>
                            }

                        </div>
                    }
                    {
                        trainingPattern === TrainingPattern.QA && <>
                            <div>AI文本拆分提示词：</div>
                            <TextArea 
                            style={{
                                height: 200
                            }}
                            value={qAPromptTemplate} onChange={(v) => {
                                setQAPromptTemplate(v.target.value);
                            }}>
                            </TextArea>
                        </>
                    }
                </div>

                <div style={{
                    height: 'calc(100vh - 400px)',
                    overflow: 'auto',
                }}>
                    {webs.map((item, index) => {
                        return <FileItem key={index}>
                            {item}
                            <Button size='small' style={{
                                float: 'right'
                            }} onClick={() => {
                                const newWebs = webs.filter((_, i) => i !== index);
                                setWebs(newWebs);
                            }}>删除</Button>
                        </FileItem>
                    })}
                </div>
                <Button type='primary' onClick={() => {
                    upload();
                }} style={{
                    float: 'right',
                    marginTop: 20,
                    marginLeft: 20,
                }}>提交数据（{webs.length}）</Button>
                <Button onClick={() => {
                    setCurrent(0);
                }} style={{
                    float: 'right',
                    marginTop: 20,
                }}>上一步</Button>
            </>
        }
    </>)
}