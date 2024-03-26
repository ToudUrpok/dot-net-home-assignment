import { cn } from '../../../../shared/lib/classNames/classNames'
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
            <h2>{post.title}</h2>
            <p>{post.content}</p>
        </Card>
    )
})
