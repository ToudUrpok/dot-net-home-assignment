import { ReactNode } from 'react'
import { RoutePaths } from '../../../shared/config/routeConfig/routeConfig'
import {
    Navigate,
    useLocation
} from 'react-router-dom'
import { selectUserAuthToken } from '../../../entities/User'
import { useAppSelector } from '../../../shared/hooks/useAppSelector'

interface RequireAuthProps {
    children?: ReactNode
}

export const RequireAuth = ({ children }: RequireAuthProps) => {
    const auth = useAppSelector(selectUserAuthToken)
    const location = useLocation()

    if (!auth) {
        return <Navigate to={RoutePaths.login} state={{ from: location }} replace />
    }

    return children
}
