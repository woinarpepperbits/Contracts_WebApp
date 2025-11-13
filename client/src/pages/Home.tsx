import { Link } from 'react-router-dom'
import './Home.css'

function Home() {
  return (
    <div className="home">
      <div className="hero">
        <h1>Willkommen bei Contracts WebApp</h1>
        <p className="subtitle">
          Moderne Verwaltung von Vertrags-Sonderkunden für EVUs
        </p>
      </div>

      <div className="features">
        <div className="feature-card">
          <h3>📝 Verträge verwalten</h3>
          <p>Erstellen, bearbeiten und verwalten Sie Ihre Vertrags-Sonderkunden effizient.</p>
          <Link to="/contracts" className="feature-link">
            Zu den Verträgen →
          </Link>
        </div>

        <div className="feature-card">
          <h3>🔍 Suche & Filter</h3>
          <p>Finden Sie schnell die gewünschten Verträge mit leistungsstarker Suche.</p>
        </div>

        <div className="feature-card">
          <h3>💰 Preismodelle</h3>
          <p>Verwalten Sie komplexe Preisstrukturen und Vertragskonditionen.</p>
        </div>

        <div className="feature-card">
          <h3>👥 Kunden zuordnen</h3>
          <p>Ordnen Sie mehrere Kunden einem Vertrag zu und verwalten Sie Abschläge.</p>
        </div>
      </div>

      <div className="info-section">
        <h2>System-Information</h2>
        <ul>
          <li><strong>Backend API:</strong> http://localhost:5166</li>
          <li><strong>Swagger UI:</strong> <a href="http://localhost:5166/swagger" target="_blank">API-Dokumentation</a></li>
          <li><strong>Version:</strong> MVP 1.0</li>
          <li><strong>Status:</strong> ✅ Entwicklung</li>
        </ul>
      </div>
    </div>
  )
}

export default Home
