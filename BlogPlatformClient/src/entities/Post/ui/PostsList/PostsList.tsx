import { usePosts } from '../../model/queries/usePosts'
import { cn } from '../../../../shared/lib/classNames/classNames'
import { Loader } from '../../../../shared/ui/Loader/Loader'
import { PostCard } from '../PostCard/PostCard'
import styles from './PostsList.module.scss'
import { memo } from 'react'
import { Button } from '../../../../shared/ui/Button/Button'
import { useMutation, useQueryClient } from '@tanstack/react-query'
import { deletePost } from '../../model/api/deletePost'
import { useLogin } from '../../../../feature/Authorization'

interface PostsListProps {
    className?: string
}

export const PostsList = memo(({ className }: PostsListProps) => {
    const { error, data, isFetching } = usePosts()
    const queryClient = useQueryClient()
    const mutation = useMutation({
        mutationFn: deletePost,
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: ['posts'] })
        },
        onError: (error: Error) => {
            alert(error.message)
        }
    })

    const { data: authToken } = useLogin()

    const handleDeleteBtnClick = (postId: number) => () => {
        if (authToken) {
            mutation.mutate({ authToken, postId })
        }
    }

    if (isFetching) {
        return (
            <div className={cn(styles.PostsList, {}, [className])}>
                <Loader />
            </div>
        )
    }

    if (error) {
        return (
            <div className={cn(styles.PostsList, {}, [className])}>
                <p className={styles.Error}>{`${error.name}\n${error.message}`}</p>
            </div>
        )
    }

    if (data && data.length === 0) {
        return (
            <div className={cn(styles.PostsList, {}, [className])}>
                <h2>{'There are no posts to show.'}</h2>
            </div>
        )
    }

    return (
        <div className={cn(styles.PostsList, {}, [className])}>
            {data && data.map(post => (
                <div className={styles.PostWrapper}>
                    <PostCard
                        key={post.id}
                        post={post}
                    />
                    {authToken && (
                        <div className={styles.DeleteBtnWrapper}>
                            <Button
                                onClick={handleDeleteBtnClick(post.id)}
                                theme={'delete'}
                            >
                                Delete
                            </Button>
                        </div>
                        
                    )}
                </div>
            ))}
        </div>
    )
})
