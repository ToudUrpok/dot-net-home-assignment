import { cn } from '../../../shared/lib/classNames/classNames'
import styles from './Card.module.scss'
import { HTMLAttributes, ReactNode, memo } from 'react'

export type CardTheme = 'normal' | 'outlined' | 'contrast'

interface CardProps extends HTMLAttributes<HTMLDivElement> {
    className?: string
    children?: ReactNode
    theme?: CardTheme
}

export const Card = memo((props: CardProps) => {
    const {
        className,
        children,
        theme = 'normal',
        ...otherProps
    } = props

    return (
        <div
            className={cn(styles.Card, {}, [className, styles[theme]])}
            {...otherProps}
        >
            {children}
        </div>
    )
})
