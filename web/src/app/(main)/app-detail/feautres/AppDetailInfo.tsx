import { memo, useEffect, useState } from "react";
import { AutoComplete, Row, Select, Checkbox, Button, Collapse, Col, Slider, message } from 'antd';
import styled from 'styled-components';
import { Input } from "@lobehub/ui";
import { FunctionCallSelect } from "@/services/FunctionService";
import { GetWikisList } from "@/services/WikiService";
import { PutChatApplications } from "@/services/ChatApplicationService";
import { set } from "lodash";
import { getModels } from "@/utils/model";

interface IAppDetailInfoProps {
    value: any;
}

const Container = styled.div`
    display: grid;
    padding: 20px; 
    /* 屏幕居中显示 */
    margin: auto;
    width: 580px;
    overflow: auto;
    height: calc(100vh - 60px);
    // 隐藏滚动条
    &::-webkit-scrollbar {
        display: none;
    }
    scrollbar-width: none;

`;

const ListItem = styled.div`
    display: flex;
    justify-content: space-between;
    padding: 20px;
    width: 100%;
`;

const AppDetailInfo = memo(({ value }: IAppDetailInfoProps) => {
    if (value === undefined) return null;

    const [selectChatModel, setSelectChatModel] = useState([] as any[]);
    const [wiki, setWiki] = useState([] as any[]);
    const [functionCallSelect, setFunctionCallSelect] = useState([] as any[]);
    const [input,] = useState({
        keyword: '',
        page: 1,
        pageSize: 10
    } as any);

    useEffect(() => {
        const models = getModels();
        setSelectChatModel(models.chatModel.map((item) => {
            return { label: item.label, value: item.value }
        }))
        loadingWiki();
        loadFunctionCallSelect();

    }, []);

    function loadFunctionCallSelect() {
        FunctionCallSelect()
            .then((value) => {
                setFunctionCallSelect(value);
            });
    }

    function loadingWiki() {
        GetWikisList(input.keyword, input.page, 100)
            .then((wiki) => {
                setWiki(wiki.result);
            });
    }

    const [application, setApplication] = useState(value);

    useEffect(() => {
        setApplication(value);
    }, [value]);

    function save() {
        PutChatApplications(application)
            .then(() => {
                message.success('保存成功');
            });
    }

    return (
        <Container>
            <ListItem>
                <span style={{
                    fontSize: 20,
                    marginRight: 20
                }}>对话模型</span>
                <AutoComplete
                    defaultValue={application.chatModel}
                    value={application.chatModel}
                    style={{ width: 380 }}
                    onChange={(v: any) => {
                        setApplication({
                            ...application,
                            chatModel: v
                        });
                    }}
                    options={selectChatModel}
                />
            </ListItem>

            <ListItem>
                <span style={{
                    fontSize: 20,
                    marginRight: 20
                }}>开场白</span>
                <textarea value={application?.opener ?? ""}
                    onChange={(e: any) => {
                        setApplication({
                            ...application,
                            opener: e.target.value
                        });
                    }}
                    style={{ width: 380, resize: "none", height: '200px' }}>

                </textarea>
            </ListItem>
            <ListItem>
                <span style={{
                    fontSize: 20,
                    marginRight: 20
                }}>提示词</span>
                <textarea value={application.prompt}
                    defaultValue={application.prompt}
                    onChange={(e: any) => {
                        setApplication({
                            ...application,
                            prompt: e.target.value
                        });
                    }}
                    style={{ width: 380, resize: "none", height: '200px' }}>

                </textarea>
            </ListItem>
            <ListItem>
                <span style={{
                    marginRight: 20
                }}>未找到回复模板</span>
                <textarea value={application.noReplyFoundTemplate ?? ''}
                    defaultValue={application.noReplyFoundTemplate ?? ''}
                    onChange={(e: any) => {
                        setApplication({
                            ...application,
                            noReplyFoundTemplate: e.target.value
                        });
                    }}
                    style={{ width: 380, resize: "none", height: '200px' }}>

                </textarea>
            </ListItem>
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
            <Collapse
                items={[{
                    key: '1', label: '高级设置', children: <>

                        <Row style={{
                            marginLeft: 20,
                            marginTop: 20,

                        }}>
                            <span style={{
                                marginRight: 20
                            }}>温度（AI智商）</span>
                            <Col span={12}>
                                <Slider
                                    min={0}
                                    max={1}
                                    step={0.1}
                                    defaultValue={application.temperature}
                                    onChange={(e: any) => {
                                        setApplication({
                                            ...application,
                                            temperature: e
                                        });
                                    }}
                                    value={application.temperature}
                                />
                            </Col>
                        </Row>
                        <Row style={{
                            marginLeft: 20,
                            marginTop: 20,

                        }}>
                            <span style={{
                                marginRight: 20
                            }}>响应token上限</span>
                            <Col span={12}>
                                <Slider
                                    max={128000}
                                    min={100}
                                    defaultValue={application.maxResponseToken}
                                    step={1}
                                    onChange={(e: any) => {
                                        setApplication({
                                            ...application,
                                            maxResponseToken: e
                                        });
                                    }}
                                    value={application.maxResponseToken}
                                />
                            </Col>
                        </Row>
                        <Row style={{
                            marginLeft: 20,
                            marginTop: 20,
                        }}>
                            <span style={{
                                marginRight: 20
                            }}>引用token上限</span>
                            <Col span={12}>
                                <Slider
                                    min={100}
                                    max={128000}
                                    step={1}
                                    defaultValue={application.referenceUpperLimit}
                                    onChange={(e: any) => {
                                        setApplication({
                                            ...application,
                                            referenceUpperLimit: e
                                        });
                                    }}
                                    value={application.referenceUpperLimit}
                                />
                            </Col>
                        </Row>
                        <Row style={{
                            marginLeft: 20,
                            marginTop: 20,

                        }}>
                            <span style={{
                                marginRight: 20
                            }}>向量匹配相似</span>
                            <Col span={12}>
                                <Slider
                                    min={0}
                                    max={1}
                                    defaultValue={application.relevancy}
                                    step={0.05}
                                    onChange={(e: any) => {
                                        setApplication({
                                            ...application,
                                            relevancy: e
                                        });
                                    }}
                                    value={application.relevancy}
                                />
                            </Col>
                        </Row>

                        <ListItem>
                            <span style={{
                                marginRight: 20
                            }}>引用模板提示词</span>
                            <textarea value={application.template}
                                defaultValue={application.template}
                                onChange={(e: any) => {
                                    setApplication({
                                        ...application,
                                        template: e.target.value
                                    });
                                }}
                                style={{ width: 380, resize: "none", height: '200px' }}>

                            </textarea>
                        </ListItem>
                    </>
                }, {
                    key: '2',
                    label: '飞书设置',
                    children: <>
                        <Row style={{
                            marginLeft: 20,
                            marginTop: 20,
                        }}>
                            <span style={{
                                marginRight: 20
                            }}>飞书机器人名称</span>
                            <Col span={12}>
                                <Input value={application.extend?.BotName ?? ""}
                                    defaultValue={application.extend?.BotName ?? ""}
                                    onChange={(e: any) => {
                                        setApplication({
                                            ...application,
                                            extend: {
                                                ...application.extend,
                                                BotName: e.target.value
                                            }
                                        });
                                    }}
                                    style={{ width: 380 }}></Input>
                            </Col>
                        </Row>
                        <Row style={{
                            marginLeft: 20,
                            marginTop: 20,
                        }}>
                            <span style={{
                                marginRight: 20
                            }}>飞书AppId</span>
                            <Col span={12}>
                                <Input value={application.extend?.FeishuAppId ?? ""}
                                    defaultValue={application.extend?.FeishuAppId ?? ""}
                                    onChange={(e: any) => {
                                        setApplication({
                                            ...application,
                                            extend: {
                                                ...application.extend,
                                                FeishuAppId: e.target.value
                                            }
                                        });
                                    }}
                                    style={{ width: 380 }}></Input>
                            </Col>
                        </Row>
                        <Row style={{
                            marginLeft: 20,
                            marginTop: 20,
                        }}>
                            <span style={{
                                marginRight: 20
                            }}>飞书AppSecret</span>
                            <Col span={12}>
                                <Input value={application.extend?.FeishuAppSecret ?? ""}
                                    defaultValue={application.extend?.FeishuAppSecret ?? ""}
                                    onChange={(e: any) => {
                                        setApplication({
                                            ...application,
                                            extend: {
                                                ...application.extend,
                                                FeishuAppSecret: e.target.value
                                            }
                                        });
                                    }}
                                    style={{ width: 380 }}></Input>
                            </Col>
                        </Row>
                    </>
                }, {
                    key: '3', label: "关联FunctionCall",
                    children: <>
                        <Select
                            allowClear
                            style={{
                                width: '100%',
                                marginTop: 20,
                                marginBottom: 20
                            }}
                            placeholder="绑定FunctionCall"
                            defaultValue={application.functionIds}
                            value={application.functionIds}
                            onChange={(v: any) => {
                                setApplication({
                                    ...application,
                                    functionIds: v
                                });
                            }}
                            options={functionCallSelect.map((item) => {
                                return {
                                    label: item.name,
                                    value: item.id
                                }
                            })}
                            mode="multiple"
                        >
                        </Select>
                    </>
                }]}
            />

            <Checkbox defaultChecked={application.showSourceFile} checked={application.showSourceFile} value={application.showSourceFile} onChange={(v: any) => {
                setApplication({
                    ...application,
                    showSourceFile: v.target.checked
                });
            }}>是否启用引用来源显示</Checkbox>
            <Button block onClick={save} style={{
                marginTop: 20
            }}>
                保存修改
            </Button>
        </Container>
    )
})

export default AppDetailInfo;