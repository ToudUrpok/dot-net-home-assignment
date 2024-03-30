import styles from './CreatePostForm.module.scss'
import { cn } from '../../../../shared/lib/classNames/classNames'
import { Button } from '../../../../shared/ui/Button/Button'
import { memo } from 'react'
import { 
    useForm, 
    SubmitHandler,
    Controller
} from 'react-hook-form'
import { CreatePostFormFields } from '../../model/types/createPostFormFields'
import { useMutation, useQueryClient } from '@tanstack/react-query'
import { postPost } from '../../model/api/postPost'
import { useAppSelector } from '../../../../shared/hooks/useAppSelector'
import { selectUserAuthToken } from '../../../User'
import { Input } from '../../../../shared/ui/Input/Input'

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
        control,
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
            <Controller
                name='title'
                control={control}
                rules={{
                    required: 'Title is required.',
                    maxLength: {
                        value: 100,
                        message: 'Post Title is max 100 characters long.'
                    }
                }}
                aria-invalid={errors.title ? 'true' : 'false'}
                render={({ field }) =>
                    <Input
                        placeholder='Title'
                        {...field}
                    />
                }
            />
            { errors.title && (
                <p className={styles.ValidationError}>
                    {errors.title.message}
                </p>
            )}
            
            <textarea
                className={styles.ContentField}
                placeholder='Content'
                {...register('content', {
                    required: "Post can't be empty.",
                    maxLength: {
                        value: 1000,
                        message: 'Post Content is max 1000 characters long.'
                    }
                })}
            />
            { errors.content && (
                <p className={styles.ValidationError}>
                    {errors.content.message}
                </p>
            )}
            
            <div className={styles.BtnsWrapper}>
                <Button
                    theme={'background'}
                    size='l'
                    type='submit'
                    disabled={!isDirty}
                >
                    Create Post
                </Button>
            </div>
        </form>
    )
})
