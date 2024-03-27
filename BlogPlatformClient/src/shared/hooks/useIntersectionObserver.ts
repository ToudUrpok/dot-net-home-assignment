import { MutableRefObject, useEffect } from 'react'

export interface UseIntersectionObserverProps {
    rootRef: MutableRefObject<HTMLElement>
    targetRef: MutableRefObject<HTMLElement>
    callback?: () => void
}

export const useIntersectionObserver = ({ rootRef, targetRef, callback }: UseIntersectionObserverProps) => {
    useEffect(() => {
        const rootElem = rootRef.current
        const targetElem = targetRef.current

        if (callback && targetElem) {
            const options = {
                root: rootElem,
                rootMargin: '1px'
            }

            const observer = new IntersectionObserver(([entry]) => {
                if (entry.isIntersecting) {
                    callback()
                }
            }, options)

            observer.observe(targetElem)

            return () => {
                observer?.unobserve(targetElem)
            }
        }
    }, [rootRef, targetRef, callback])
}
