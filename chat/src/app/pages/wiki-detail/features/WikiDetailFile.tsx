import { Markdown, Modal, SpotlightCard } from "@lobehub/ui";
import { useEffect, useState } from "react";
import { Button, message } from "antd";

import { Flexbox } from 'react-layout-kit';
import { DelDetailsVector, GetWikiDetailVectorQuantity } from "@/services/WikiService";


interface IWikiDetailProps {
    wikiDetail: any;
    visible: boolean;
    onClose: () => void;
}

export default function WikiDetailFile({
    wikiDetail,
    visible,
    onClose
}: IWikiDetailProps) {

    const [showValue, setShowValue] = useState('')

    const [wikiDetailInfo, setWikiDetailInfo] = useState([] as any[]);
    const [taotal, setTotal] = useState(0);
    const [input, setInput] = useState({
        page: 1,
        pageSize: 10,
        wikiId: wikiDetail.id
    } as any);

    useEffect(() => {
        if (visible) {
            loadingWikiDetail();
            setInput({
                ...input,
                wikiId: wikiDetail.id
            });
        }

    }, [wikiDetail, visible]);

    function loadingWikiDetail() {
        if (wikiDetail.id === undefined) return;
        GetWikiDetailVectorQuantity(wikiDetail.id, input.page, input.pageSize)
            .then((wikiDetail) => {
                setWikiDetailInfo(wikiDetail.result);
                setTotal(wikiDetail.total);
            });
    }

    async function removeWikiDetail(id: string) {
        try {
            await DelDetailsVector(id)
            message.success('删除成功');
            loadingWikiDetail();
        } catch (error) {
            message.error('删除失败');
        }
    }

    function renderItem(item: any) {
        return <Flexbox align={'flex-start'} gap={8} horizontal style={{ padding: 16, height: 200 }}>
            <Flexbox>
                <div style={{ fontSize: 15, fontWeight: 600 }}>{item.document_Id} #({item.index})</div>
                <div style={{ opacity: 0.6, fontSize: 12 }}>
                    {item.content}
                </div>
            </Flexbox>
            <Button onClick={() => removeWikiDetail(item.id)} type='primary' size='small' style={{
                position: 'absolute',
                right: 16,
                bottom: 16
            }}>删除</Button>
            <Button onClick={() => {
                setShowValue(item.content)
            }} type='primary' size='small' style={{
                position: 'absolute',
                bottom: 16
            }}>查看</Button>
        </Flexbox>
    }

    return (
        <Modal width={'100%'} title='知识库详情' onCancel={onClose} open={visible} footer={[]}>
            <div style={{
                marginBottom: 16,
            }}>
                <span>{wikiDetail.fileName}</span>
                <Button style={{
                    float: 'inline-end',
                    position: 'absolute',
                    right: 16,
                }} onClick={() => { }}>插入</Button>
            </div>
            <SpotlightCard renderItem={renderItem} items={wikiDetailInfo}>
            </SpotlightCard>
            {taotal}

            <Modal
                title='查看'
                onCancel={() => setShowValue('')}
                footer={[]}
                open={showValue !== ''}
            >
                <Markdown>
                    {showValue}
                </Markdown>
            </Modal>
        </Modal>
    )
}
