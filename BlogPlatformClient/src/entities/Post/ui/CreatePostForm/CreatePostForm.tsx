import styles from './CreatePostForm.module.scss'
import { cn } from '../../../../shared/lib/classNames/classNames'
import { Button } from '../../../../shared/ui/Button/Button'
import { memo } from 'react'
import { 
    useForm, 
    SubmitHandler
} from 'react-hook-form'
import { CreatePostFormFields } from '../../model/types/createPostFormFields'
import { useMutation, useQueryClient } from '@tanstack/react-query'
import { postPost } from '../../model/api/postPost'
import { useAppSelector } from '../../../../shared/hooks/useAppSelector'
import { selectUserAuthToken } from '../../../User'

export interface CreatePostFormProps {
    className?: string
    onSuccess: () => void
    onFailed?: (error?: string) => void
}

export const CreatePostForm = memo(({ className, onSuccess, onFailed }: CreatePostFormProps) => {
    const authToken = useAppSelector(selectUserAuthToken)

    const queryClient = useQueryClient()
    const mutation = useMutation({
        mutationFn: postPost,
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: ['posts'] })
            onSuccess()
        },
        onError: (error: Error) => {
            onFailed?.(error.message)
        }
    })

    const {
        register,
        handleSubmit,
        formState: { errors, isDirty }
    } = useForm<CreatePostFormFields>()

    const onSubmit: SubmitHandler<CreatePostFormFields> = async (data) => {
        if (data.title && data.content && authToken){
            mutation.mutate({ authToken, data})
        }
    }

    return (
        <form
            className={cn(styles.CreatePostForm, {}, [className])}
            onSubmit={handleSubmit(onSubmit)}
        >
            <textarea {...register('title', { required: true, maxLength: 100 })} />
            { errors.title && <p className={styles.ValidationError}>{errors.title.type}</p> }

            <textarea {...register('content', { required: true, maxLength: 1000 })} />
            { errors.content && <p className={styles.ValidationError}>{errors.content.type}</p> }

            <Button
                theme={'background'}
                size='l'
                type='submit'
                disabled={!isDirty}
            >
                Create Post
            </Button>
        </form>
    )
})
