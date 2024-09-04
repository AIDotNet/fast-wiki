import { useEffect, useState } from "react";
import { Table } from "antd";
import { Radio, Button } from "antd";
import { EditableMessage, Input, TextArea } from "@lobehub/ui";
import { UploadFile } from "@/services/StorageService";
import { CreateWikiDetails } from "@/services/WikiService";
import { ProcessMode, TrainingPattern } from './index.d';

interface IUploadWikiDataProps {
    id: string;
    onChagePath(key: any): void;
}

export default function UploadWikiData(props: IUploadWikiDataProps) {
    const [editing, setEdit] = useState(true)
    const [content, setContent] = useState('');
    
    const [processMode, setProcessMode] = useState(ProcessMode.Auto);
    const [trainingPattern, setTrainingPattern] = useState(TrainingPattern.Subsection);
    const [maxTokensPerParagraph, setMaxTokensPerParagraph] = useState(1000); // 每个段落标记的最大数量。当对文档进行分区时，每个分区通常包含一个段落。
    const [maxTokensPerLine, setMaxTokensPerLine] = useState(300); // 每行，也就是每个句子的最大标记数。当分割一个文本块时，文本将被分割成句子，然后被分组成段落。注意，这适用于任何文本格式，包括表格、代码、聊天记录、日志文件等。
    const [overlappingTokens, setOverlappingTokens] = useState(100); // 重叠标记数。当对文档进行分区时，每个分区的开始和结束部分将重叠。这有助于确保模型在分区之间保持上下文一致性。
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

    async function save() {
        // 将content转换成File对象
        const file = new File([content], 'data.txt', { type: 'text/plain' });

        const fileItem = await UploadFile(file);

        await CreateWikiDetails({
            name: file.name,
            wikiId: props.id,
            fileId: fileItem.id,
            filePath: fileItem.path,
            maxTokensPerParagraph: maxTokensPerParagraph,
            maxTokensPerLine: maxTokensPerLine,
            overlappingTokens: overlappingTokens,
            qAPromptTemplate: qAPromptTemplate,
            mode: processMode,
            trainingPattern: trainingPattern
        })

        props.onChagePath('data-item');
    }

    return (
        <>

            <div >
                <Button onClick={() => {
                    props.onChagePath('data-item')
                }}>返回</Button>
            </div>

            <div style={{
                display: 'flex',
                padding: '20px',
            }}>
                <EditableMessage
                    styles={{
                        input: {
                            width: '100%',
                            height: '100%'
                        },
                        markdown: {
                            width: '100%',
                            height: '100%'
                        }
                    }}
                    editing={editing}
                    text={{
                        cancel: "取消",
                        confirm: "保存",
                        edit: "编辑",
                    }}
                    placeholder="请输入您的内容"
                    onEditingChange={() => {
                        // 
                        save();
                    }}
                    value={content}
                    onChange={setContent}
                />
            </div>

            <div style={{
                marginBottom: 20
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
                    }} value={TrainingPattern.QA}>
                        AI文本拆分
                    </Radio>
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
        </>
    );
}