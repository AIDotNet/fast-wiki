
import styled from 'styled-components';
import { Button } from 'antd';
import { Features, FeaturesProps } from '@lobehub/ui';
import {
    GithubOutlined, 
    OpenAIOutlined, 
    RadarChartOutlined, 
    CoffeeOutlined,
    BugOutlined
} from "@ant-design/icons";

const Title = styled.span`
    font-size: 40px;
    font-weight: bold;
    text-align: center;
    margin-top: 20px;
    margin-bottom: 20px;
    display: block;
`;

const SubTitle = styled.span`
    font-size: 20px;
    text-align: center;
    margin-top: 20px;
    margin-bottom: 20px;
    display: block;

`;

const GithubStar = styled.div`
    margin-bottom: 20px;
    display: flex;
    justify-content: center;
    align-items: center;
    width: 100%;
`;

const Slogan = styled.span`
    font-size: 20px;
    text-align: center;
    margin-top: 20px;
    margin-bottom: 20px;
    display: block;
`;

export function Publicity() {
    const items: FeaturesProps['items'] = [
        {
            description:
                'Fast Wiki项目代码开源，并且使用Apache-2.0协议，您可以自由使用、修改、分发Fast Wiki的代码。我们欢迎您的贡献！',
            icon: GithubOutlined as any,
            title: '更开放',
        },
        {
            description:
                '支持GPT,Clude,ChatGLM等多种模型，满足您的更复杂的需求',
            icon: OpenAIOutlined as any,
            title: '支持多种模型',
        },
        {
            description:
                '基于Semantic Kernel使您的应用更加智能的扩展，让您的知识库更加智能化',
            icon: RadarChartOutlined,
            title: '智能扩展',
        },
        {
            description:
                'Fast Wiki前端基于React+LobeHub UI，后端.NET8+MasaFramework，让代码更易维护和扩展',
            icon: CoffeeOutlined,
            title: '优雅的架构设计',
        },
        {
            description:
                '拥有搜索测试，引用修改，数据AI分析等多种功能，让您的调试更加直观',
            icon: BugOutlined,
            title: '可视化调试',
        },
    ];

    return (
        <>
            <Title>让您的知识更智能</Title>
            <SubTitle>基于LLM大模型的智能化管理平台</SubTitle>
            <GithubStar>
                <Button onClick={() => {
                    window.open('https://github.com/239573049/fast-wiki')
                }} style={{
                    display: 'flex',
                }}>
                    <div style={{
                        backgroundImage: "url('https://img.shields.io/github/stars/239573049%2Ffast-wiki')",
                        backgroundRepeat: 'no-repeat',
                        backgroundSize: 'contain',
                        width: '100px',
                        height: '35px',
                        cursor: 'pointer',
                    }}>
                    </div>
                </Button>
                <Button style={{
                    marginLeft: '20px',
                }} type='primary' onClick={()=>{
                    window.location.href = '/app';
                }}>立即开始</Button>
            </GithubStar>
            <Slogan >
                为什么使用Fast Wiki？
            </Slogan>
            <Features style={{
                margin: '20px',
                width: '96%',
                maxWidth: '96%',
            }} items={items} />
        </>
    );
}