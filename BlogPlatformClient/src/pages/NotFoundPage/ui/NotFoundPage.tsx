import { Page } from '../../../widgets/Page'
import cls from './NotFoundPage.module.scss'
import { memo } from 'react'

export const NotFoundPage = memo(() => {
    return (
        <Page className={cls.NotFoundPage}>
            { 'Page is not found.' }
        </Page>
    )
})
