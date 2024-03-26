import cls from './NotFoundPage.module.scss'
import { memo } from 'react'

export const NotFoundPage = memo(() => {
    return (
        <section className={cls.NotFoundPage}>
            { 'Page is not found.' }
        </section>
    )
})
