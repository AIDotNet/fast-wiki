import { GridShowcase } from '@lobehub/ui';
import { PropsWithChildren } from 'react';
import { Flexbox } from 'react-layout-kit';

const COPYRIGHT = `© ${new Date().getFullYear()} TokenAI, Inc. All rights reserved.`;

const DesktopLayout = ({ children }: PropsWithChildren) => {
  return (
    <>
      <Flexbox
        align={'center'}
        height={'100%'}
        justify={'space-between'}
        padding={16}
        style={{ overflow: 'hidden', position: 'relative' }}
        width={'100%'}
      >
        <span style={{
          fontSize: '24px',
          fontWeight: 'bold',
          fontFamily: 'Arial, sans-serif',
          alignSelf: 'flex-start', 
          userSelect: 'none',
          color: 'var(--leva-colors-highlight3)',
        }}>
          FastWiki
        </span>
        <GridShowcase
          innerProps={{ gap: 24 }}
          style={{ maxHeight: 'calc(100% - 104px)', maxWidth: 1024 }}
          width={'100%'}
        >
          {children}
        </GridShowcase>
        <Flexbox align={'center'} horizontal justify={'space-between'}>
          <span style={{ opacity: 0.5 }}>{COPYRIGHT}</span>
        </Flexbox>
      </Flexbox>
    </>
  );
};

export default DesktopLayout;
