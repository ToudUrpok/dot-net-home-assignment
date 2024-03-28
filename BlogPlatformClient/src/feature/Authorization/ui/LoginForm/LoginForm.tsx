import styles from './LoginForm.module.scss'
import { cn } from '../../../../shared/lib/classNames/classNames'
import { Button } from '../../../../shared/ui/Button/Button'
import { Input } from '../../../../shared/ui/Input/Input'
import { memo } from 'react'
import { 
    useForm, 
    SubmitHandler, 
    Controller 
} from 'react-hook-form'
import { LoginFormFields } from '../../model/types/loginFormFields'
import { useAppDispatch } from '../../../../shared/hooks/useAppDispatch'
import { useAppSelector } from '../../../../shared/hooks/useAppSelector'
import { 
    fetchUser,
    loginUser, 
    selectUserError, 
    selectUserIsLoading 
} from '../../../../entities/User'

export interface LoginFormProps {
    className?: string
    onSuccess: () => void
    onFailed?: (error?: string) => void
}

const LoginForm = ({ className, onSuccess, onFailed }: LoginFormProps) => {
    const dispatch = useAppDispatch()
    const loginError = useAppSelector(selectUserError)
    const isLoading = useAppSelector(selectUserIsLoading)
    const {
        control,
        handleSubmit,
        formState: { errors, isDirty }
    } = useForm<LoginFormFields>({
        defaultValues: {}
    })

    const onSubmit: SubmitHandler<LoginFormFields> = async (data) => {
        if (data.email && data.password){
            const loginResult = await dispatch(loginUser({ email: data.email, password: data.password }))
            if (loginResult.meta.requestStatus === 'fulfilled') {
                await dispatch(fetchUser())
                onSuccess()
            } else {
                onFailed?.(loginError)
            }
        }
    }

    return (
        <form
            className={cn(styles.LoginForm, {}, [className])}
            onSubmit={handleSubmit(onSubmit)}
        >
            <Controller
                name='email'
                control={control}
                rules={{ required: true }}
                aria-invalid={errors.email ? 'true' : 'false'}
                render={({ field }) =>
                    <Input
                        type={'email'}
                        placeholder='your@email.com'
                        maxLength={320}
                        {...field}
                    />
                }
            />
            { errors.email && <p className={styles.ValidationError}>{errors.email.type}</p> }

            <Controller
                name='password'
                control={control}
                rules={{ required: true }}
                aria-invalid={errors.password ? 'true' : 'false'}
                render={({ field }) =>
                    <Input
                        type={'password'}
                        placeholder='password'
                        {...field}
                    />
                }
            />
            { errors.password && <p className={styles.ValidationError}>{errors.password.type}</p> }

            <Button
                theme={'background'}
                size='l'
                type='submit'
                disabled={!isDirty  || isLoading}
            >
                Log In
            </Button>

            {loginError && <p className={styles.LoginError}>{loginError}</p>}
        </form>
    )
}

export default memo(LoginForm)