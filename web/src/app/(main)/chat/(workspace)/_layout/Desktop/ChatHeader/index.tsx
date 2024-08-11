import { ChatHeader } from '@lobehub/ui';

import HeaderAction from './HeaderAction';

const Header = () => <ChatHeader right={<HeaderAction />} style={{ zIndex: 11 }} />;

export default Header;
