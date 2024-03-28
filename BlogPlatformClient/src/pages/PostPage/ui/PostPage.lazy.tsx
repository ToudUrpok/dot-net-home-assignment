import { lazy } from 'react'

export const PostPageLazy = lazy(async () => await import('./PostPage'))
