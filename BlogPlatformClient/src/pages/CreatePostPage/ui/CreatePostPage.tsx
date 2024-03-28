import { cn } from '../../../shared/lib/classNames/classNames'
import { Page } from '../../../widgets/Page'
import styles from './CreatePostPage.module.scss'
import { memo } from 'react'

interface CreatePostPageProps {
    className?: string
}

const CreatePostPage = (props: CreatePostPageProps) => {
    const {
        className
    } = props

    return (
        <Page className={cn(styles.CreatePostPage, {}, [className])}>
            <h1>Create Post Page</h1>
            <h3>If you are able to access this Page you are authorized.</h3>
        </Page>
    )
}

export default memo(CreatePostPage)