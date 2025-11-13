import { useState } from 'react'
import { useQuery } from '@tanstack/react-query'
import { Link } from 'react-router-dom'
import { contractService } from '../services/contract.service'
import type { ContractStatus } from '../types/contract.types'
import { format } from 'date-fns'
import './ContractList.css'

function ContractList() {
  const [search, setSearch] = useState('')
  const [statusFilter, setStatusFilter] = useState<ContractStatus | ''>('')
  const [page, setPage] = useState(1)
  const pageSize = 25

  const { data, isLoading, error } = useQuery({
    queryKey: ['contracts', search, statusFilter, page, pageSize],
    queryFn: () => contractService.getContracts({
      search: search || undefined,
      status: statusFilter !== '' ? statusFilter : undefined,
      page,
      pageSize,
    }),
  })

  const getStatusBadge = (status: ContractStatus) => {
    const badges: Record<ContractStatus, string> = {
      [0]: 'status-negotiation',
      [1]: 'status-active',
      [2]: 'status-terminated',
      [3]: 'status-ended',
      [4]: 'status-suspended',
    }
    return badges[status] || 'status-default'
  }

  if (error) {
    return (
      <div className="error">
        <h2>Fehler beim Laden der Verträge</h2>
        <p>{(error as Error).message}</p>
      </div>
    )
  }

  return (
    <div className="contract-list">
      <div className="list-header">
        <h2>Vertrags-Sonderkunden</h2>
        <Link to="/contracts/new" className="btn btn-primary">
          + Neuer Vertrag
        </Link>
      </div>

      <div className="filters">
        <input
          type="text"
          placeholder="Suche nach Vertragsnummer, Kunde..."
          value={search}
          onChange={(e) => {
            setSearch(e.target.value)
            setPage(1)
          }}
          className="search-input"
        />
        
        <select
          value={statusFilter}
          onChange={(e) => {
            setStatusFilter(e.target.value === '' ? '' : Number(e.target.value) as ContractStatus)
            setPage(1)
          }}
          className="filter-select"
        >
          <option value="">Alle Status</option>
          <option value="0">In Verhandlung</option>
          <option value="1">Aktiv</option>
          <option value="2">Gekündigt</option>
          <option value="3">Beendet</option>
          <option value="4">Ausgesetzt</option>
        </select>
      </div>

      {isLoading ? (
        <div className="loading">Lade Verträge...</div>
      ) : data && data.data.length > 0 ? (
        <>
          <div className="table-container">
            <table>
              <thead>
                <tr>
                  <th>Vertragsnummer</th>
                  <th>Kunde</th>
                  <th>Mandant</th>
                  <th>Status</th>
                  <th>Beginn</th>
                  <th>Ende</th>
                  <th>Aktion</th>
                </tr>
              </thead>
              <tbody>
                {data.data.map((contract) => (
                  <tr key={contract.id}>
                    <td>
                      <Link to={`/contracts/${contract.id}`} className="contract-link">
                        {contract.contractNumber}
                      </Link>
                    </td>
                    <td>
                      <div className="customer-info">
                        <strong>{contract.customerName}</strong>
                        <span className="customer-number">{contract.customerNumber}</span>
                      </div>
                    </td>
                    <td>{contract.mandantName}</td>
                    <td>
                      <span className={`status-badge ${getStatusBadge(contract.status)}`}>
                        {contract.statusDisplay}
                      </span>
                    </td>
                    <td>{format(new Date(contract.startDate), 'dd.MM.yyyy')}</td>
                    <td>
                      {contract.isUnlimited 
                        ? '∞ Unbefristet' 
                        : contract.endDate 
                          ? format(new Date(contract.endDate), 'dd.MM.yyyy')
                          : '-'
                      }
                    </td>
                    <td>
                      <Link to={`/contracts/${contract.id}`} className="btn btn-small">
                        Details
                      </Link>
                    </td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>

          <div className="pagination">
            <button
              onClick={() => setPage(p => Math.max(1, p - 1))}
              disabled={page === 1}
              className="btn"
            >
              ← Zurück
            </button>
            <span className="page-info">
              Seite {page} | Gesamt: {data.totalCount} Verträge
            </span>
            <button
              onClick={() => setPage(p => p + 1)}
              disabled={data.data.length < pageSize}
              className="btn"
            >
              Weiter →
            </button>
          </div>
        </>
      ) : (
        <div className="no-data">
          <p>Keine Verträge gefunden.</p>
          <Link to="/contracts/new" className="btn btn-primary">
            Ersten Vertrag erstellen
          </Link>
        </div>
      )}
    </div>
  )
}

export default ContractList
