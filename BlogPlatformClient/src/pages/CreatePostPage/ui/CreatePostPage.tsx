import { CreatePostForm } from '../../../entities/Post'
import { cn } from '../../../shared/lib/classNames/classNames'
import { Page } from '../../../widgets/Page'
import styles from './CreatePostPage.module.scss'
import { memo, useCallback, useState } from 'react'

interface CreatePostPageProps {
    className?: string
}

const CreatePostPage = (props: CreatePostPageProps) => {
    const {
        className
    } = props

    const [error, setError] = useState<string | undefined>(undefined)

    const handleSuccess = useCallback(() => {
        setError(undefined)
    }, [])

    const handleFailure = useCallback((error?: string) => {
        setError(error)
    }, [])

    return (
        <Page className={cn(styles.CreatePostPage, {}, [className])}>
            <h1>Create Post Page</h1>
            <h3>If you are able to access this Page you are authorized.</h3>
            {error && <p>{error}</p>}
            <CreatePostForm
                onSuccess={handleSuccess}
                onFailed={handleFailure}
            />
        </Page>
    )
}

export default memo(CreatePostPage)