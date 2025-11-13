import { Routes, Route, Link, useLocation } from 'react-router-dom'
import './App.css'
import ContractList from './pages/ContractList'
import ContractDetail from './pages/ContractDetail'
import ContractCreate from './pages/ContractCreate'
import Home from './pages/Home'

function App() {
  const location = useLocation()

  return (
    <div className="app">
      <header className="header">
        <h1>📋 Contracts WebApp</h1>
        <nav className="nav">
          <Link to="/" className={location.pathname === '/' ? 'active' : ''}>
            Home
          </Link>
          <Link to="/contracts" className={location.pathname === '/contracts' ? 'active' : ''}>
            Verträge
          </Link>
        </nav>
      </header>

      <main>
        <Routes>
          <Route path="/" element={<Home />} />
          <Route path="/contracts" element={<ContractList />} />
          <Route path="/contracts/:id" element={<ContractDetail />} />
          <Route path="/contracts/new" element={<ContractCreate />} />
        </Routes>
      </main>
    </div>
  )
}

export default App
