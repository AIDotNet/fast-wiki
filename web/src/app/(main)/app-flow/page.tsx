"use client"


import ReactFlow, {
    Controls,
    applyNodeChanges,
    applyEdgeChanges,
    Background,
    Node,
    addEdge,
    Panel,
} from 'reactflow';
import './index.css'
import { Button, Card, Drawer, List, message } from 'antd'
import { useState, useCallback, useEffect } from 'react';
import 'reactflow/dist/style.css';
import nodeTypes, { getList } from './plugin';
import { GetChatApplications, PutChatApplications } from '@/services/ChatApplicationService';
import { uuid } from '@/utils/uuid';

export default function AppFlow() {

    const query = new URLSearchParams(window.location.search);
    const id = query.get('id');
    const [application, setApplication] = useState({} as any);

    const initialNodes = [] as Node[];

    const initialEdges = [{ id: 'e1-2', source: '1', target: '2' }, { id: 'e2-3', source: '2', target: '3' }];

    const [nodes, setNodes] = useState(initialNodes);
    const [edges, setEdges] = useState(initialEdges);
    const [showTools, setShowTools] = useState(false);
    const tools = getList()

    const onNodesChange = useCallback(
        (changes) => setNodes((nds) => applyNodeChanges(changes, nds)),
        [],
    );
    const onEdgesChange = useCallback(
        (changes) => setEdges((eds) => applyEdgeChanges(changes, eds) as any),
        [],
    );

    const onConnect = useCallback(
        (params) => setEdges((eds) => addEdge(params, eds) as any),
        [],
    );

    useEffect(() => {
        if (id) {
            loadingApplication();
        }
    }, [id]);

    async function loadingApplication() {
        if (id) {
            GetChatApplications(id as string)
                .then((application) => {
                    setApplication(application);
                });
        }
    }

    const onApplication = (value: any) => {
        setApplication({
            ...application,
            value
        });
    };

    useEffect(() => {
        setNodes([
            {
                id: '1',
                data: { application: application },
                position: { x: 0, y: 0 },
                type: 'star',
            },
            {
                id: '2',
                data: { application: application, onApplication: onApplication },
                position: { x: 180, y: 300 },
                type: 'wikiSearch',
            },
            {
                id: '3',
                data: { application: application, onApplication: onApplication },
                position: { x: 180, y: 850 },
                type: 'aiChat',
            }
        ]);
    }, [application]);

    function onSave() {
        PutChatApplications(application)
            .then(() => {
                message.success('保存成功');
            });
    }

    return (
        <div style={{
            height: '100%',
            width: '100%',
            overflow: 'hidden'
        }}>
            <div style={{
                width: '100%',
                height: '45px',
            }}>
                <h2 style={{
                    paddingTop: '15px',
                    paddingLeft: '20px',
                    float: 'left',
                }}>
                    高级流程编排
                </h2>

                <div style={{
                    float: 'right',
                    paddingTop: '15px',
                    paddingRight: '20px',
                }}>
                    <Button onClick={() => { onSave() }}>保存</Button>
                </div>
            </div>
            <div style={{
                height: 'calc(100vh - 45px)',
                width: '100%',
            }}>
                <ReactFlow
                    nodes={nodes}
                    onNodesChange={onNodesChange}
                    edges={edges}
                    onEdgesChange={onEdgesChange}
                    onConnect={onConnect}
                    nodeTypes={nodeTypes}
                    fitView
                >
                    <Background />
                    <Panel position='top-left'>
                        <Button onClick={() => {
                            setShowTools(true);
                        }}>
                            添加
                        </Button>
                    </Panel>
                    <Drawer
                        title="工具栏"
                        placement='left'
                        onClose={() => {
                            setShowTools(false);
                        }}
                        open={showTools}
                        getContainer={false}
                    >
                        {
                            tools.map((tool) => {
                                return (
                                    <div>
                                        <Card title={tool.label}>
                                            <div>
                                                {tool.children.map(x=>{
                                                    return(<div className='tool-item'>
                                                        {x.label}
                                                        <Button size='small'  className='tool-item-add' onClick={() => {
                                                            setNodes((n) => n.concat([{
                                                                id: uuid(),
                                                                data: { application: application },
                                                                position: { x: 0, y: 0 },
                                                                type: x.type,
                                                            }]));
                                                        }
                                                        }>+</Button>
                                                    </div>)
                                                })}
                                            </div>
                                        </Card>
                                    </div>
                                );
                            })
                        }
                    </Drawer>
                    <Controls showInteractive={false} />
                    <svg>
                        <defs>
                            <linearGradient id="edge-gradient">
                                <stop offset="0%" stopColor="#ae53ba" />
                                <stop offset="100%" stopColor="#2a8af6" />
                            </linearGradient>
                            <marker
                                id="edge-circle"
                                viewBox="-5 -5 10 10"
                                refX="0"
                                refY="0"
                                markerUnits="strokeWidth"
                                markerWidth="10"
                                markerHeight="10"
                                orient="auto"
                            >
                                <circle stroke="#2a8af6" strokeOpacity="0.75" r="2" cx="0" cy="0" />
                            </marker>
                        </defs>
                    </svg>
                </ReactFlow>
            </div>
        </div>
    );
}