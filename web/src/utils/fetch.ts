import { fetchEventSource } from '@microsoft/fetch-event-source';
import { t } from 'i18next';
import { produce } from 'immer';
import { message } from 'antd';
import { THOR_CHAT_OBSERVATION_ID, THOR_CHAT_TRACE_ID } from '@/const/trace';
import { ErrorResponse, ErrorType } from '@/types/fetch';
import {
  ChatMessageError,
  MessageToolCall,
  MessageToolCallChunk,
  MessageToolCallSchema,
} from '@/types/message';
import { VITE_API_URL } from '@/const/url';

export const getMessageError = async (response: Response) => {
  let chatMessageError: ChatMessageError;

  // 尝试取一波业务错误语义
  try {
    const data = (await response.json()) as ErrorResponse;
    chatMessageError = {
      body: data.body,
      message: t(`response.${data.errorType}` as any, { ns: 'error' }),
      type: data.errorType,
    };
  } catch {
    // 如果无法正常返回，说明是常规报错
    chatMessageError = {
      message: t(`response.${response.status}` as any, { ns: 'error' }),
      type: response.status as ErrorType,
    };
  }

  return chatMessageError;
};

type SSEFinishType = 'done' | 'error' | 'abort';

export type OnFinishHandler = (
  text: string,
  context: {
    observationId?: string | null;
    toolCalls?: MessageToolCall[];
    traceId?: string | null;
    type?: SSEFinishType;
  },
) => Promise<void>;

export interface MessageTextChunk {
  text: string;
  type: 'text';
}

interface MessageToolCallsChunk {
  isAnimationActives?: boolean[];
  tool_calls: MessageToolCall[];
  type: 'tool_calls';
}

export interface FetchSSEOptions {
  fetcher?: typeof fetch;
  onAbort?: (text: string) => Promise<void>;
  onErrorHandle?: (error: ChatMessageError) => void;
  onFinish?: OnFinishHandler;
  onMessageHandle?: (chunk: MessageTextChunk | MessageToolCallsChunk) => void;
  smoothing?: boolean;
}

export const parseToolCalls = (origin: MessageToolCall[], value: MessageToolCallChunk[]) =>
  produce(origin, (draft) => {
    // if there is no origin, we should parse all the value and set it to draft
    if (draft.length === 0) {
      draft.push(...value.map((item) => MessageToolCallSchema.parse(item)));
      return;
    }

    // if there is origin, we should merge the value to the origin
    value.forEach(({ index, ...item }) => {
      if (!draft?.[index]) {
        // if not, we should insert it to the draft
        draft?.splice(index, 0, MessageToolCallSchema.parse(item));
      } else {
        // if it is already in the draft, we should merge the arguments to the draft
        if (item.function?.arguments) {
          draft[index].function.arguments += item.function.arguments;
        }
      }
    });
  });

const createSmoothMessage = (params: { onTextUpdate: (delta: string, text: string) => void }) => {
  let buffer = '';
  // why use queue: https://shareg.pt/GLBrjpK
  let outputQueue: string[] = [];

  // eslint-disable-next-line no-undef
  let animationTimeoutId: NodeJS.Timeout | null = null;
  let isAnimationActive = false;

  // when you need to stop the animation, call this function
  const stopAnimation = () => {
    isAnimationActive = false;
    if (animationTimeoutId !== null) {
      clearTimeout(animationTimeoutId);
      animationTimeoutId = null;
    }
  };

  // define startAnimation function to display the text in buffer smooth
  // when you need to start the animation, call this function
  const startAnimation = (speed = 2) =>
    new Promise<void>((resolve) => {
      if (isAnimationActive) {
        resolve();
        return;
      }

      isAnimationActive = true;

      const updateText = () => {
        // 如果动画已经不再激活，则停止更新文本
        if (!isAnimationActive) {
          clearTimeout(animationTimeoutId!);
          animationTimeoutId = null;
          resolve();
        }

        // 如果还有文本没有显示
        // 检查队列中是否有字符待显示
        if (outputQueue.length > 0) {
          // 从队列中获取前两个字符（如果存在）
          const charsToAdd = outputQueue.splice(0, speed).join('');
          buffer += charsToAdd;

          // 更新消息内容，这里可能需要结合实际情况调整
          params.onTextUpdate(charsToAdd, buffer);

          // 设置下一个字符的延迟
          animationTimeoutId = setTimeout(updateText, 16); // 16 毫秒的延迟模拟打字机效果
        } else {
          // 当所有字符都显示完毕时，清除动画状态
          isAnimationActive = false;
          animationTimeoutId = null;
          resolve();
        }
      };

      updateText();
    });

  const pushToQueue = (text: string) => {
    outputQueue.push(...text.split(''));
  };

  return {
    isAnimationActive,
    isTokenRemain: () => outputQueue.length > 0,
    pushToQueue,
    startAnimation,
    stopAnimation,
  };
};

