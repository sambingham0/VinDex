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

@Injectable({
  providedIn: 'root'
})
export class VehicleService {

  private apiBaseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  // get vehicle info by VIN, with option to include recalls
  getVehicleInfo(vin: string, withRecalls = false): Observable<VehicleInfoDto | VehicleWithRecalls> {
    const suffix = withRecalls ? '?WRecalls=true' : '';
    return this.http.get<VehicleInfoDto | VehicleWithRecalls>(`${this.apiBaseUrl}/${vin}${suffix}`);
  }
}
