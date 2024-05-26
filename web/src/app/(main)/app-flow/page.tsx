"use client"
import ReactFlow, {
    Controls,
    applyNodeChanges,
    applyEdgeChanges,
    Background,
    Node,
    addEdge,
} from 'reactflow';
import { Divider } from 'antd'
import { useState, useCallback, useEffect } from 'react';
import 'reactflow/dist/style.css';
import nodeTypes from './plugin';
import { GetChatApplications } from '@/services/ChatApplicationService';

export default function AppFlow() {


    const query = new URLSearchParams(window.location.search);
    const id = query.get('id');
    const [application, setApplication] = useState({} as any);

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


    const initialNodes = [
        {
            id: '1',
            data: { label: 'Hello' },
            position: { x: 0, y: 0 },
            type: 'star',
        },
        {
            id: '2',
            data: { label: 'World' },
            position: { x: 200, y: 100 },
            type: 'wikiSearch',
        },
    ] as Node[];

    const initialEdges = [
        { id: '1-2', source: '1', target: '2', label: 'to the', type: 'step' },
    ];

    const [nodes, setNodes] = useState(initialNodes);
    const [edges, setEdges] = useState(initialEdges);

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

    return (
        <div style={{
            height: '100%',
            width: '100%',
            overflow: 'hidden'
        }}>
            <div style={{
                width: '100%',
            }}>
                <h2>
                    App Flow
                </h2>

            </div>
            <Divider />
            <div style={{
                height: 'calc(100vh - 75px)',
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
                    <Controls />
                </ReactFlow>
            </div>
        </div>
    );
}