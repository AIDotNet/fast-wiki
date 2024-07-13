import { Icon } from '@lobehub/ui';
import { useTheme } from 'antd-style';
import { LucideIcon, SquareArrowOutUpRight } from 'lucide-react';
import { memo } from 'react';
import { Flexbox } from 'react-layout-kit';
import { Link } from 'react-router-dom';

export interface ItemLinkProps {
  href: string;
  icon?: LucideIcon;
  label: string;
  value: string;
}

const ItemLink = memo<ItemLinkProps>(({ label, href }) => {
  const theme = useTheme();

  return (
    <Link to={href} style={{ color: 'inherit' }} target={'_blank'}>
      <Flexbox align={'center'} gap={8} horizontal>
        {label}
        <Icon
          color={theme.colorTextDescription}
          icon={SquareArrowOutUpRight}
          size={{ fontSize: 14 }}
        />
      </Flexbox>
    </Link>
  );
});

export default ItemLink;
