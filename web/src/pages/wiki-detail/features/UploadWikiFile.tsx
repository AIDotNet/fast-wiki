
import { Button, Steps, Upload, UploadProps, Table, Progress, Radio, Input, message } from 'antd';
import { useState } from 'react';
import { InboxOutlined, CloseOutlined } from '@ant-design/icons';
import styled from 'styled-components';
import { bytesToSize } from '../../../utils/stringHelper';
import { ProcessMode, TrainingPattern } from '../../../models/index.d';
import { UploadFile as FileService } from '../../../services/StorageService';
import { CreateWikiDetails } from '../../../services/WikiService';


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
    const [uploading, setUploading] = useState(false);
    const [fileList, setFileList] = useState<any[]>([]);
    const [processMode, setProcessMode] = useState(ProcessMode.Auto);
    const [trainingPattern, setTrainingPattern] = useState(TrainingPattern.Subsection);
    const [subsection, setSubsection] = useState(400); // 分段长度
    const props: UploadProps = {
        name: 'file',
        multiple: true,
        showUploadList: false,
        accept: '.md,.pdf,.docs,.txt,.json,.excel,.word,.html',
        beforeUpload: (file, files) => {
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
    ];

    function saveFile() {
        fileList.forEach(async (file, index) => {
            const fileItem = await FileService(file);
            file.progress = 100;

            setFileList([...fileList]);

            await CreateWikiDetails({
                name: file.name,
                wikiId: id,
                fileId: fileItem.id,
                filePath: fileItem.path,
                subsection: subsection,
                mode: processMode,
                trainingPattern: trainingPattern
            })

            file.dataProgress = 100;

            setFileList([...fileList]);

        });
        message.success('上传成功');
    }

    return (<>
        <div >
            <Button onClick={() => {
                onChagePath(1)
            }}>返回</Button>
        </div>

        <Steps
            style={{
                marginTop: '20px',
                marginBottom: '20px',
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
                }} height={400}>
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
                    height: '300px',
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
                    height: 140,
                }}>
                    <Radio.Group style={{
                        marginBottom: 20
                    }} onChange={(v) => {
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
                        }} value={TrainingPattern.QA}>QA问答拆分</Radio>
                    </Radio.Group>
                    {
                        trainingPattern === TrainingPattern.Subsection && <div>
                            <span>处理模式：</span>
                            <Radio.Group onChange={(v) => {
                                const value = Number(v.target.value);
                                setProcessMode(value as ProcessMode);
                            }} value={processMode}>
                                <Radio value={ProcessMode.Auto}>自动</Radio>
                                <Radio value={ProcessMode.Custom}>自定义</Radio>
                            </Radio.Group>
                            {
                                processMode === ProcessMode.Custom && <div style={{
                                    marginTop: 10
                                }}>
                                    <span>理想：</span>
                                    <Input
                                        placeholder="请输入分段长度"
                                        value={subsection}
                                        onChange={(e) => {
                                            setSubsection(Number(e.target.value));
                                        }}
                                        style={{
                                            width: 200
                                        }} />
                                </div>
                            }

                        </div>
                    }
                    {
                        trainingPattern === TrainingPattern.QA && <div>
                            暂时不支持
                        </div>
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