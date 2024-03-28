import { cn } from '../../../../shared/lib/classNames/classNames'
import styles from './CommentCard.module.scss'
import { Comment } from '../../model/types/comment'
import { memo } from 'react'

interface CommentCardProps {
    className?: string
    comment: Comment
}

export const CommentCard = memo((props: CommentCardProps) => {
    const {
        className,
        comment
    } = props

    return (
        <div className={cn(styles.CommentCard, {}, [className])}>
            <p>{comment.text}</p>
        </div>
    )
})
