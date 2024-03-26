import styles from './Sidebar.module.scss'
import { cn } from '../../../../shared/lib/classNames/classNames'
import { memo, useMemo, useState } from 'react'
import { SidebarItem } from '../SidebarItem/SidebarItem'
import { useSidebarNavItems } from '../../model/useSidebarNavItems'
import { Button } from '../../../../shared/ui/Button/Button'

export const Sidebar = memo(() => {
    const [collapsed, setCollapsed] = useState(false)
    const sidebarNavItems = useSidebarNavItems()

    const onToggle = () => {
        setCollapsed(prev => !prev)
    }

    const items = useMemo(() => sidebarNavItems.map((item) => (
        <SidebarItem
            key={item.Path}
            item={item}
            collapsed={collapsed}
        />
    )), [collapsed, sidebarNavItems])

    return (
        <aside className={cn(styles.Sidebar, { [styles.Collapsed]: collapsed })} >
            <div className={styles.SidebarCollapse}>
                <Button
                    theme={'backgroundInverted'}
                    size={'xl'}
                    onClick={onToggle}
                >
                    { collapsed ? '>' : '<'}
                </Button>
            </div>
            <div className={styles.LinkItemsList}>
                {items}
            </div>
        </aside>
    )
})