const createSmoothToolCalls = (params: {
  onToolCallsUpdate: (toolCalls: MessageToolCall[], isAnimationActives: boolean[]) => void;
}) => {
  let toolCallsBuffer: MessageToolCall[] = [];

  // 为每个 tool_call 维护一个输出队列和动画控制器

  // eslint-disable-next-line no-undef
  const animationTimeoutIds: (NodeJS.Timeout | null)[] = [];
  const outputQueues: string[][] = [];
  const isAnimationActives: boolean[] = [];

  const stopAnimation = (index: number) => {
    isAnimationActives[index] = false;
    if (animationTimeoutIds[index] !== null) {
      clearTimeout(animationTimeoutIds[index]!);
      animationTimeoutIds[index] = null;
    }
  };

  const startAnimation = (index: number, speed = 2) =>
    new Promise<void>((resolve) => {
      if (isAnimationActives[index]) {
        resolve();
        return;
      }

      isAnimationActives[index] = true;

      const updateToolCall = () => {
        if (!isAnimationActives[index]) {
          resolve();
        }

        if (outputQueues[index].length > 0) {
          const charsToAdd = outputQueues[index].splice(0, speed).join('');

          const toolCallToUpdate = toolCallsBuffer[index];

          if (toolCallToUpdate) {
            toolCallToUpdate.function.arguments += charsToAdd;

            // 触发 ui 更新
            params.onToolCallsUpdate(toolCallsBuffer, [...isAnimationActives]);
          }

          animationTimeoutIds[index] = setTimeout(updateToolCall, 16);
        } else {
          isAnimationActives[index] = false;
          animationTimeoutIds[index] = null;
          resolve();
        }
      };

      updateToolCall();
    });

  const pushToQueue = (toolCallChunks: MessageToolCallChunk[]) => {
    toolCallChunks.forEach((chunk) => {
      // init the tool call buffer and output queue
      if (!toolCallsBuffer[chunk.index]) {
        toolCallsBuffer[chunk.index] = MessageToolCallSchema.parse(chunk);
      }

      if (!outputQueues[chunk.index]) {
        outputQueues[chunk.index] = [];
        isAnimationActives[chunk.index] = false;
        animationTimeoutIds[chunk.index] = null;
      }

      outputQueues[chunk.index].push(...(chunk.function?.arguments || '').split(''));
    });
  };

  const startAnimations = async (speed = 2) => {
    const pools = toolCallsBuffer.map(async (_, index) => {
      if (outputQueues[index].length > 0 && !isAnimationActives[index]) {
        await startAnimation(index, speed);
      }
    });

    await Promise.all(pools);
  };
  const stopAnimations = () => {
    toolCallsBuffer.forEach((_, index) => {
      stopAnimation(index);
    });
  };

  return {
    isAnimationActives,
    isTokenRemain: () => outputQueues.some((token) => token.length > 0),
    pushToQueue,
    startAnimations,
    stopAnimations,
  };
};

/**
 * Fetch data using stream method
 */
