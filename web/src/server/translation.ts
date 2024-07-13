import { get } from 'lodash-es';

import { DEFAULT_LANG } from '@/const/locale';
import { NS, normalizeLocale } from '@/locales/resources';
import { isDev } from '@/utils/env';

export const translation = async (ns: NS = 'common') => {
  let i18ns = {};
  try {
    const lng = navigator.language || DEFAULT_LANG;
    let locale = normalizeLocale(lng);
    let url = `/locales/${locale}/${ns}.json`;

    // 如果是开发环境，且默认语言文件不存在，尝试加载中文文件
    const response = await fetch(url);
    if (!response.ok) {
      locale = normalizeLocale(isDev ? 'zh-CN' : DEFAULT_LANG);
      url = `/locales/${locale}/${ns}.json`;
      const fallbackResponse = await fetch(url);
      if (!fallbackResponse.ok) throw new Error('Failed to fetch translation file');
      i18ns = await fallbackResponse.json();
    } else {
      i18ns = await response.json();
    }
  } catch (e) {
    console.error('Error while fetching translation file', e);
  }

  return {
    t: (key: string, options: { [key: string]: string } = {}) => {
      if (!i18ns) return key;
      let content = get(i18ns, key);
      if (!content) return key;
      if (options) {
        Object.entries(options).forEach(([key, value]) => {
          content = content.replace(`{{${key}}}`, value);
        });
      }
      return content;
    },
  };
};