import styles from './Input.module.scss'
import { cn } from '../../../shared/lib/classNames/classNames'
import { ChangeEvent, InputHTMLAttributes, memo } from 'react'

type HTMLInputProps = Omit<InputHTMLAttributes<HTMLInputElement>, 'onChange'>

interface InputProps extends HTMLInputProps {
    className?: string
    value?: string | number
    onChange?: (value: string) => void
    readOnly?: boolean
}

export const Input = memo((props: InputProps) => {
    const {
        className,
        value,
        onChange,
        readOnly,
        ...otherProps
    } = props

    const changeEventHandler = (e: ChangeEvent<HTMLInputElement>) => {
        onChange?.(e.target.value)
    }

    const attributes: Record<string, boolean | undefined> = {
        [styles.readonly]: readOnly
    }

    return (
        <div
            className={cn(styles.Input, attributes, [className])}
        >
            <input
                className={styles.Input_Input}
                value={value}
                onChange={changeEventHandler}
                readOnly={readOnly}
                {...otherProps}
            />
        </div>
    )
})
