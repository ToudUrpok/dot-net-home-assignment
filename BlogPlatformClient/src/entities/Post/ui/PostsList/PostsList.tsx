import { Post } from '../..'
import { cn } from '../../../../shared/lib/classNames/classNames'
import { Loader } from '../../../../shared/ui/Loader/Loader'
import { PostCard } from '../PostCard/PostCard'
import styles from './PostsList.module.scss'
import { memo } from 'react'

interface PostsListProps {
    className?: string
    isLoading: boolean
    posts?: Post[]
}

export const PostsList = memo((props: PostsListProps) => {
    const {
        className,
        isLoading,
        posts
    } = props

    if (isLoading) {
        return (
            <div className={cn(styles.PostsList, {}, [className])}>
                <Loader />
            </div>
        )
    }

    if (posts === undefined) {
        return null
    }

    if (posts && posts.length === 0) {
        return (
            <div className={cn(styles.PostsList, {}, [className])}>
                <h2>{'There are no posts to show.'}</h2>
            </div>
        )
    }

    return (
        <div className={cn(styles.PostsList, {}, [className])}>
            {posts && posts.map(post => (
                <PostCard
                    key={post.id}
                    post={post}
                />
            ))}
        </div>
    )
})
