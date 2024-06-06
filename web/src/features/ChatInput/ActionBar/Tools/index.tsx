import { ActionIcon, Avatar, Icon } from '@lobehub/ui';
import { Dropdown } from 'antd';
import { createStyles } from 'antd-style';
import isEqual from 'fast-deep-equal';
import { ArrowRight, Blocks, Store, ToyBrick } from 'lucide-react';
import { memo } from 'react';
import { useTranslation } from 'react-i18next';
import { Flexbox } from 'react-layout-kit';

import { useWorkspaceModal } from '@/app/(main)/chat/(workspace)/features/useWorkspaceModal';
import { useAgentStore } from '@/store/agent';
import { agentSelectors } from '@/store/agent/selectors';
import { featureFlagsSelectors, useServerConfigStore } from '@/store/serverConfig';
import { pluginHelpers, useToolStore } from '@/store/tool';
import { builtinToolSelectors, pluginSelectors } from '@/store/tool/selectors';
import { useUserStore } from '@/store/user';
import { modelProviderSelectors } from '@/store/user/selectors';

import ToolItem from './ToolItem';

const useStyles = createStyles(({ css, prefixCls }) => ({
  menu: css`
    &.${prefixCls}-dropdown-menu {
      padding-block: 8px;
    }

    .${prefixCls}-dropdown-menu-item-group-list .${prefixCls}-dropdown-menu-item {
      padding: 0;
      border-radius: 4px;
    }
  `,
}));

const Tools = memo(() => {
  const { t } = useTranslation('setting') as any;
  const list = useToolStore(pluginSelectors.installedPluginMetaList, isEqual);
  const { showDalle } = useServerConfigStore(featureFlagsSelectors);
  const builtinList = useToolStore(builtinToolSelectors.metaList(showDalle), isEqual);

  const enablePluginCount = useAgentStore(
    (s) =>
      agentSelectors
        .currentAgentPlugins(s)
        .filter((i) => !builtinList.some((b) => b.identifier === i)).length,
  );

  const [open, setOpen] = useWorkspaceModal();
  const { styles } = useStyles();

  const model = useAgentStore(agentSelectors.currentAgentModel);
  const enableFC = useUserStore(modelProviderSelectors.isModelEnabledFunctionCall(model));

  const items: any[] = [
    (builtinList.length !== 0 && {
      children: builtinList.map((item) => ({
        icon: <Avatar avatar={item.meta.avatar} size={24} />,
        key: item.identifier,
        label: (
          <ToolItem identifier={item.identifier} label={item.meta?.title || item.identifier} />
        ),
      })),

      key: 'builtins',
      label: t('tools.builtins.groupName'),
      type: 'group',
    }) as any,
    {
      children: [
        ...list.map((item) => ({
          icon: item.meta?.avatar ? (
            <Avatar avatar={pluginHelpers.getPluginAvatar(item.meta)} size={24} />
          ) : (
            <Icon icon={ToyBrick} size={{ fontSize: 16 }} style={{ padding: 4 }} />
          ),
          key: item.identifier,
          label: (
            <ToolItem
              identifier={item.identifier}
              label={pluginHelpers.getPluginTitle(item?.meta) || item.identifier}
            />
          ),
        })),
        {
          icon: <Icon icon={Store} size={{ fontSize: 16 }} style={{ padding: 4 }} />,

          key: 'plugin-store',
          label: (
            <Flexbox gap={40} horizontal justify={'space-between'} padding={'8px 12px'}>
              {t('tools.plugins.store')} <Icon icon={ArrowRight} />
            </Flexbox>
          ),
          onClick: (e) => {
            e.domEvent.stopPropagation();
            setOpen(true);
          },
        },
      ],
      key: 'plugins',
      label: (
        <Flexbox align={'center'} gap={40} horizontal justify={'space-between'}>
          {t('tools.plugins.groupName')}
          {enablePluginCount === 0 ? null : (
            <div style={{ fontSize: 12, marginInlineEnd: 4 }}>
              {t('tools.plugins.enabled', { num: enablePluginCount })}
            </div>
          )}
        </Flexbox>
      ),
      type: 'group',
    } as any,
  ].filter(Boolean);

  return (
    <>
      <Dropdown
        arrow={false}
        menu={{
          className: styles.menu,
          items,
          onClick: (e) => {
            e.domEvent.preventDefault();
          },
          style: {
            maxHeight: 500,
            overflowY: 'scroll',
          },
        }}
        placement={'top'}
        trigger={['click']}
      >
        <ActionIcon
          disable={!enableFC}
          icon={Blocks}
          placement={'bottom'}
          title={t(enableFC ? 'tools.title' : 'tools.disabled')}
        />
      </Dropdown>
    </>
  );
});

export default Tools;
