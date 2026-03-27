import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { MaintenanceRecord } from '../models/maintenance-record.model';
import { environment } from '../../environments/environment';

@Injectable({ providedIn: 'root' })
export class MaintenanceService {
  private readonly apiUrl = environment.apiUrl + '/maintenance';

  constructor(private http: HttpClient) {}

  getByVin(vin: string): Observable<MaintenanceRecord[]> {
    return this.http.get<MaintenanceRecord[]>(`${this.apiUrl}/${vin}`);
  }

  add(record: MaintenanceRecord): Observable<MaintenanceRecord> {
    return this.http.post<MaintenanceRecord>(this.apiUrl, record);
  }

  update(record: MaintenanceRecord): Observable<MaintenanceRecord> {
    return this.http.put<MaintenanceRecord>(`${this.apiUrl}/${record.id}`, record);
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
