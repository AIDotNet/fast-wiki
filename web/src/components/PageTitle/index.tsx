import { memo, useEffect } from 'react';

const PageTitle = memo<{ title: string }>(({ title }) => {
  useEffect(() => {
    document.title = title ? `${title} · FastWki-Chat` : 'FastWki-Chat';
  }, [title]);

  return null;
});

export default PageTitle;
