import styles from './Button.module.scss'
import { cn } from '../../lib/classNames/classNames'
import { ButtonHTMLAttributes, ReactNode, memo } from 'react'

export type ButtonTheme = 'plain' | 'outlined' | 'background' | 'backgroundInverted' | 'delete'
export type ButtonSize = 'm' | 'l' | 'xl'

interface ButtonProps extends ButtonHTMLAttributes<HTMLButtonElement> {
    className?: string
    theme?: ButtonTheme
    size?: ButtonSize
    disabled?: boolean
    children?: ReactNode
}

export const Button = memo((props: ButtonProps) => {
    const {
        className,
        children,
        theme = 'outlined',
        size = 'm',
        disabled,
        ...otherProps
    } = props

    const attributes: Record<string, boolean | undefined> = {
        [styles.disabled]: disabled
    }

    return (
        <button
            className={cn(styles.Button, attributes, [className, styles[theme], styles[size]])}
            {...otherProps}
        >
            { children }
        </button>
    )
})
