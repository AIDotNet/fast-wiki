import clsx from 'clsx';
import Heading from '@theme/Heading';
import styles from './styles.module.css';

type FeatureItem = {
  title: string;
  Svg: React.ComponentType<React.ComponentProps<'svg'>>;
  description: JSX.Element;
};

const FeatureList: FeatureItem[] = [
  {
    title: '知识库功能',
    Svg: require('@site/static/img/undraw_docusaurus_mountain.svg').default,
    description: (
      <>
        支持pdf，word，excel，ppt，md等文档上传，然后根据上传的文档提供知识回复更好的搜索体验。
      </>
    ),
  },
  {
    title: '对话分享',
    Svg: require('@site/static/img/undraw_docusaurus_tree.svg').default,
    description: (
      <>
        应用支持创建多个对话分享并且对于对话进行管理和控制。
      </>
    ),
  },
  {
    title: '体验和架构设计',
    Svg: require('@site/static/img/undraw_docusaurus_react.svg').default,
    description: (
      <>
        框架使用MasaBlazor+ MasaFramework并且前后分离，可以根据自己的需求进行二次开发，MasaBlazor在UI上非常美观，并且体验更好。
      </>
    ),
  },
];

function Feature({ title, Svg, description }: FeatureItem) {
  return (
    <div className={clsx('col col--4')}>
      <div className="text--center">
        <Svg className={styles.featureSvg} role="img" />
      </div>
      <div className="text--center padding-horiz--md">
        <Heading as="h3">{title}</Heading>
        <p>{description}</p>
      </div>
    </div>
  );
}

export default function HomepageFeatures(): JSX.Element {
  return (
    <section className={styles.features}>
      <div className="container">
        <div className="row">
          {FeatureList.map((props, idx) => (
            <Feature key={idx} {...props} />
          ))}
        </div>
      </div>
    </section>
  );
}
