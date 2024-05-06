function embedChatbot() {
    const chatBtnId = 'fastwiki-chatbot-button';
    const chatWindowId = 'fastwiki-chatbot-window';
    const script = document.getElementById('chatbot-iframe');
    const botSrc = script?.getAttribute('data-bot-src');
    const defaultOpen = script?.getAttribute('data-default-open') === 'true';
    const canDrag = script?.getAttribute('data-drag') === 'true';
    const MessageIcon =
      script?.getAttribute('data-open-icon') ||
      `data:image/svg+xml;base64,PD94bWwgdmVyc2lvbj0iMS4wIiBzdGFuZGFsb25lPSJubyI/PjwhRE9DVFlQRSBzdmcgUFVCTElDICItLy9XM0MvL0RURCBTVkcgMS4xLy9FTiIgImh0dHA6Ly93d3cudzMub3JnL0dyYXBoaWNzL1NWRy8xLjEvRFREL3N2ZzExLmR0ZCI+PHN2ZyB0PSIxNzE1MDA0NjQyNTU1IiBjbGFzcz0iaWNvbiIgdmlld0JveD0iMCAwIDEwMjQgMTAyNCIgdmVyc2lvbj0iMS4xIiB4bWxucz0iaHR0cDovL3d3dy53My5vcmcvMjAwMC9zdmciIHAtaWQ9IjU1MDciIHhtbG5zOnhsaW5rPSJodHRwOi8vd3d3LnczLm9yZy8xOTk5L3hsaW5rIiB3aWR0aD0iMjAwIiBoZWlnaHQ9IjIwMCI+PHBhdGggZD0iTTUxMiA2NGMyNTkuMiAwIDQ2OS4zMzMzMzMgMjAwLjU3NiA0NjkuMzMzMzMzIDQ0OHMtMjEwLjEzMzMzMyA0NDgtNDY5LjMzMzMzMyA0NDhhNDg0LjQ4IDQ4NC40OCAwIDAgMS0yMzIuNzI1MzMzLTU4Ljg4bC0xMTYuMzk0NjY3IDUwLjY0NTMzM2E0Mi42NjY2NjcgNDIuNjY2NjY3IDAgMCAxLTU4LjUxNzMzMy00OS4wMDI2NjZsMjkuNzYtMTI1LjAxMzMzNEM3Ni42MjkzMzMgNzAzLjQwMjY2NyA0Mi42NjY2NjcgNjExLjQ3NzMzMyA0Mi42NjY2NjcgNTEyIDQyLjY2NjY2NyAyNjQuNTc2IDI1Mi44IDY0IDUxMiA2NHogbTAgNjRDMjg3LjQ4OCAxMjggMTA2LjY2NjY2NyAzMDAuNTg2NjY3IDEwNi42NjY2NjcgNTEyYzAgNzkuNTczMzMzIDI1LjU1NzMzMyAxNTUuNDM0NjY3IDcyLjU1NDY2NiAyMTkuMjg1MzMzbDUuNTI1MzM0IDcuMzE3MzM0IDE4LjcwOTMzMyAyNC4xOTItMjYuOTY1MzMzIDExMy4yMzczMzMgMTA1Ljk4NC00Ni4wOCAyNy40NzczMzMgMTUuMDE4NjY3QzM3MC44NTg2NjcgODc4LjIyOTMzMyA0MzkuOTc4NjY3IDg5NiA1MTIgODk2YzIyNC41MTIgMCA0MDUuMzMzMzMzLTE3Mi41ODY2NjcgNDA1LjMzMzMzMy0zODRTNzM2LjUxMiAxMjggNTEyIDEyOHogbS0xNTcuNjk2IDM0MS4zMzMzMzNhNDIuNjY2NjY3IDQyLjY2NjY2NyAwIDEgMSAwIDg1LjMzMzMzNCA0Mi42NjY2NjcgNDIuNjY2NjY3IDAgMCAxIDAtODUuMzMzMzM0eiBtMTU5LjAxODY2NyAwYTQyLjY2NjY2NyA0Mi42NjY2NjcgMCAxIDEgMCA4NS4zMzMzMzQgNDIuNjY2NjY3IDQyLjY2NjY2NyAwIDAgMSAwLTg1LjMzMzMzNHogbTE1OC45OTczMzMgMGE0Mi42NjY2NjcgNDIuNjY2NjY3IDAgMSAxIDAgODUuMzMzMzM0IDQyLjY2NjY2NyA0Mi42NjY2NjcgMCAwIDEgMC04NS4zMzMzMzR6IiBmaWxsPSIjMTI5NmRiIiBwLWlkPSI1NTA4Ij48L3BhdGg+PC9zdmc+`;
    const CloseIcon =
      script?.getAttribute('data-close-icon') ||
      'data:image/svg+xml;base64,PHN2ZyB0PSIxNjkwNTM1NDQxNTI2IiBjbGFzcz0iaWNvbiIgdmlld0JveD0iMCAwIDEwMjQgMTAyNCIgdmVyc2lvbj0iMS4xIiB4bWxucz0iaHR0cDovL3d3dy53My5vcmcvMjAwMC9zdmciIHAtaWQ9IjYzNjciIHhtbG5zOnhsaW5rPSJodHRwOi8vd3d3LnczLm9yZy8xOTk5L3hsaW5rIj48cGF0aCBkPSJNNTEyIDEwMjRBNTEyIDUxMiAwIDEgMSA1MTIgMGE1MTIgNTEyIDAgMCAxIDAgMTAyNHpNMzA1Ljk1NjU3MSAzNzAuMzk1NDI5TDQ0Ny40ODggNTEyIDMwNS45NTY1NzEgNjUzLjYwNDU3MWE0NS41NjggNDUuNTY4IDAgMSAwIDY0LjQzODg1OCA2NC40Mzg4NThMNTEyIDU3Ni41MTJsMTQxLjYwNDU3MSAxNDEuNTMxNDI5YTQ1LjU2OCA0NS41NjggMCAwIDAgNjQuNDM4ODU4LTY0LjQzODg1OEw1NzYuNTEyIDUxMmwxNDEuNTMxNDI5LTE0MS42MDQ1NzFhNDUuNTY4IDQ1LjU2OCAwIDEgMC02NC40Mzg4NTgtNjQuNDM4ODU4TDUxMiA0NDcuNDg4IDM3MC4zOTU0MjkgMzA1Ljk1NjU3MWE0NS41NjggNDUuNTY4IDAgMCAwLTY0LjQzODg1OCA2NC40Mzg4NTh6IiBmaWxsPSIjNGU4M2ZkIiBwLWlkPSI2MzY4Ij48L3BhdGg+PC9zdmc+';
  
    if (!botSrc) {
      console.error(`Can't find appid`);
      return;
    }
    if (document.getElementById(chatBtnId)) {
      return;
    }
  
    const ChatBtn = document.createElement('div');
    ChatBtn.id = chatBtnId;
    ChatBtn.style.cssText =
      'position: fixed; bottom: 30px; right: 60px; width: 40px; height: 40px; cursor: pointer; z-index: 2147483647; transition: 0;';
  
    // btn icon
    const ChatBtnDiv = document.createElement('img');
    ChatBtnDiv.src = defaultOpen ? CloseIcon : MessageIcon;
    ChatBtnDiv.setAttribute('width', '100%');
    ChatBtnDiv.setAttribute('height', '100%');
    ChatBtnDiv.draggable = false;
  
    const iframe = document.createElement('iframe');
    iframe.allow = '*';
    iframe.referrerPolicy = 'no-referrer';
    iframe.title = 'fastwiki Chat Window';
    iframe.id = chatWindowId;
    iframe.src = botSrc;
    iframe.style.cssText =
      'border: none; position: fixed; flex-direction: column; justify-content: space-between; box-shadow: rgba(150, 150, 150, 0.2) 0px 10px 30px 0px, rgba(150, 150, 150, 0.2) 0px 0px 0px 1px; bottom: 80px; right: 60px; width: 375px; height: 667px; max-width: 90vw; max-height: 85vh; border-radius: 0.75rem; display: flex; z-index: 2147483647; overflow: hidden; left: unset; background-color: #F3F4F6;';
    iframe.style.visibility = defaultOpen ? 'unset' : 'hidden';
  
    document.body.appendChild(iframe);
  
    let chatBtnDragged = false;
    let chatBtnDown = false;
    let chatBtnMouseX;
    let chatBtnMouseY;
    ChatBtn.addEventListener('click', function () {
      if (chatBtnDragged) {
        chatBtnDragged = false;
        return;
      }
      const chatWindow = document.getElementById(chatWindowId);
  
      if (!chatWindow) return;
      const visibilityVal = chatWindow.style.visibility;
      if (visibilityVal === 'hidden') {
        chatWindow.style.visibility = 'unset';
        ChatBtnDiv.src = CloseIcon;
      } else {
        chatWindow.style.visibility = 'hidden';
        ChatBtnDiv.src = MessageIcon;
      }
    });
  
    ChatBtn.addEventListener('mousedown', (e) => {
      e.stopPropagation();
  
      if (!chatBtnMouseX && !chatBtnMouseY) {
        chatBtnMouseX = e.clientX;
        chatBtnMouseY = e.clientY;
      }
  
      chatBtnDown = true;
    });
  
    window.addEventListener('mousemove', (e) => {
      e.stopPropagation();
      if (!canDrag || !chatBtnDown) return;
  
      chatBtnDragged = true;
      const transformX = e.clientX - chatBtnMouseX;
      const transformY = e.clientY - chatBtnMouseY;
  
      ChatBtn.style.transform = `translate3d(${transformX}px, ${transformY}px, 0)`;
    });
  
    window.addEventListener('mouseup', (e) => {
      chatBtnDown = false;
    });
  
    ChatBtn.appendChild(ChatBtnDiv);
    document.body.appendChild(ChatBtn);
  }
  window.addEventListener('load', embedChatbot);
  