import { normalizeLocale } from '@/locales/resources';

export const getAntdLocale = async (lang?: string) => {
  let normalLang = normalizeLocale(lang);
  if (normalLang === 'ar') normalLang = 'ar-EG';

  const { default: locale } = await import(`antd/locale/${normalLang.replace('-', '_')}.js`);

  return locale;
};
