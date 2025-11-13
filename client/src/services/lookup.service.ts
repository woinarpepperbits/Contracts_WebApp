import { apiClient } from './api.service';
import {
  LookupItem,
  CustomerLookup,
  ContractStatusLookup,
  ContractTypeLookup,
} from '../types/contract.types';

class LookupService {
  private readonly baseUrl = '/api/lookups';

  async getCustomers() {
    const response = await apiClient.get<CustomerLookup[]>(`${this.baseUrl}/customers`);
    return response.data;
  }

  async getMandants() {
    const response = await apiClient.get<LookupItem[]>(`${this.baseUrl}/mandants`);
    return response.data;
  }

  async getContractGroups() {
    const response = await apiClient.get<LookupItem[]>(`${this.baseUrl}/contract-groups`);
    return response.data;
  }

  async getCurrencies() {
    const response = await apiClient.get<LookupItem[]>(`${this.baseUrl}/currencies`);
    return response.data;
  }

  async getPriceTypes() {
    const response = await apiClient.get<LookupItem[]>(`${this.baseUrl}/price-types`);
    return response.data;
  }

  async getContractStatuses() {
    const response = await apiClient.get<ContractStatusLookup[]>(`${this.baseUrl}/contract-statuses`);
    return response.data;
  }

  async getContractTypes() {
    const response = await apiClient.get<ContractTypeLookup[]>(`${this.baseUrl}/contract-types`);
    return response.data;
  }
}

export const lookupService = new LookupService();
