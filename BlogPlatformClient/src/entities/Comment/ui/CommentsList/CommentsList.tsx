import { cn } from '../../../../shared/lib/classNames/classNames'
import cls from './CommentsList.module.scss'
import { Comment } from '../../model/types/comment'
import { CommentCard } from '../Comment/CommentCard'
import { memo } from 'react'

interface CommentsListProps {
    className?: string
    comments: Comment[]
}

export const CommentsList = memo((props: CommentsListProps) => {
    const {
        className,
        comments
    } = props

    return (
        <div className={cn(cls.CommentsListWrapper, {}, [className])}>
            <h2 className={cls.Title} >Comments</h2>
            <div className={cls.CommentsList}>
                {comments?.length
                    ? comments.map(c => (
                        <CommentCard
                            className={cls.CommentItem}
                            key={c.id}
                            comment={c}
                        />
                    ))
                    : <p className={cls.EmptyMessage}>There are no comments to the post.</p>
                }
            </div>
        </div>
    )
})
