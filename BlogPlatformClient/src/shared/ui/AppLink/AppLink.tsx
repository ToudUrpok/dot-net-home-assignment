import styles from './AppLink.module.scss'
import { cn } from '../../lib/classNames/classNames'
import { Link, LinkProps } from 'react-router-dom'
import { ReactNode, memo } from 'react'

export type AppLinkTheme = 'primary' | 'secondary'| 'contrast'

interface AppLinkProps extends LinkProps {
    className?: string
    theme?: AppLinkTheme
    children?: ReactNode
}

export const AppLink = memo((props: AppLinkProps) => {
    const { 
        to,
        className,
        children,
        theme = 'primary',
        ...otherProps
    } = props

    return (
        <Link
            to={to}
            className={cn(styles.AppLink, {}, [className, styles[theme]])}
            {...otherProps}
        >
            { children }
        </Link>
    )
})
