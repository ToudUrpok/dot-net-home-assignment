import { RoutePaths } from '../../../../shared/config/routeConfig/routeConfig'
import { SidebarNavItem } from '../types/SidebarNavItem'
import BlogIcon from '../../../../shared/assets/icons/link-blog-20-20.svg?react'
import PlusIcon from '../../../../shared/assets/icons/icons8-plus.svg?react'
import { useMemo } from 'react'
import { selectUserAuthToken } from '../../../../entities/User'
import { useAppSelector } from '../../../../shared/hooks/useAppSelector'

const publicSidebarNavItems: SidebarNavItem[] = [
    {
        Path: RoutePaths.blog,
        Label: 'Blog',
        Icon: BlogIcon
    }
]

export const useSidebarNavItems = (): SidebarNavItem[] => {
    const auth = useAppSelector(selectUserAuthToken)

    const items = useMemo<SidebarNavItem[]>(() => {
        if (auth) {
            return [
                ...publicSidebarNavItems,
                {
                    Path: RoutePaths.createPost,
                    Label: 'Create Post',
                    Icon: PlusIcon
                }
            ]
        }

        return publicSidebarNavItems
    }, [auth])

    return items
}
