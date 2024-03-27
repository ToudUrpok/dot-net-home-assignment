import { lazy } from 'react'

//export const ProfilePageLazy = lazy(async () => await import('./ProfilePage'))
export const ProfilePageLazy = lazy(async () => await new Promise(resolve => {
    // @ts-expect-error fake delay is set to display loader
    setTimeout(() => { resolve(import('./ProfilePage')) }, 1500)
}))