// eslint-disable-next-line no-undef
export const fetchSSE = async (url: string, options: RequestInit & FetchSSEOptions = {}) => {
  let output = '';
  let toolCalls: undefined | MessageToolCall[];
  let triggerOnMessageHandler = false;

  let finishedType: SSEFinishType = 'done';
  let response!: Response;

  const { smoothing = true } = options;

  const textController = createSmoothMessage({
    onTextUpdate: (delta, text) => {
      output = text;
      options.onMessageHandle?.({ text: delta, type: 'text' });
    },
  });

  const toolCallsController = createSmoothToolCalls({
    onToolCallsUpdate: (toolCalls, isAnimationActives) => {
      options.onMessageHandle?.({ isAnimationActives, tool_calls: toolCalls, type: 'tool_calls' });
    },
  });

  try {
    await fetchEventSource(url, {
      body: options.body,
      // @ts-ignore
      fetch: options?.fetcher,
      headers: options.headers as Record<string, string>,
      method: options.method,
      onerror: (error) => {
        if ((error as TypeError).name === 'AbortError') {
          finishedType = 'abort';
          options?.onAbort?.(output);
          textController.stopAnimation();
        } else {
          finishedType = 'error';
          options.onErrorHandle?.(error);
        }

        throw new Error(error);
      },
      onmessage: (ev) => {
        triggerOnMessageHandler = true;
        let data;
        try {
          const value = JSON.parse(ev.data);
          // 如果value还是字符串，说明是文本消息
          if (typeof value === 'string') {
            data = value;
          } else if (!value.choices || !value.choices[0].delta?.content) {
            return;
          }
          else if (value.choices[0].delta && value.choices[0].delta?.content) {
            data = value.choices[0].delta.content;
          } else {
            data = '';
          }
        } catch (e) {
          console.warn('parse error, fallback to stream', e);
          options.onMessageHandle?.({ text: data, type: 'text' });
          return;
        }
        
        if (smoothing) {
          textController.pushToQueue(data);

          if (!textController.isAnimationActive) textController.startAnimation();
        } else {
          output += data;
          options.onMessageHandle?.({ text: data, type: 'text' });
        }

      },
      onopen: async (res) => {
        response = res.clone();
        // 如果不 ok 说明有请求错误
        if (!response.ok) {
          throw await getMessageError(res);
        }
      },
      // we should keep open when page hidden, or it will case lots of token cost
      // refs: https://github.comAIDotNet/lobe-chat/issues/2501
      openWhenHidden: true,
      signal: options.signal,
    });
  } catch { }

  // only call onFinish when response is available
  // so like abort, we don't need to call onFinish
  if (response) {
    textController.stopAnimation();
    toolCallsController.stopAnimations();

    if (response.ok) {
      // if there is no onMessageHandler, we should call onHandleMessage first
      if (!triggerOnMessageHandler) {
        output = await response.clone().text();
        options.onMessageHandle?.({ text: output, type: 'text' });
      }

      const traceId = response.headers.get(THOR_CHAT_TRACE_ID);
      const observationId = response.headers.get(THOR_CHAT_OBSERVATION_ID);

      if (textController.isTokenRemain()) {
        await textController.startAnimation(15);
      }

      if (toolCallsController.isTokenRemain()) {
        await toolCallsController.startAnimations(15);
      }

      await options?.onFinish?.(output, { observationId, toolCalls, traceId, type: finishedType });
    }
  }

  return response;
};


export async function fetch(url: string, options: any) {
  const token = localStorage.getItem('token');
  const headers = {
    ...options.headers,
    Authorization: `Bearer ${token}`
  };
  try {
    url = `${VITE_API_URL}${url}`.replace(/([^:]\/)\/+/g, '$1');
    const response = await window.fetch(url, { ...options, headers });
    if (response.status >= 200 && response.status < 300) {
      const data = await response.text();
      if (data === '' || data === null) {
        return null;
      }
      return JSON.parse(data);
    }

    if (response.status === 204) {
      return null;
    }

    // 如果是401，跳转到登录页
    if (response.status === 401) {
      if (typeof window === 'undefined') return;
      window.location.href = '/auth-login';
    }

    if (response.status === 400) {
      // 读取body内容
      const data = await response.json();
      message.error(data.message);
      throw new Error(data);
    } else if (response.status === 404) {
      message.error('请求的资源不存在');
      const data = await response.json();
      message.error(data.message);
      throw new Error(data);
    } else if (response.status === 500) {
      message.error('服务器错误');
      const data = await response.json();
      message.error(data.message);
      throw new Error(data);
    }

    const error = new Error();
    throw error;
  } catch (error: any) {
    throw error;
  }
}

export async function fetchRaw(url: string, data: any) {
  const token = localStorage.getItem('token');
  const headers = {
    Authorization: `Bearer ${token}`,
    'Content-Type': 'application/json',

  };
  try {
    // 拼接baseUrl并且处理/重复问题
    url = `${VITE_API_URL}${url}`.replace(/([^:]\/)\/+/g, '$1');
    const response = await window.fetch(url, {
      headers,
      method: 'POST',
      body: JSON.stringify(data)
    });

    if (response.ok === false) {
      const reader = await response.text();
      throw new Error(reader);
    }

    const reader = response.body!.getReader();
    return {
      [Symbol.asyncIterator]() {
        return {
          async next() {
            const { done, value } = await reader.read();
            if (done) {
              return { done: true, value: null };
            }
            return {
              done: false,
              value: new TextDecoder("utf-8").decode(value),
            };
          },
        };
      },
    };
  } catch (error: any) {
    throw error;
  }
}

export const get = (url: string, options?: any) => {
  return fetch(url, {
    method: 'GET',
    ...options
  });
};

export const post = (url: string, options?: any) => {
  return fetch(url, {
    method: 'POST',
    ...options
  });
};

export const postJson = (url: string, data: any) => {
  return post(url, {
    headers: {
      'Content-Type': 'application/json'
    },
    body: JSON.stringify(data)
  });
}

export const put = (url: string, options?: any) => {
  return fetch(url, {
    method: 'PUT',
    ...options
  });
};

export const putJson = (url: string, data: any) => {
  return put(url, {
    headers: {
      'Content-Type': 'application/json'
    },
    body: JSON.stringify(data)
  });

}

export const del = (url: string, options?: any) => {
  return fetch(url, {
    method: 'DELETE',
    ...options
  });
};

