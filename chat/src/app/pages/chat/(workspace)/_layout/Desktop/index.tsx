import ChatConversation from "../../../@conversation/default";
import HotKeys from "./HotKeys";
import { Flexbox } from 'react-layout-kit';


export default function DesktopLayout() {
  return (
    <>
      <Flexbox
        height={'100%'}
        horizontal
        style={{ overflow: 'hidden', position: 'relative' }}
        width={'100%'}
      >
        <Flexbox
          height={'100%'}
          style={{ overflow: 'hidden', position: 'relative' }}
          width={'100%'}
        >
          <ChatConversation />
        </Flexbox>
      </Flexbox>
      <HotKeys />
    </>
  );
}