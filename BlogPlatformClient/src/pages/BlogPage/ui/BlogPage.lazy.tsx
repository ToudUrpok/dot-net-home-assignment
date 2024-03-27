import { lazy } from 'react'

//export const BlogPageLazy = lazy(async () => await import('./BlogPage'))
export const BlogPageLazy = lazy(async () => await new Promise(resolve => {
    // @ts-expect-error fake delay is set to display loader
    setTimeout(() => { resolve(import('./BlogPage')) }, 1500)
}))