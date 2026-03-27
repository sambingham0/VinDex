import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { VehicleInfoDto } from '../models/vehicle-info.model';
import { RecallDto } from '../models/recall.model';
import { environment } from '../../environments/environment';

export interface VehicleWithRecalls {
  vehicle: VehicleInfoDto;
  recalls: RecallDto[];
}

export interface SavedVehicleDto {
  savedAt: string;
  vehicle: VehicleInfoDto;
}

export interface SaveVehicleResponse {
  alreadySaved: boolean;
  vehicle: VehicleInfoDto;
}

@Injectable({
  providedIn: 'root'
})
export class VehicleService {

  private apiBaseUrl = environment.apiUrl + '/vin';
  private garageApiBaseUrl = environment.garageApiUrl;

  constructor(private http: HttpClient) { }

  // get vehicle info by VIN, with option to include recalls
  getVehicleInfo(vin: string, withRecalls = false): Observable<VehicleInfoDto | VehicleWithRecalls> {
    const suffix = withRecalls ? '?WRecalls=true' : '';
    return this.http.get<VehicleInfoDto | VehicleWithRecalls>(`${this.apiBaseUrl}/${vin}${suffix}`);
  }

  getGarageVehicles(): Observable<SavedVehicleDto[]> {
    return this.http.get<SavedVehicleDto[]>(this.garageApiBaseUrl);
  }

  saveVehicle(vin: string): Observable<SaveVehicleResponse> {
    return this.http.post<SaveVehicleResponse>(this.garageApiBaseUrl, { vin });
  }

  deleteVehicle(vin: string): Observable<void> {
    return this.http.delete<void>(`${this.garageApiBaseUrl}/${vin}`);
  }
}
