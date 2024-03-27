import { Suspense, useEffect } from 'react'
import { Navbar } from '../widgets/Navbar'
import { Sidebar } from '../widgets/Sidebar'
import { AppRouter } from './router'
import './styles/index.scss'
import { useAppDispatch } from '../shared/hooks/useAppDispatch'
import { userActions } from '../entities/User'
import { fetchUser } from '../entities/User'

const App = () => {
  const dispatch = useAppDispatch()

  useEffect(() => {
    dispatch(userActions.initAuth())
    dispatch(fetchUser())
  }, [dispatch])

  return (
    <div className='app'>
      <Suspense fallback=''>
        <Navbar />
        <div className='content-page'>
          <Sidebar />
          <AppRouter />
        </div>
      </Suspense>
    </div>
  )
}

export default App
