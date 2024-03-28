import styles from './BlogPage.module.scss'
import { memo } from 'react'
/* import smileIcon from '/smiley-icon.svg' */
import { PostsList, usePosts } from '../../../entities/Post'
import { Page } from '../../../widgets/Page'
import { AppLink } from '../../../shared/ui/AppLink/AppLink'
import { RoutePaths } from '../../../shared/config/routeConfig/routeConfig'

const BlogPage = () => {
    const { error, data, isFetching } = usePosts()
    /* const auth =  */

    return (
        <Page className={styles.BlogPage}>
            {/* <img src={smileIcon} className={styles.logo} alt=":)" /> */}
            <div>
                <AppLink
                    theme={'contrast'}
                    to={RoutePaths.createPost}
                >
                    
                </AppLink>
            </div>
            <div className={styles.BlogPageContent}>
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
        </Page>
    )
}

export default memo(BlogPage)