import styles from './Button.module.scss'
import { cn } from '../../lib/classNames/classNames'
import { ButtonHTMLAttributes, ReactNode, memo } from 'react'

export type ButtonTheme = 'plain' | 'outlined' | 'background' | 'backgroundInverted'
export type ButtonSize = 'm' | 'l' | 'xl'

interface ButtonProps extends ButtonHTMLAttributes<HTMLButtonElement> {
    theme?: ButtonTheme
    size?: ButtonSize
    disabled?: boolean
    children?: ReactNode
}

export const Button = memo((props: ButtonProps) => {
    const {
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
            className={cn(styles.Button, attributes, [styles[theme], styles[size]])}
            {...otherProps}
        >
            { children }
        </button>
    )
})
