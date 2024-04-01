import { ReactNode } from 'react'
import { cn } from '../../../shared/lib/classNames/classNames'
import styles from './Page.module.scss'

interface PageProps {
    className?: string
    children: ReactNode
    onScrollEnd?: () => void
}

export const Page = (props: PageProps) => {
    const {
        className,
        children
    } = props

    return (
        <section className={cn(styles.Page, {}, [className])} >
            {children}
        </section>
    )
}
