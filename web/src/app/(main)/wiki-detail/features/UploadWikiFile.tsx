
import { Button, Steps, Upload, UploadProps, Table, Progress, Radio, Input, message, MenuProps, Dropdown } from 'antd';
import { useState } from 'react';
import { InboxOutlined, CloseOutlined } from '@ant-design/icons';
import styled from 'styled-components';
import { TextArea } from '@lobehub/ui';
import { CreateWikiDetails } from '@/services/WikiService';
import { UploadFile } from '@/services/StorageService';
import { bytesToSize } from '@/utils/imageToBase64';
import { ProcessMode, TrainingPattern } from './index.d';


const FileItem = styled.div`
    transition: border-color 0.3s linear;
    border: 1px solid #d9d9d9;
    border-radius: 8px;
    padding: 10px;
    margin-right: 10px;
    display: flex;
    cursor: pointer;
    margin-bottom: 10px;
    &:hover {
        border-color: #1890ff;
        transition: border-color 0.3s linear;
        box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
    }
`;

const { Dragger } = Upload;

interface IUploadWikiFileProps {
    id: string;
    onChagePath(key: any): void;
}

export default function UploadWikiFile({ id, onChagePath }: IUploadWikiFileProps) {
    const [current, setCurrent] = useState(0);
    // const [uploading, setUploading] = useState(false);
    const [fileList, setFileList] = useState<any[]>([]);
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
    const props: UploadProps = {
        name: 'file',
        multiple: true,
        showUploadList: false,
        accept: '.md,.pdf,.docs,.txt,.json,.excel,.word,.html',
        beforeUpload: (file: any) => {
            fileList.push(file);
            setFileList([...fileList]);
            return false;
        }
    };

    const columns = [
        {
            title: '文件名',
            dataIndex: 'fileName',
            key: 'fileName',
        },
        {
            title: '文件上传进度',
            dataIndex: 'progress',
            key: 'progress',
            render: (value: number) => {
                return <Progress percent={value} size="small" />
            }
        },
        {
            title: '数据上传进度',
            dataIndex: 'dataProgress',
            key: 'dataProgress',
            render: (value: number) => {
                return <Progress percent={value} size="small" />
            }
        },
        {
            title: '操作',
            dataIndex: 'handler',
            key: 'handler',
            render: (_: any, item: any) => {
                const items: MenuProps['items'] = [];
                items.push({
                    key: 'delete',
                    label: '删除',
                    onClick: () => {
                        setFileList([...fileList.filter((i) => i !== item)]);
                    }
                })
                return (
                    <>
                        <Dropdown menu={{ items }} trigger={['click']}>
                            <Button>操作</Button>
                        </Dropdown>
                    </>
                )
            }
        },
    ];

    function saveFile() {
        fileList.forEach(async (file) => {
            await Upload(file)
        });
        message.success('上传成功');
    }

    async function Upload(file: any) {

        const fileItem = await UploadFile(file);
        file.progress = 100;

        setFileList([...fileList]);

        await CreateWikiDetails({
            name: file.name,
            wikiId: id,
            fileId: fileItem.id,
            filePath: fileItem.path,
            maxTokensPerParagraph: maxTokensPerParagraph,
            maxTokensPerLine: maxTokensPerLine,
            overlappingTokens: overlappingTokens,
            qAPromptTemplate: qAPromptTemplate,
            mode: processMode,
            trainingPattern: trainingPattern
        })

        file.dataProgress = 100;

        setFileList([...fileList]);
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
                marginBottom: '10px',
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
                <Dragger {...props} style={{
                    padding: '20px',
                    border: '1px dashed #d9d9d9',
                    borderRadius: '2px',
                    justifyContent: 'center',
                    alignItems: 'center',
                    flexDirection: 'column'
                }} height={200}>
                    <p className="ant-upload-drag-icon">
                        <InboxOutlined />
                    </p>
                    <p className="ant-upload-text">点击或推动文件上传</p>
                    <p className="ant-upload-hint">
                        支持单个或批量上传，支持 .md .pdf .docs .txt .json .excel .word .html等格式,
                        最多支持1000个文件。单文件最大支持100M。
                    </p>
                </Dragger>
                <div style={{
                    padding: '20px',
                    display: 'flex',
                    flexWrap: 'wrap',
                    overflow: 'auto',
                    height: '200px',
                    alignContent: 'flex-start'
                }}>

                    {fileList.length > 0 && fileList.map((item, index) => {
                        return <FileItem>
                            <span>{item.name}</span>
                            <span style={{
                                marginLeft: 10
                            }}>
                                {bytesToSize(item.size || 0)}
                            </span>
                            <span style={{
                                marginLeft: 10
                            }}>
                                <CloseOutlined
                                    onClick={() => {
                                        setFileList(fileList.filter((_, i) => i !== index));
                                    }} />
                            </span>
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
                <Table dataSource={fileList.map(item => {
                    return {
                        fileName: item.name,
                        progress: item.progress || 0,
                        dataProgress: item.dataProgress || 0
                    }
                })} columns={columns} />
                <Button type='primary' onClick={() => {
                    saveFile();
                }} style={{
                    float: 'right',
                    marginTop: 20,
                    marginLeft: 20,
                }}>提交数据（{fileList.length}）</Button>
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