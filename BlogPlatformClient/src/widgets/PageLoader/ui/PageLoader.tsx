import styles from './PageLoader.module.scss'
import { Loader } from '../../../shared/ui/Loader/Loader'

export const PageLoader = () => {
    return (
        <div className={styles.PageLoader}>
            <Loader />
        </div>
    )
}
