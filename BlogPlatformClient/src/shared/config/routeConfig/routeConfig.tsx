import { RouteProps } from 'react-router-dom'
import { BlogPage } from '../../../pages/BlogPage'
import { LoginPage } from '../../../pages/LoginPage'
import { NotFoundPage } from '../../../pages/NotFoundPage'
import { CreatePostPage } from '../../../pages/CreatePostPage'
import { PostPage } from '../../../pages/PostPage'

export type AppRouteProps = RouteProps & {
    authOnly?: boolean
}

export type AppRoutes = 'blog' | 'login' | 'createPost' | 'notFound' | 'post'

export const RoutePaths: Record<AppRoutes, string> = {
    'blog': '/',
    'login': '/login',
    'createPost': '/createPost',
    'post': '/post/',   // :id
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
        path: RoutePaths['post'] + ':id',
        element: <PostPage />
    },
    {
        authOnly: true,
        path: RoutePaths['createPost'],
        element: <CreatePostPage />
    },
    {
        path: RoutePaths['notFound'],
        element: <NotFoundPage />
    }
]
