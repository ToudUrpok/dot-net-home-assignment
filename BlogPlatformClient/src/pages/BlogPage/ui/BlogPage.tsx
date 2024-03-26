import styles from './BlogPage.module.scss'
import { memo } from 'react'
import smileIcon from '/smiley-icon.svg'

const BlogPage = memo(() => {
    return (
        <section className={styles.BlogPage}>
            <h1>Blog Main Page</h1>
            <img src={smileIcon} className={styles.logo} alt=":)" />
        </section>
    )
})

export default BlogPage