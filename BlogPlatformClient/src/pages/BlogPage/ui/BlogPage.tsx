import styles from './BlogPage.module.scss'
import { memo } from 'react'
import { PostsList } from '../../../entities/Post'
import { Page } from '../../../widgets/Page'
import { AppLink } from '../../../shared/ui/AppLink/AppLink'
import { RoutePaths } from '../../../shared/config/routeConfig/routeConfig'
import PlusIcon from '../../../shared/assets/icons/icons8-plus.svg?react'

const BlogPage = () => {
    return (
        <Page className={styles.BlogPage}>
            <AppLink
                theme={'contrast'}
                to={RoutePaths.createPost}
            >
                <PlusIcon className={styles.AddPostIcon} />
            </AppLink>
            <PostsList className={styles.Posts} />
        </Page>
    )
}

export default memo(BlogPage)