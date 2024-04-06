import { Input, Modal } from "@lobehub/ui";
import { useEffect, useRef, useState } from "react";
import styled from "styled-components";
import * as monaco from 'monaco-editor/esm/vs/editor/editor.api';
import { Button, message } from 'antd';
import { CreateFunction } from "../../../services/FunctionService";
import { FastWikiFunctionCallInput } from "../../../models";

interface ICreateFunctionCallProps {
    visible: boolean;
    onSuccess: any;
    onCancel: any;

}


const SInput = styled(Input)`
    margin-bottom: 20px;
`

const SButton = styled(Button)`
    margin-top: 20px;
    width: 100%;
`


export default function CreateFunctionCall({
    visible,
    onSuccess,
    onCancel
}: ICreateFunctionCallProps) {
    const [editor, setEditor] = useState<monaco.editor.IStandaloneCodeEditor | null>(null);
    const monacoEl = useRef(null);
    useEffect(() => {
        if (visible) {
            setEditor((editor) => {
                // if (editor) return editor;
                // 判断是否有monacoEl，如果没有就返回
                if (!monacoEl.current) return editor;

                const e = monaco.editor.create(monacoEl.current!, {
                    value: ['function Test(x,y) {', '\treturn x+y;', '} '].join('\n'),
                    language: 'typescript',
                    theme: 'vs-dark',
                    automaticLayout: true,

                });


                return e;
            });
        }

        return () => editor?.dispose();
    }, [visible]);
    const [functionCall, setFunctionCall] = useState<FastWikiFunctionCallInput>({
        name: '',
        description: '',
        content: '',
        parameters: [],
        items: [],
        imports: [],
        main: ''
    });


    function onSumit() {
        // 判断parametersList是否有空的参数
        for (let i = 0; i < functionCall.parameters.length; i++) {
            if (functionCall.parameters[i].key === '' || functionCall.parameters[i].value === '') {
                message.error('参数名和参数描述不能为空');
                return;
            }
        }
        // 判断parameters是否有重复的key
        const keys = functionCall.parameters.map((item) => item.key);
        const set = new Set(keys);
        if (keys.length !== set.size) {
            message.error('参数名不能重复');
            return;
        }

        functionCall.content = editor?.getValue() || '';

        // 判断描述
        if (functionCall.name === '' || functionCall.description === '' || functionCall.content === '') {
            message.error('函数名称、函数描述、函数内容不能为空');
            return;
        }
        CreateFunction(functionCall)
            .then(() => {
                message.success('创建成功');
                onSuccess();
            })
            .catch(() => {
                message.error('创建失败');
            });
    }

    return (
        <Modal allowFullscreen={true} open={visible} title="创建函数调用"
            footer={null}
            onCancel={() => onCancel()}
        >
            <SInput
                value={functionCall.name}
                onChange={(e: any) => {
                    setFunctionCall((functionCall) => {
                        return {
                            ...functionCall,
                            name: e.target.value
                        }
                    });
                }}
                size='large' placeholder="请输入函数名称" />

            <SInput
                value={functionCall.description}
                onChange={(e: any) => {
                    setFunctionCall((functionCall) => {
                        return {
                            ...functionCall,
                            description: e.target.value
                        }
                    });
                }}
                size='large' placeholder="请输入函数描述(尽可能的描述这个function的作用描述会影响到Function的调用！)" />
            <SInput
                value={functionCall.main}
                onChange={(e: any) => {
                    setFunctionCall((functionCall) => {
                        return {
                            ...functionCall,
                            main: e.target.value
                        }
                    });
                }}
                size='large' placeholder="请输入Main函数名称" />
            <div>
                <span>
                    Function JS代码。
                </span>
                <div style={{
                    height: '400px',
                    width: '100%',
                    border: '1px solid #e8e8e8',
                    borderRadius: '5px',
                    marginTop: '10px'
                }} ref={monacoEl}>

                </div>
            </div>
            <SButton onClick={
                () => {
                    // 判断是否有空的参数
                    for (let i = 0; i < functionCall.parameters.length; i++) {
                        if (functionCall.parameters[i].key === '' || functionCall.parameters[i].value === '') {
                            message.error('参数名和参数描述不能为空');
                            return;
                        }
                    }
                    // 添加Function参数描述
                    setFunctionCall((functionCall) => {
                        return {
                            ...functionCall,
                            parameters: functionCall.parameters.concat({
                                key: '',
                                value: ''
                            } as any)
                        }
                    });
                }
            } block >
                添加Function参数描述
            </SButton>
            {
                // 渲染parametersList
                functionCall.parameters.map((item, index) => {
                    return (
                        <div key={index} style={{
                            display: 'flex',
                            justifyContent: 'space-between',
                            marginTop: '10px'
                        }}>
                            <SInput size='large'
                                value={item.key}
                                onChange={(e: any) => {
                                    setFunctionCall((functionCall) => {
                                        const parametersList = functionCall.parameters;
                                        parametersList[index].key = e.target.value;
                                        return {
                                            ...functionCall,
                                            parametersList
                                        }
                                    });
                                }}
                                placeholder="请输入参数名" />
                            <SInput size='large'
                                value={item.value}
                                onChange={(e: any) => {
                                    setFunctionCall((functionCall) => {
                                        const parametersList = functionCall.parameters;
                                        parametersList[index].value = e.target.value;
                                        return {
                                            ...functionCall,
                                            parametersList
                                        }
                                    });
                                }}
                                placeholder="请输入参数描述" />
                            <Button onClick={() => {
                                setFunctionCall((functionCall) => {
                                    const parametersList = functionCall.parameters;
                                    parametersList.splice(index, 1);
                                    return {
                                        ...functionCall,
                                        parametersList
                                    }
                                });
                            }} type="primary" danger>
                                删除
                            </Button>
                        </div>
                    )
                })
            }
            <SButton onClick={() => onSumit()} block>
                保存
            </SButton>
        </Modal>
    );
}