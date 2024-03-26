import { lazy } from 'react'

//export const BlogPageLazy = lazy(async () => await import('./BlogPage'))
export const BlogPageLazy = lazy(async () => await new Promise(resolve => {
    // @ts-expect-error test
    setTimeout(() => { resolve(import('./BlogPage')) }, 1500)
}))