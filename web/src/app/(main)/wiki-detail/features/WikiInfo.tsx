import styled from 'styled-components';
import { AutoComplete } from 'antd';
import { useEffect, useState } from 'react';
import { Button, message, Input } from 'antd';
import { Tooltip } from '@lobehub/ui';
import { GetWikis, PutWikis } from '@/services/WikiService';
import { getModels } from '@/utils/model';

const Container = styled.div`
    display: grid;
    padding: 20px; 
    /* 屏幕居中显示 */
    margin: auto;
    width: 580px;
    overflow: auto;
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
    align-items: center;
`;

interface IWikiInfoProps {
    id: string;
}

export default function WikiInfo(props: IWikiInfoProps) {
    const [model, setModel] = useState([] as any[]);
    const [embeddingModel, setEmbeddingModel] = useState([] as any[]);
    const [wikiInfo, setWikiInfo] = useState({} as any);
    useEffect(() => {
        const models = getModels();

        setModel(models.chatModel.map((item) => {
            return { label: item.label, value: item.value }
        }));

        setEmbeddingModel(models.embeddingModel.map((item) => {
            return { label: item.label, value: item.value }
        }));

    }, []);

    useEffect(() => {
        loadingWiki();
    }, [props.id]);

    async function saveWiki() {
        try {
            await PutWikis(wikiInfo)
            message.success('保存成功');
        } catch (error) {
            message.error('保存失败');
        }
    }
    function loadingWiki() {
        GetWikis(props.id as string)
            .then((wiki) => {
                setWikiInfo(wiki);
            });
    }
    return (
        <Container>
            <ListItem>
                <span style={{
                    fontSize: 20,
                    marginRight: 20,
                    width: 100
                }}>名称</span>
                <Input value={wikiInfo.name} onChange={(e) => {
                    setWikiInfo({
                        ...wikiInfo,
                        name: e.target.value
                    });
                }}>
                </Input>
            </ListItem>
            <ListItem>
                <span style={{
                    fontSize: 20,
                    marginRight: 20
                }}>对话模型</span>
                <AutoComplete
                    defaultValue={wikiInfo.model}
                    value={wikiInfo.model}
                    style={{ width: 380 }}
                    onChange={(v) => {
                        setWikiInfo({
                            ...wikiInfo,
                            model: v
                        });
                    }}
                    options={model}
                />
            </ListItem>
            <ListItem>
                <Tooltip title="用于将内容量化的模型" trigger='hover'>
                    <span style={{
                        fontSize: 20,
                        marginRight: 20
                    }}>嵌入模型</span>
                </Tooltip>
                <AutoComplete
                    defaultValue={wikiInfo.embeddingModel}
                    value={wikiInfo.embeddingModel}
                    style={{ width: 380 }}
                    onChange={(v) => {
                        setWikiInfo({
                            ...wikiInfo,
                            embeddingModel: v
                        });
                    }}
                    options={embeddingModel}
                />
            </ListItem>
            <Button onClick={() => saveWiki()}>保存修改</Button>
        </Container>
    )
}
