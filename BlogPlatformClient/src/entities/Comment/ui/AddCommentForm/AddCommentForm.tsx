import { Input } from '../../../../shared/ui/Input/Input'
import { cn } from '../../../../shared/lib/classNames/classNames'
import cls from './AddCommentForm.module.scss'
import { memo, useState } from 'react'
import { useLogin } from '../../../../feature/Authorization'
import { useMutation, useQueryClient } from '@tanstack/react-query'
import { postComment } from '../../model/api/postComment'
import { Button } from '../../../../shared/ui/Button/Button'

interface AddCommentFormProps {
    className?: string
    postId: string
}

export const AddCommentForm = memo(({ className, postId }: AddCommentFormProps) => {
    const { data: authToken } = useLogin()

    const queryClient = useQueryClient()
    const mutation = useMutation({
        mutationFn: postComment,
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: ['posts'] })
            setText('')
        },
        onError: (error: Error) => {
            alert(error.message)
        }
    })

    const [text, setText] = useState('')
    const [addBtnDisabled, setAddBtnDisabled] = useState(true)

    const handleTextInputChange = (value: string) => {
        if (value.trim().length) {
            setAddBtnDisabled(false)
        } else {
            setAddBtnDisabled(true)
        }
        setText(value)
    }

    const addComment = async () => {
        const commentText = text.trim()
        if (commentText.length && authToken) {
            setAddBtnDisabled(true)
            mutation.mutate({ authToken, data: { postId, text: commentText} })
        }
    }

    if (authToken) {
        return (
            <div className={cn(cls.AddCommentForm, {}, [className])}>
                <Input
                    className={cls.CommentInput}
                    placeholder='Enter your comment'
                    value={text}
                    onChange={handleTextInputChange}
                />
                <Button
                    onClick={addComment}
                    disabled={addBtnDisabled}
                >
                    Add Comment
                </Button>
            </div>
        )
    }
})
