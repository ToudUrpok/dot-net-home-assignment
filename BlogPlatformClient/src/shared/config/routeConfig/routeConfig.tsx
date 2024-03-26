import { RouteProps } from 'react-router-dom'
import { BlogPage } from '../../../pages/BlogPage'
import { LoginPage } from '../../../pages/LoginPage'
import { NotFoundPage } from '../../../pages/NotFoundPage'

export type AppRouteProps = RouteProps & {
    authOnly?: boolean
}

export type AppRoutes = 'blog' | 'login' | 'notFound'

export const RoutePaths: Record<AppRoutes, string> = {
    'blog': '/',
    'login': '/login',
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
        path: RoutePaths['notFound'],
        element: <NotFoundPage />
    }
]
