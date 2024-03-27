import styles from './LoginPage.module.scss'
import { useLocation, useNavigate } from 'react-router-dom'
import { LoginForm } from '../../../feature/Authorization'
import { Page } from '../../../widgets/Page'
import { memo, useCallback } from 'react'

const LoginPage = () => {
    const navigate = useNavigate()
    const location = useLocation()

    const navigateBack = useCallback(() => {
        const origin = location.state?.from?.pathname
        navigate(origin ? origin : -1)
    }, [location.state?.from?.pathname, navigate])

    return (
        <Page className={styles.LoginPage}>
            <h1>Login Page</h1>
            <LoginForm onSuccess={navigateBack} />
        </Page>
    )
}

export default memo(LoginPage)