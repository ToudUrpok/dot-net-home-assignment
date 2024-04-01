import { ReactNode } from 'react'
import { RoutePaths } from '../../../shared/config/routeConfig/routeConfig'
import {
    Navigate,
    useLocation
} from 'react-router-dom'
import { useLogin } from '../../../feature/Authorization'

interface RequireAuthProps {
    children?: ReactNode
}

export const RequireAuth = ({ children }: RequireAuthProps) => {
    const { data } = useLogin()
    const location = useLocation()

    if (!data) {
        return <Navigate to={RoutePaths.login} state={{ from: location }} replace />
    }

    return children
}
