import { Suspense, memo, useCallback } from 'react'
import { Route, Routes } from 'react-router-dom'
import { AppRouteProps, routeConfig } from '../../../shared/config/routeConfig/routeConfig'
import { PageLoader } from '../../../widgets/PageLoader'

const AppRouter = () => {
    const renderRoute = useCallback((routeProps: AppRouteProps) => {
        return (
            <Route
                key={routeProps.path}
                path={routeProps.path}
                element= { routeProps.element }
            />
        )
    }, [])

    return (
        <Suspense fallback={<PageLoader />}>
            <Routes>
                { Object.values(routeConfig).map(renderRoute) }
            </Routes>
        </Suspense>
    )
}

export default memo(AppRouter)
