import styles from './Navbar.module.scss'
import { cn } from '../../../shared/lib/classNames/classNames'
import { memo, useCallback } from 'react'
import { Button } from '../../../shared/ui/Button/Button'
import { selectUserAuthToken, userActions } from '../../../entities/User'
import { RoutePaths } from '../../../shared/config/routeConfig/routeConfig'
import { AppLink } from '../../../shared/ui/AppLink/AppLink'
import { useAppSelector } from '../../../shared/hooks/useAppSelector'
import { useAppDispatch } from '../../../shared/hooks/useAppDispatch'

interface NavbarProps {
    className?: string
}

export const Navbar = memo(({ className }: NavbarProps) => {
    const dispatch = useAppDispatch()
    const auth = useAppSelector(selectUserAuthToken)

    const logOut = useCallback(() => {
        dispatch(userActions.logOut())
    }, [dispatch])

    return (
        <header className={cn(styles.Navbar, {}, [className])}>
            <div className={styles.Links}>
                <div className={styles.Link}>
                    {auth ? (
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