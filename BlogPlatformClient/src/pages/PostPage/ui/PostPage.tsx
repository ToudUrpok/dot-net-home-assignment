import { useParams } from 'react-router-dom'
import { cn } from '../../../shared/lib/classNames/classNames'
import { Page } from '../../../widgets/Page'
import styles from './PostPage.module.scss'
import { memo } from 'react'
import { PostDetailed } from '../../../entities/Post'

interface PostPageProps {
    className?: string
}

const PostPage = (props: PostPageProps) => {
    const {
        className
    } = props
    const { id } = useParams()

    return (
        <Page className={cn(styles.PostPage, {}, [className])}>
            {id && (
                <PostDetailed
                    postId={id}
                />
            )}
        </Page>
    )
}

export default memo(PostPage)