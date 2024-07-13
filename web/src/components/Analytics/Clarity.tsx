import React, { memo } from 'react';

interface ClarityProps {
  projectId?: string;
}

const Clarity: React.FC<ClarityProps> = memo(({ projectId }) => {
  return projectId ? (
    <script
      dangerouslySetInnerHTML={{
        __html: `
          (function(c,l,a,r,i,t,y){
              c[a]=c[a]||function(){(c[a].q=c[a].q||[]).push(arguments)};
              t=l.createElement(r);t.async=1;t.src="https://www.clarity.ms/tag/"+i;
              y=l.getElementsByTagName(r)[0];y.parentNode.insertBefore(t,y);
          })(window, document, "clarity", "script", "${projectId}");
        `,
      }}
      id="clarity-script"
    />
  ) : null;
});

export default Clarity;
