import { memo, useState } from "react";
import { AppList } from "../features/AppList";

const MobileLayout = memo(() => {
    const [input, setInput] = useState({
        page: 1,
        pageSize: 10
    });
    return (<>
        <AppList setInput={(v)=>{
            setInput(v);
        }} input={input}/>
    </>)
});

export default MobileLayout;