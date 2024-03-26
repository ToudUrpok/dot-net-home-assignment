import { RoutePaths } from '../../../shared/config/routeConfig/routeConfig'
import { ISidebarNavItem } from './SidebarNavItem'
import BlogIcon from '../../../shared/assets/icons/link-blog-20-20.svg?react'
import { useMemo } from 'react'

const publicSidebarNavItems: ISidebarNavItem[] = [
    {
        Path: RoutePaths.blog,
        Label: 'Blog',
        Icon: BlogIcon
    }
]

export const useSidebarNavItems = (): ISidebarNavItem[] => {
    const authData = true

    const items = useMemo<ISidebarNavItem[]>(() => {
        if (authData) {
            return [
                ...publicSidebarNavItems,
            ]
        }

        return publicSidebarNavItems
    }, [authData])

    return items
}
