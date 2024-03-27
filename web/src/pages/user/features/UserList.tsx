import { Table } from 'antd';
import { useEffect, useState } from 'react';
import { DeleteUser, DisableUser, GetUsers, UpdateUserRole } from '../../../services/UserService';

import { Button, Dropdown, MenuProps,message } from 'antd';
import { RoleType } from '../../../models/index.d';

interface IUserListProps {
    keyword: string;
}

export default function UserList({
    keyword
}:IUserListProps) {
    const [input, setInput] = useState({
        page: 1,
        pageSize: 10
    });
    const [total, setTotal] = useState(0);
    const [data, setData] = useState([]);
    async function handleTableChange(page: number, pageSize: number) {
        setInput({
            ...input,
            page: page,
            pageSize: pageSize,
        });
        
        await loadingData();
    }

    const columns = [
        {
            title: '账户',
            dataIndex: 'account',
            key: 'account',
        },
        {
            title: '昵称',
            dataIndex: 'name',
            key: 'name',
        },
        {
            title: '邮箱',
            dataIndex: 'email',
            key: 'email',
        },
        {
            title: '手机号',
            dataIndex: 'phone',
            key: 'phone',
        },
        {
            title: '是否禁用',
            dataIndex: 'isDisable',
            key: 'isDisable',
            render: (text: boolean) => text ? '是' : '否'
        },
        {
            title: '角色',
            dataIndex: 'roleName',
            key: 'roleName'
        },
        {
            title: '操作',
            dataIndex: 'acting',
            key: 'acting',
            render: (_: any, item: any) => {
                const items: MenuProps['items'] = [];
                items.push({
                    key: '1',
                    label: '编辑',
                    onClick: () => {
                        console.log('编辑');
                    }
                })
                if (item.role === RoleType.Admin) {
                    items.push({
                        key: '4',
                        label: '取消管理员',
                        onClick: async () => {
                            await UpdateUserRole(item.id, RoleType.User);
                            message.success('取消成功');
                            await ResetLoading();
                        }
                    })
                } else {

                    if (!item.isDisable) {
                        items.push({
                            key: '3',
                            label: '禁用',
                            onClick: async () => {
                                await DisableUser(item.id, true);
                                message.success('禁用成功');
                                await ResetLoading();
                            }
                        })
                    } else {
                        items.push({
                            key: '3',
                            label: '启用',
                            onClick: async () => {
                                await DisableUser(item.id, false);
                                message.success('启用成功');
                                await ResetLoading();
                            }
                        })
                    }

                    items.push({
                        key: '2',
                        label: '删除',
                        onClick: async () => {
                            await DeleteUser(item.id);
                            message.success('删除成功');
                            await ResetLoading();
                        }
                    })

                    items.push({
                        key: '4',
                        label: '设为管理员',
                        onClick: async () => {
                            await UpdateUserRole(item.id, RoleType.Admin);
                            message.success('设置成功');
                            await ResetLoading();
                        }
                    })
                }
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

    async function ResetLoading(){
        setInput({
            ...input,
            page: 1,
            pageSize: 10
        });
        await loadingData();
    }

    async function loadingData() {
        try {
            const result = await GetUsers(keyword, input.page, input.pageSize)
            setData(result.result);
            setTotal(result.total);
        } catch (error) {

        }
    }

    useEffect(() => {
        loadingData();
    }, [keyword]);

    useEffect(() => {
        loadingData();
    }, []);


    return (
        <Table
            pagination={{
                current: input.page,
                pageSize: input.pageSize,
                total: total,
                onChange: handleTableChange,
            }} scroll={{ y: 'calc(100vh - 240px)' }}
            columns={columns}
            dataSource={data} />
    )
}