import { memo, useEffect, useState } from "react";
import { getModels } from "../../../store/Model";
import { Select, Row, Checkbox, Button, Collapse, Col, Slider, message } from 'antd';
import styled from 'styled-components';
import { ChatApplicationDto } from "../../../models";
import { PutChatApplications } from "../../../services/ChatApplicationService";
import { GetWikisList } from "../../../services/WikiService";

interface IAppDetailInfoProps {
    value: ChatApplicationDto
}

const Container = styled.div`
    display: grid;
    padding: 20px; 
    /* 屏幕居中显示 */
    margin: auto;
    width: 580px;
    overflow: auto;
    height: 100%;

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

    const [model, setModel] = useState([] as any[]);
    const [wiki, setWiki] = useState([] as any[]);
    const [input,] = useState({
        keyword: '',
        page: 1,
        pageSize: 10
    } as any);

    useEffect(() => {
        getModels()
            .then((models) => {
                setModel(models.chatModel.map((item) => {
                    return { label: item.label, value: item.value }
                }));
            });

        loadingWiki();

    }, []);

    function loadingWiki() {
        // TODO: 暂时写死
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
                <Select
                    defaultValue={application.chatModel}
                    value={application.chatModel}
                    style={{ width: 380 }}
                    onChange={(v) => {
                        setApplication({
                            ...application,
                            chatModel: v
                        });
                    }}
                    options={model}
                />
            </ListItem>
            <ListItem>
                <span style={{
                    fontSize: 20,
                    marginRight: 20
                }}>开场白</span>
                <textarea value={application?.opener??""}
                    onChange={(e) => {
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
                    onChange={(e) => {
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
                    onChange={(e) => {
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
                onChange={(v) => {
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
                                    onChange={(e) => {
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
                                    onChange={(e) => {
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
                                    onChange={(e) => {
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
                                    onChange={(e) => {
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
                                onChange={(e) => {
                                    setApplication({
                                        ...application,
                                        template: e.target.value
                                    });
                                }}
                                style={{ width: 380, resize: "none", height: '200px' }}>

                            </textarea>
                        </ListItem>
                    </>
                }]}
            />
            <Checkbox defaultChecked={application.showSourceFile} checked={application.showSourceFile} value={application.showSourceFile} onChange={(v) => {
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