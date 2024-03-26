import cls from './Navbar.module.scss'
import { memo } from 'react'
import {
    useNavigate,
    useLocation
} from 'react-router-dom'
import { Button } from '../../../shared/ui/Button/Button'

export const Navbar = memo(() => {
    const navigate = useNavigate()
    const location = useLocation()

    return (
        <header className={cls.Navbar}>
            <div className={cls.Links}>
                <div className={cls.Link}>
                    <Button
                        theme={'backgroundInverted'}
                        size={'l'}
                    >
                            {'Log In'}
                    </Button>
                </div>
            </div>
        </header>
    )
})