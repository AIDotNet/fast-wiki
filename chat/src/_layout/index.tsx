import { memo } from "react";
import AppTheme from "./AppTheme";
import { Outlet } from "react-router-dom";
import StyleRegistry from "./StyleRegistry";


const Layout = memo(() => {
    return (
        <AppTheme>
            <StyleRegistry>
                <Outlet />
            </StyleRegistry>
        </AppTheme>
    );
});

export default Layout;