import styles from './BlogPage.module.scss'
import { memo } from 'react'
import smileIcon from '/smiley-icon.svg'
import { PostsList, usePosts } from '../../../entities/Post'

const BlogPage = memo(() => {
    const { error, data, isFetching } = usePosts()

    return (
        <section className={styles.BlogPage}>
            <div className={styles.BlogPageContent}>
                <h1>Blog Main Page</h1>
                <img src={smileIcon} className={styles.logo} alt=":)" />
                {error && (
                    <div>
                        {`${error.name}\n${error.message}`}
                    </div>
                )}
                {(error === null) && (
                    <PostsList
                        className={styles.Posts}
                        isLoading={isFetching}
                        posts={data}
                    />
                )}
            </div>
        </section>
    )
})

export default BlogPage