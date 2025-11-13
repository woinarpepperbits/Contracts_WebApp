import { useState, useEffect } from 'react'
import { useNavigate } from 'react-router-dom'
import { useMutation, useQuery } from '@tanstack/react-query'
import { contractService } from '../services/contract.service'
import { lookupService } from '../services/lookup.service'
import type { CreateContractDto, ContractStatus, ContractType } from '../types/contract.types'
import './ContractCreate.css'

function ContractCreate() {
  const navigate = useNavigate()
  const [step, setStep] = useState(1)

  // Lookup-Daten laden
  const { data: customers } = useQuery({
    queryKey: ['customers'],
    queryFn: lookupService.getCustomers,
  })

  const { data: mandants } = useQuery({
    queryKey: ['mandants'],
    queryFn: lookupService.getMandants,
  })

  const { data: contractGroups } = useQuery({
    queryKey: ['contractGroups'],
    queryFn: lookupService.getContractGroups,
  })

  const { data: currencies } = useQuery({
    queryKey: ['currencies'],
    queryFn: lookupService.getCurrencies,
  })

  // Formular State
  const [formData, setFormData] = useState<CreateContractDto>({
    contractNumber: '',
    customerId: '',
    mandantId: '',
    contractGroupId: undefined,
    currencyId: '',
    status: 0 as ContractStatus,
    type: 0 as ContractType,
    startDate: new Date().toISOString().split('T')[0],
    endDate: undefined,
    isUnlimited: true,
    terminationNoticePeriod: 3,
    description: '',
    notes: '',
    billingCycle: '',
    sapContractAccount: '',
    budgetResponsible: '',
    responsiblePerson: '',
    contactPerson: '',
  })

  // EUR als Standard-Währung setzen
  useEffect(() => {
    if (currencies && currencies.length > 0 && !formData.currencyId) {
      const eur = currencies.find(c => c.value === 'EUR')
      if (eur) {
        setFormData(prev => ({ ...prev, currencyId: eur.id }))
      }
    }
  }, [currencies, formData.currencyId])

  const createMutation = useMutation({
    mutationFn: contractService.createContract,
    onSuccess: (data) => {
      navigate(`/contracts/${data.id}`)
    },
  })

  const handleChange = (field: keyof CreateContractDto, value: any) => {
    setFormData(prev => ({ ...prev, [field]: value }))
  }

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault()
    createMutation.mutate(formData)
  }

  const isStep1Valid = () => {
    return formData.contractNumber && formData.customerId && formData.mandantId && formData.currencyId
  }

  const isStep2Valid = () => {
    return formData.startDate && (formData.isUnlimited || formData.endDate)
  }

  return (
    <div className="contract-create">
      <div className="create-header">
        <h2>Neuen Vertrag erstellen</h2>
        <div className="wizard-steps">
          <div className={`step ${step >= 1 ? 'active' : ''}`}>
            <span className="step-number">1</span>
            <span className="step-label">Grunddaten</span>
          </div>
          <div className={`step ${step >= 2 ? 'active' : ''}`}>
            <span className="step-number">2</span>
            <span className="step-label">Laufzeit</span>
          </div>
          <div className={`step ${step >= 3 ? 'active' : ''}`}>
            <span className="step-number">3</span>
            <span className="step-label">Details</span>
          </div>
        </div>
      </div>

      <form onSubmit={handleSubmit} className="create-form">
        {/* Schritt 1: Grunddaten */}
        {step === 1 && (
          <div className="form-section">
            <h3>Grunddaten</h3>
            
            <div className="form-group">
              <label>Vertragsnummer *</label>
              <input
                type="text"
                value={formData.contractNumber}
                onChange={(e) => handleChange('contractNumber', e.target.value)}
                placeholder="z.B. VS-2024-001"
                required
              />
            </div>

            <div className="form-row">
              <div className="form-group">
                <label>Status *</label>
                <select
                  value={formData.status}
                  onChange={(e) => handleChange('status', Number(e.target.value) as ContractStatus)}
                  required
                >
                  <option value="0">In Verhandlung</option>
                  <option value="1">Aktiv</option>
                  <option value="2">Gekündigt</option>
                  <option value="3">Beendet</option>
                  <option value="4">Ausgesetzt</option>
                </select>
              </div>

              <div className="form-group">
                <label>Typ *</label>
                <select
                  value={formData.type}
                  onChange={(e) => handleChange('type', Number(e.target.value) as ContractType)}
                  required
                >
                  <option value="0">Strom</option>
                  <option value="1">Gas</option>
                  <option value="2">Sonstige</option>
                </select>
              </div>
            </div>

            <div className="form-row">
              <div className="form-group">
                <label>Kunde *</label>
                <select
                  value={formData.customerId}
                  onChange={(e) => handleChange('customerId', e.target.value)}
                  required
                >
                  <option value="">Bitte wählen...</option>
                  {customers?.map((c) => (
                    <option key={c.id} value={c.id}>
                      {c.label}
                    </option>
                  ))}
                </select>
              </div>

              <div className="form-group">
                <label>Mandant *</label>
                <select
                  value={formData.mandantId}
                  onChange={(e) => handleChange('mandantId', e.target.value)}
                  required
                >
                  <option value="">Bitte wählen...</option>
                  {mandants?.map((m) => (
                    <option key={m.id} value={m.id}>
                      {m.label}
                    </option>
                  ))}
                </select>
              </div>
            </div>

            <div className="form-row">
              <div className="form-group">
                <label>Vertragsgruppe</label>
                <select
                  value={formData.contractGroupId || ''}
                  onChange={(e) => handleChange('contractGroupId', e.target.value || undefined)}
                >
                  <option value="">Keine</option>
                  {contractGroups?.map((g) => (
                    <option key={g.id} value={g.id}>
                      {g.label}
                    </option>
                  ))}
                </select>
              </div>

              <div className="form-group">
                <label>Währung *</label>
                <select
                  value={formData.currencyId}
                  onChange={(e) => handleChange('currencyId', e.target.value)}
                  required
                >
                  {currencies?.map((c) => (
                    <option key={c.id} value={c.id}>
                      {c.label}
                    </option>
                  ))}
                </select>
              </div>
            </div>

            <div className="form-group">
              <label>Beschreibung</label>
              <textarea
                value={formData.description}
                onChange={(e) => handleChange('description', e.target.value)}
                placeholder="Kurze Beschreibung des Vertrags"
                rows={3}
              />
            </div>

            <div className="form-actions">
              <button type="button" onClick={() => navigate('/contracts')} className="btn">
                Abbrechen
              </button>
              <button
                type="button"
                onClick={() => setStep(2)}
                className="btn btn-primary"
                disabled={!isStep1Valid()}
              >
                Weiter →
              </button>
            </div>
          </div>
        )}

        {/* Schritt 2: Laufzeit */}
        {step === 2 && (
          <div className="form-section">
            <h3>Laufzeit</h3>

            <div className="form-group">
              <label>Startdatum *</label>
              <input
                type="date"
                value={formData.startDate}
                onChange={(e) => handleChange('startDate', e.target.value)}
                required
              />
            </div>

            <div className="form-group">
              <label>
                <input
                  type="checkbox"
                  checked={formData.isUnlimited}
                  onChange={(e) => handleChange('isUnlimited', e.target.checked)}
                />
                {' '}Unbefristeter Vertrag
              </label>
            </div>

            {!formData.isUnlimited && (
              <div className="form-group">
                <label>Enddatum *</label>
                <input
                  type="date"
                  value={formData.endDate || ''}
                  onChange={(e) => handleChange('endDate', e.target.value || undefined)}
                  required={!formData.isUnlimited}
                />
              </div>
            )}

            <div className="form-group">
              <label>Kündigungsfrist (Monate) *</label>
              <input
                type="number"
                value={formData.terminationNoticePeriod}
                onChange={(e) => handleChange('terminationNoticePeriod', Number(e.target.value))}
                min="0"
                required
              />
            </div>

            <div className="form-actions">
              <button type="button" onClick={() => setStep(1)} className="btn">
                ← Zurück
              </button>
              <button
                type="button"
                onClick={() => setStep(3)}
                className="btn btn-primary"
                disabled={!isStep2Valid()}
              >
                Weiter →
              </button>
            </div>
          </div>
        )}

        {/* Schritt 3: Details */}
        {step === 3 && (
          <div className="form-section">
            <h3>Details & Verantwortliche</h3>

            <div className="form-group">
              <label>Abrechnungszyklus</label>
              <input
                type="text"
                value={formData.billingCycle}
                onChange={(e) => handleChange('billingCycle', e.target.value)}
                placeholder="z.B. Monatlich, Quartalsweise"
              />
            </div>

            <div className="form-group">
              <label>SAP Vertragskonto</label>
              <input
                type="text"
                value={formData.sapContractAccount}
                onChange={(e) => handleChange('sapContractAccount', e.target.value)}
                placeholder="SAP Konto-Nummer"
              />
            </div>

            <div className="form-group">
              <label>Budgetverantwortlicher</label>
              <input
                type="text"
                value={formData.budgetResponsible}
                onChange={(e) => handleChange('budgetResponsible', e.target.value)}
                placeholder="Name des Budgetverantwortlichen"
              />
            </div>

            <div className="form-group">
              <label>Vertragsverantwortlicher</label>
              <input
                type="text"
                value={formData.responsiblePerson}
                onChange={(e) => handleChange('responsiblePerson', e.target.value)}
                placeholder="Name des Vertragsverantwortlichen"
              />
            </div>

            <div className="form-group">
              <label>Ansprechpartner</label>
              <input
                type="text"
                value={formData.contactPerson}
                onChange={(e) => handleChange('contactPerson', e.target.value)}
                placeholder="Name des Ansprechpartners"
              />
            </div>

            <div className="form-group">
              <label>Bemerkungen</label>
              <textarea
                value={formData.notes}
                onChange={(e) => handleChange('notes', e.target.value)}
                placeholder="Interne Notizen zum Vertrag"
                rows={5}
              />
            </div>

            {createMutation.isError && (
              <div className="error-message">
                Fehler beim Erstellen: {(createMutation.error as Error).message}
              </div>
            )}

            <div className="form-actions">
              <button type="button" onClick={() => setStep(2)} className="btn">
                ← Zurück
              </button>
              <button
                type="submit"
                className="btn btn-primary"
                disabled={createMutation.isPending}
              >
                {createMutation.isPending ? 'Erstelle...' : '✓ Vertrag erstellen'}
              </button>
            </div>
          </div>
        )}
      </form>
    </div>
  )
}

export default ContractCreate
