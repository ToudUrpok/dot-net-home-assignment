import { RoutePaths } from '../../../../shared/config/routeConfig/routeConfig'
import { cn } from '../../../../shared/lib/classNames/classNames'
import { AppLink } from '../../../../shared/ui/AppLink/AppLink'
import { Card } from '../../../../shared/ui/Card/Card'
import { Post } from '../../model/types/post'
import styles from './PostCard.module.scss'
import { memo } from 'react'

interface PostCardProps {
    className?: string
    post: Post
}

export const PostCard = memo((props: PostCardProps) => {
    const {
        className,
        post
    } = props

    return (
        <Card className={cn(styles.PostCard, {}, [className])}>
            <h2 className={styles.Title}>{post.title}</h2>
            <p className={styles.Content}>{post.content}</p>
            <AppLink
                className={styles.ReadLink}
                theme={'contrast'}
                to={RoutePaths.post + post.id}
            >
                read
            </AppLink>
        </Card>
    )
})
