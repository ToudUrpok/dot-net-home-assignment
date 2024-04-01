import { RoutePaths } from '../../../../shared/config/routeConfig/routeConfig'
import { SidebarNavItem } from '../types/SidebarNavItem'
import BlogIcon from '../../../../shared/assets/icons/link-blog-20-20.svg?react'
import PlusIcon from '../../../../shared/assets/icons/icons8-plus.svg?react'
import { useMemo } from 'react'
import { useLogin } from '../../../../feature/Authorization'

const publicSidebarNavItems: SidebarNavItem[] = [
    {
        Path: RoutePaths.blog,
        Label: 'Blog',
        Icon: BlogIcon
    }
]

export const useSidebarNavItems = (): SidebarNavItem[] => {
    const { data } = useLogin()

    const items = useMemo<SidebarNavItem[]>(() => {
        if (data) {
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
    }, [data])

    return items
}
