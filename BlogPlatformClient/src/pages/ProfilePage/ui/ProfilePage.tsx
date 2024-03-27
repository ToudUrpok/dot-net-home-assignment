import { cn } from '../../../shared/lib/classNames/classNames'
import { Page } from '../../../widgets/Page'
import styles from './ProfilePage.module.scss'
import { memo } from 'react'

interface ProfilePageProps {
    className?: string
}

const ProfilePage = (props: ProfilePageProps) => {
    const {
        className
    } = props

    return (
        <Page className={cn(styles.ProfilePage, {}, [className])}>
            <h1>Profile Page</h1>
            <h3>If you are able to access this Page you are authorized.</h3>
        </Page>
    )
}

export default memo(ProfilePage)