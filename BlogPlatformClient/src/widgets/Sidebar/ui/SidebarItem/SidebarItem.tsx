import styles from './SidebarItem.module.scss'
import { cn } from '../../../../shared/lib/classNames/classNames'
import { ISidebarNavItem } from '../../model/SidebarNavItem'
import { AppLink } from '../../../../shared/ui/AppLink/AppLink'
import { memo } from 'react'

interface SidebarItemProps {
    item: ISidebarNavItem
    collapsed: boolean
}

export const SidebarItem = memo(({ item, collapsed }: SidebarItemProps) => {
    return (
        <nav className={cn(styles.SidebarItem, { [styles.collapsed]: collapsed })}>
            <AppLink
                className={styles.SidebarLinkWrapper}
                to={item.Path}
                theme={'contrast'}
            >
                <item.Icon className={styles.SidebarItemIcon}/>
                <span className={styles.SidebarItemLink}>
                    {item.Label}
                </span>
            </AppLink>
        </nav>
    )
})