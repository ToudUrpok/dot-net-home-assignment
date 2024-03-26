import styles from './LoginPage.module.scss'
import { memo } from 'react'

const LoginPage = memo(() => {
    return (
        <section className={styles.LoginPage}>
            <h1>Login Page</h1>
        </section>
    )
})

export default LoginPage