import cls from './BlogPage.module.scss'
import { memo } from 'react'
import smileIcon from '/smiley-icon.svg'

const BlogPage = memo(() => {
    return (
        <div className={cls.blogPage}>
            <h1>Blog Main Page</h1>
            <img src={smileIcon.toString()} className={cls.logo} alt=":)" />
        </div>
    )
})

export default BlogPage