'use client'
import UserList from "./features/UserList";

import styled from "styled-components";
import { Input } from "@lobehub/ui";
import { useState } from "react";

const UserWrapper = styled.div`
    padding: 20px;

`

export default function User(){
    const [keyword, setKeyword] = useState('');

    return(<UserWrapper>
        <div style={{
            display: 'flex',
            justifyContent: 'space-between',
            marginBottom: '20px',

        }}>
            <div style={{
                fontSize: '30px',
                fontWeight: 'bold',
                width: '200px',
                textAlign: 'center',

            }}>用户管理</div>
            <div style={{
                display: 'flex',
                justifyContent: 'space-between',
                width: '260px'
            }}>
                <Input value={keyword}
                onChange={(e) => setKeyword(e.target.value)}
                style={{
                    marginRight: '20px',
                    marginTop: '5px'
                }} placeholder="请输入账户/昵称/邮箱/手机号"/>
            </div>
        </div>
        <UserList keyword={keyword}/>
    </UserWrapper>)
}