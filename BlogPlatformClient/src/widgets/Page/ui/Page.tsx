import { MutableRefObject, ReactNode, useRef } from 'react'
import { cn } from '../../../shared/lib/classNames/classNames'
import styles from './Page.module.scss'
import { useIntersectionObserver } from '../../../shared/hooks/useIntersectionObserver'

interface PageProps {
    className?: string
    children: ReactNode
    onScrollEnd?: () => void
}

export const Page = (props: PageProps) => {
    const {
        className,
        children,
        onScrollEnd
    } = props

    const pageRef = useRef() as MutableRefObject<HTMLDivElement>
    const endOfPageRef = useRef() as MutableRefObject<HTMLDivElement>

    useIntersectionObserver({
        rootRef: pageRef,
        targetRef: endOfPageRef,
        callback: onScrollEnd
    })

    return (
        <section
            className={cn(styles.Page, {}, [className])}
            ref={pageRef}
        >
            {children}
            {onScrollEnd ? <div ref={endOfPageRef}/> : null}
        </section>
    )
}
