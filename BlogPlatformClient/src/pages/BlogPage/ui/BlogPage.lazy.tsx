import { lazy } from 'react'

export const BlogPageLazy = lazy(async () => await import('./BlogPage'))