import styles from './Navbar.module.scss'
import { cn } from '../../../shared/lib/classNames/classNames'
import { memo, useCallback } from 'react'
import { Button } from '../../../shared/ui/Button/Button'
import { RoutePaths } from '../../../shared/config/routeConfig/routeConfig'
import { AppLink } from '../../../shared/ui/AppLink/AppLink'
import { logout, useLogin } from '../../../feature/Authorization'
import { useMutation, useQueryClient } from '@tanstack/react-query'

interface NavbarProps {
    className?: string
}

export const Navbar = memo(({ className }: NavbarProps) => {
    const { data } = useLogin()
    const queryClient = useQueryClient()
    const mutation = useMutation({
        mutationFn: logout,
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: ['login'] })
        }
    })

    const logOut = useCallback(() => {
        mutation.mutate()
    }, [])

    return (
        <header className={cn(styles.Navbar, {}, [className])}>
            <div className={styles.Links}>
                <div className={styles.Link}>
                    {data ? (
                        <Button
                            theme={'backgroundInverted'}
                            size={'l'}
                            onClick={logOut}
                        >
                                {'Log Out'}
                        </Button>
                    ) : (
                        <AppLink
                            theme={'contrast'}
                            to={RoutePaths.login}
                        >
                            {'Log In'}
                        </AppLink>
                    )}
                </div>
            </div>
        </header>
    )
})