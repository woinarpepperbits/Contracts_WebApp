import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query'
import { useParams, useNavigate, Link } from 'react-router-dom'
import { contractService } from '../services/contract.service'
import { format } from 'date-fns'
import './ContractDetail.css'

function ContractDetail() {
  const { id } = useParams<{ id: string }>()
  const navigate = useNavigate()
  const queryClient = useQueryClient()

  const { data: contract, isLoading, error } = useQuery({
    queryKey: ['contract', id],
    queryFn: () => contractService.getContract(id!),
    enabled: !!id,
  })

  const deleteMutation = useMutation({
    mutationFn: () => contractService.deleteContract(id!),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['contracts'] })
      navigate('/contracts')
    },
  })

  const handleDelete = () => {
    if (window.confirm('Möchten Sie diesen Vertrag wirklich löschen?')) {
      deleteMutation.mutate()
    }
  }

  if (isLoading) {
    return <div className="loading">Lade Vertrag...</div>
  }

  if (error) {
    return (
      <div className="error">
        <h2>Fehler beim Laden des Vertrags</h2>
        <p>{(error as Error).message}</p>
        <Link to="/contracts" className="btn">Zurück zur Liste</Link>
      </div>
    )
  }

  if (!contract) {
    return (
      <div className="error">
        <h2>Vertrag nicht gefunden</h2>
        <Link to="/contracts" className="btn">Zurück zur Liste</Link>
      </div>
    )
  }

  return (
    <div className="contract-detail">
      <div className="detail-header">
        <div>
          <h2>Vertrag {contract.contractNumber}</h2>
          <span className={`status-badge status-${contract.status}`}>
            {contract.statusDisplay}
          </span>
        </div>
        <div className="actions">
          <Link to="/contracts" className="btn">← Zurück</Link>
          <button onClick={handleDelete} className="btn btn-danger">
            🗑️ Löschen
          </button>
        </div>
      </div>

      <div className="detail-grid">
        {/* Grunddaten */}
        <div className="detail-section">
          <h3>Grunddaten</h3>
          <div className="detail-row">
            <label>Vertragsnummer:</label>
            <span>{contract.contractNumber}</span>
          </div>
          <div className="detail-row">
            <label>Typ:</label>
            <span>{contract.typeDisplay}</span>
          </div>
          <div className="detail-row">
            <label>Beschreibung:</label>
            <span>{contract.description || '-'}</span>
          </div>
        </div>

        {/* Vertragspartner */}
        <div className="detail-section">
          <h3>Vertragspartner</h3>
          <div className="detail-row">
            <label>Kunde:</label>
            <span>
              <strong>{contract.customerName}</strong><br />
              {contract.customerNumber}
            </span>
          </div>
          <div className="detail-row">
            <label>Mandant:</label>
            <span>{contract.mandantName}</span>
          </div>
          <div className="detail-row">
            <label>Vertragsgruppe:</label>
            <span>{contract.contractGroupName || '-'}</span>
          </div>
        </div>

        {/* Laufzeit */}
        <div className="detail-section">
          <h3>Laufzeit</h3>
          <div className="detail-row">
            <label>Beginn:</label>
            <span>{format(new Date(contract.startDate), 'dd.MM.yyyy')}</span>
          </div>
          <div className="detail-row">
            <label>Ende:</label>
            <span>
              {contract.isUnlimited 
                ? '∞ Unbefristet' 
                : contract.endDate 
                  ? format(new Date(contract.endDate), 'dd.MM.yyyy')
                  : '-'
              }
            </span>
          </div>
          <div className="detail-row">
            <label>Kündigungsfrist:</label>
            <span>{contract.terminationNoticePeriod} Monate</span>
          </div>
        </div>

        {/* Abrechnung */}
        <div className="detail-section">
          <h3>Abrechnung</h3>
          <div className="detail-row">
            <label>Währung:</label>
            <span>{contract.currencyCode}</span>
          </div>
          <div className="detail-row">
            <label>Abrechnungszyklus:</label>
            <span>{contract.billingCycle || '-'}</span>
          </div>
          <div className="detail-row">
            <label>SAP Vertragskonto:</label>
            <span>{contract.sapContractAccount || '-'}</span>
          </div>
          <div className="detail-row">
            <label>Budgetverantwortlicher:</label>
            <span>{contract.budgetResponsible || '-'}</span>
          </div>
        </div>

        {/* Verantwortliche Personen */}
        <div className="detail-section">
          <h3>Verantwortliche</h3>
          <div className="detail-row">
            <label>Vertragsverantwortlicher:</label>
            <span>{contract.responsiblePerson || '-'}</span>
          </div>
          <div className="detail-row">
            <label>Ansprechpartner:</label>
            <span>{contract.contactPerson || '-'}</span>
          </div>
        </div>

        {/* Bemerkungen */}
        {contract.notes && (
          <div className="detail-section full-width">
            <h3>Bemerkungen</h3>
            <div className="notes-box">
              {contract.notes}
            </div>
          </div>
        )}

        {/* Preise */}
        {contract.prices && contract.prices.length > 0 && (
          <div className="detail-section full-width">
            <h3>Preise ({contract.prices.length})</h3>
            <table className="prices-table">
              <thead>
                <tr>
                  <th>Preisart</th>
                  <th>Preis</th>
                  <th>Einheit</th>
                  <th>Gültig von</th>
                  <th>Gültig bis</th>
                  <th>Prognose Verbrauch</th>
                </tr>
              </thead>
              <tbody>
                {contract.prices.map((price) => (
                  <tr key={price.id}>
                    <td>{price.priceTypeName}</td>
                    <td>{price.price.toFixed(4)} {contract.currencyCode}</td>
                    <td>{price.unit || '-'}</td>
                    <td>{format(new Date(price.validFrom), 'dd.MM.yyyy')}</td>
                    <td>
                      {price.validTo 
                        ? format(new Date(price.validTo), 'dd.MM.yyyy')
                        : '∞'
                      }
                    </td>
                    <td>{price.forecastConsumption?.toFixed(2) || '-'}</td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>
        )}

        {/* Zugeordnete Kunden */}
        {contract.contractCustomers && contract.contractCustomers.length > 0 && (
          <div className="detail-section full-width">
            <h3>Zugeordnete Kunden ({contract.contractCustomers.length})</h3>
            <table className="customers-table">
              <thead>
                <tr>
                  <th>Kunde</th>
                  <th>Kundennummer</th>
                  <th>Rolle</th>
                  <th>Abschlag</th>
                  <th>Gültig von</th>
                  <th>Gültig bis</th>
                </tr>
              </thead>
              <tbody>
                {contract.contractCustomers.map((cc) => (
                  <tr key={cc.id}>
                    <td>{cc.customerName}</td>
                    <td>{cc.customerNumber}</td>
                    <td>{cc.roleDisplay}</td>
                    <td>{cc.depositAmount?.toFixed(2) || '-'} {contract.currencyCode}</td>
                    <td>{format(new Date(cc.validFrom), 'dd.MM.yyyy')}</td>
                    <td>
                      {cc.validTo 
                        ? format(new Date(cc.validTo), 'dd.MM.yyyy')
                        : '∞'
                      }
                    </td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>
        )}

        {/* Audit Info */}
        <div className="detail-section full-width audit-info">
          <h3>Audit</h3>
          <div className="detail-row">
            <label>Erstellt:</label>
            <span>
              {format(new Date(contract.createdAt), 'dd.MM.yyyy HH:mm')} 
              {contract.createdBy && ` von ${contract.createdBy}`}
            </span>
          </div>
          {contract.updatedAt && (
            <div className="detail-row">
              <label>Geändert:</label>
              <span>
                {format(new Date(contract.updatedAt), 'dd.MM.yyyy HH:mm')}
                {contract.updatedBy && ` von ${contract.updatedBy}`}
              </span>
            </div>
          )}
        </div>
      </div>
    </div>
  )
}

export default ContractDetail
