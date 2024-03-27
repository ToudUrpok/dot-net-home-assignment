import styles from './LoginForm.module.scss'
import { cn } from '../../../../shared/lib/classNames/classNames'
import { Button } from '../../../../shared/ui/Button/Button'
import { Input } from '../../../../shared/ui/Input/Input'
import { memo } from 'react'
import { useForm, SubmitHandler, Controller } from 'react-hook-form'
import { LoginFormFields } from '../../model/types/loginFormFields'
import { ReducersList, useDynamicReducer } from '../../../../shared/hooks/useDynamicReducer'
import {
    reducer as loginReducer,
    selectLoginError,
    selectLoginIsLoading
} from '../../model/slice/loginSlice'
import { login } from '../../model/services/login'
import { useAppDispatch } from '../../../../shared/hooks/useAppDispatch'
import { useAppSelector } from '../../../../shared/hooks/useAppSelector'

export interface LoginFormProps {
    className?: string
    onSuccess: () => void
}

const reducersToLoad: ReducersList = {
    login: loginReducer
}

const LoginForm = ({ className, onSuccess }: LoginFormProps) => {
    useDynamicReducer(reducersToLoad, true)
    const dispatch = useAppDispatch()
    const error = useAppSelector(selectLoginError)
    const isLoading = useAppSelector(selectLoginIsLoading)
    const {
        control,
        handleSubmit,
        formState: { errors, isDirty }
    } = useForm<LoginFormFields>({
        defaultValues: {}
    })

    const onSubmit: SubmitHandler<LoginFormFields> = async (data) => {
        const result = await dispatch(login(data))
        if (result.meta.requestStatus === 'fulfilled') {
            onSuccess()
        }
    }

    return (
        <div >
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
                            className={styles.InputField}
                            type={'email'}
                            placeholder='your@email.com'
                            maxLength={320}
                            {...field}
                        />
                    }
                />
                { errors.email && <p>{errors.email.type}</p> }

                <Controller
                    name='password'
                    control={control}
                    rules={{ required: true }}
                    aria-invalid={errors.password ? 'true' : 'false'}
                    render={({ field }) =>
                        <Input
                            className={styles.InputField}
                            type={'password'}
                            placeholder='password'
                            {...field}
                        />
                    }
                />
                { errors.password && <p>{errors.password.type}</p> }

                <div>
                    <Button
                        className={styles.LoginBtn}
                        theme={'outlined'}
                        type='submit'
                        disabled={!isDirty && !isLoading}
                    >
                        Log In
                    </Button>
                </div>
            </form>

            {error && <p>{error}</p>}
        </div>
    )
}

export default memo(LoginForm)
