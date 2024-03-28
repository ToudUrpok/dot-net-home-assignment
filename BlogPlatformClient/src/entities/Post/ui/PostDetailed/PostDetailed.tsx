import { cn } from '../../../../shared/lib/classNames/classNames'
import { Card } from '../../../../shared/ui/Card/Card'
import { Loader } from '../../../../shared/ui/Loader/Loader'
import { CommentsList } from '../../../Comment'
import { usePost } from '../../model/queries/usePost'
import styles from './PostDetailed.module.scss'
import { memo } from 'react'

interface PostDetailedProps {
    className?: string
    postId: string
}

export const PostDetailed = memo((props: PostDetailedProps) => {
    const {
        className,
        postId
    } = props

    const { error, data, isFetching } = usePost(postId)

    if (isFetching) {
        return (
            <div className={cn(styles.PostDetailed, {}, [className])}>
                <Loader />
            </div>
        )
    }

    if (error) {
        return (
            <div className={cn(styles.PostDetailed, {}, [className])}>
                {error.message}
            </div>
        )
    }

    return (
        <div className={cn(styles.PostDetailed, {}, [className])}>
            <Card className={styles.Post}>
                <h1 className={styles.Title}>{data?.title}</h1>
                <p className={styles.Content}>{data?.content}</p>
            </Card>
            {data?.comments && (
                <CommentsList
                    className={styles.PostComments}
                    comments={data.comments}
                />
            )}
        </div>
    )
})