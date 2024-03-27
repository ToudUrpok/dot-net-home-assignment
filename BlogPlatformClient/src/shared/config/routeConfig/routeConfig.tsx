import { RouteProps } from 'react-router-dom'
import { BlogPage } from '../../../pages/BlogPage'
import { LoginPage } from '../../../pages/LoginPage'
import { NotFoundPage } from '../../../pages/NotFoundPage'
import { ProfilePage } from '../../../pages/ProfilePage'

export type AppRouteProps = RouteProps & {
    authOnly?: boolean
}

export type AppRoutes = 'blog' | 'login' | 'profile' | 'notFound'

export const RoutePaths: Record<AppRoutes, string> = {
    'blog': '/',
    'login': '/login',
    'profile': '/profile',
    'notFound': '*'
}

export const routeConfig: AppRouteProps[] = [
    {
        path: RoutePaths['blog'],
        element: <BlogPage />
    },
    {
        path: RoutePaths['login'],
        element: <LoginPage />
    },
    {
        authOnly: true,
        path: RoutePaths['profile'],
        element: <ProfilePage />
    },
    {
        path: RoutePaths['notFound'],
        element: <NotFoundPage />
    }
]
