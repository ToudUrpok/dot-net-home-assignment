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
        alert('Post was Created Successfully.')
    }, [])

    const handleFailure = useCallback((error?: string) => {
        setError(error)
    }, [])

    return (
        <Page className={cn(styles.CreatePostPage, {}, [className])}>
            { error && (
                <p className={styles.Error}>
                    {error}
                </p>
            )}
            <CreatePostForm
                onSuccess={handleSuccess}
                onFailed={handleFailure}
            />
        </Page>
    )
}

export default memo(CreatePostPage)