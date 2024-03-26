import { Suspense } from 'react'
import { Navbar } from '../widgets/Navbar'
import { Sidebar } from '../widgets/Sidebar'
import { AppRouter } from './router'
import './styles/index.scss'

const App = () => {
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
