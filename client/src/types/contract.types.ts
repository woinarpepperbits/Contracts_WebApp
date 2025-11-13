// API Types based on backend DTOs

export enum ContractStatus {
  InNegotiation = 0,
  Active = 1,
  Terminated = 2,
  Ended = 3,
  Suspended = 4,
}

export enum ContractType {
  Sale = 0,
  Supplier = 1,
  SalesOpportunity = 2,
}

export interface ContractDto {
  id: string;
  contractNumber: string;
  customerId: string;
  customerName: string;
  customerNumber: string;
  mandantId: string;
  mandantName: string;
  contractGroupId: string;
  contractGroupName: string;
  contractType: ContractType;
  contractTypeDisplay: string;
  status: ContractStatus;
  statusDisplay: string;
  startDate: string;
  endDate?: string | null;
  isUnlimited: boolean;
  noticePeriodMonths: number;
  noticeDeadline?: string | null;
  autoRenew: boolean;
  billingStartDate: string;
  responsibleSales: string;
  responsibleAccounting: string;
  responsiblePricing: string;
  currencyId: string;
  currencyCode: string;
  notes: string;
  createdAt: string;
  createdBy: string;
  updatedAt: string;
  updatedBy: string;
}

export interface CreateContractDto {
  contractNumber: string;
  customerId: string;
  mandantId: string;
  contractGroupId: string;
  contractType: ContractType;
  status: ContractStatus;
  startDate: string;
  endDate?: string | null;
  isUnlimited: boolean;
  noticePeriodMonths: number;
  noticeDeadline?: string | null;
  autoRenew: boolean;
  billingStartDate: string;
  responsibleSales: string;
  responsibleAccounting: string;
  responsiblePricing: string;
  currencyId: string;
  notes: string;
}

export interface UpdateContractDto extends CreateContractDto {}

// Lookup types
export interface LookupItem {
  id: string;
  code: string;
  name: string;
  display: string;
}

export interface CustomerLookup {
  id: string;
  customerNumber: string;
  name: string;
  display: string;
}

export interface ContractStatusLookup {
  value: number;
  display: string;
}

export interface ContractTypeLookup {
  value: number;
  display: string;
}

// API Response types
export interface ApiError {
  message: string;
  errors?: Record<string, string[]>;
}

export interface PaginatedResponse<T> {
  data: T[];
  totalCount: number;
  page: number;
  pageSize: number;
}
