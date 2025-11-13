import { apiClient } from './api.service';
import type {
  ContractDto,
  CreateContractDto,
  UpdateContractDto,
  ContractStatus,
} from '../types/contract.types';

export interface GetContractsParams {
  search?: string;
  status?: ContractStatus;
  page?: number;
  pageSize?: number;
}

class ContractService {
  private readonly baseUrl = '/api/contracts';

  async getContracts(params?: GetContractsParams) {
    const response = await apiClient.get<ContractDto[]>(this.baseUrl, params);
    
    // Extract pagination headers
    const totalCount = response.headers['x-total-count'];
    const page = response.headers['x-page'];
    const pageSize = response.headers['x-page-size'];

    return {
      data: response.data,
      totalCount: totalCount ? parseInt(totalCount) : response.data.length,
      page: page ? parseInt(page) : 1,
      pageSize: pageSize ? parseInt(pageSize) : 25,
    };
  }

  async getContract(id: string) {
    const response = await apiClient.get<ContractDto>(`${this.baseUrl}/${id}`);
    return response.data;
  }

  async createContract(data: CreateContractDto) {
    const response = await apiClient.post<ContractDto>(this.baseUrl, data);
    return response.data;
  }

  async updateContract(id: string, data: UpdateContractDto) {
    const response = await apiClient.put<ContractDto>(`${this.baseUrl}/${id}`, data);
    return response.data;
  }

  async deleteContract(id: string) {
    await apiClient.delete(`${this.baseUrl}/${id}`);
  }
}

export const contractService = new ContractService();
