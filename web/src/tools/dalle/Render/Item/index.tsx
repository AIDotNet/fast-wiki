import { Highlighter, Icon } from '@lobehub/ui';
import { Spin } from 'antd';
import { createStyles } from 'antd-style';
import { Loader2 } from 'lucide-react';
import { memo, useState } from 'react';
import { useTranslation } from 'react-i18next';
import { Flexbox } from 'react-layout-kit';

import { useChatStore } from '@/store/chat';
import { DallEImageItem } from '@/types/tool/dalle';

import EditMode from './EditMode';
import Error from './Error';
import ImagePreview from './Image';

const useStyles = createStyles(({ css, token, prefixCls }) => ({
  action: css`
    position: absolute;
    z-index: 100;
    top: 4px;
    right: 4px;
  `,
  container: css`
    overflow: scroll;
    aspect-ratio: 1;
    border: 1px solid ${token.colorBorder};
    border-radius: 8px;

    .${prefixCls}-spin-nested-loading {
      height: 100%;
    }
  `,
}));

const ImageItem = memo<DallEImageItem & { index: number; messageId: string }>(
  ({ prompt, messageId, imageId, previewUrl, index, style, size, quality }) => {
    const { t } = useTranslation('tool');
    const { styles } = useStyles();

    const [edit, setEdit] = useState(false);

    if (edit)
      return (
        <Flexbox className={styles.container} padding={8}>
          <EditMode
            imageId={imageId}
            prompt={prompt}
            quality={quality}
            setEdit={setEdit}
            size={size}
            style={style}
          />
        </Flexbox>
      );

    if (imageId || previewUrl)
      return <ImagePreview imageId={imageId} previewUrl={previewUrl} prompt={prompt} />;

    return (
      <Flexbox className={styles.container} padding={8}>
        
      </Flexbox>
    );
  },
);

export default ImageItem;
