import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { VehicleService, VehicleWithRecalls } from '../../services/vehicle.service';
import { VehicleInfoDto } from '../../models/vehicle-info.model';
import { RecallDto } from '../../models/recall.model';

@Component({
  selector: 'app-vehicle-search',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './vehicle-search.component.html',
  styleUrl: './vehicle-search.component.css'
})
export class VehicleSearchComponent {
  vin = '';
  withRecalls = false;
  loading = false;
  error = '';
  vehicle?: VehicleInfoDto;
  recalls: RecallDto[] = [];

  constructor(private vehicleService: VehicleService) {}

  search(): void {
    const vin = this.vin.trim().toUpperCase();
    if (!vin) {
      this.error = 'Please enter a VIN.';
      return;
    }

    this.error = '';
    this.loading = true;
    this.vehicle = undefined;
    this.recalls = [];

    this.vehicleService.getVehicleInfo(vin, this.withRecalls).subscribe({
      next: (res) => {
        if (isVehicleWithRecalls(res)) {
          this.vehicle = res.vehicle;
          this.recalls = res.recalls ?? [];
        } else {
          this.vehicle = res;
        }
        this.loading = false;
      },
      error: () => {
        this.error = 'Could not find that VIN.';
        this.loading = false;
      }
    });
  }
}

function isVehicleWithRecalls(value: VehicleInfoDto | VehicleWithRecalls): value is VehicleWithRecalls {
  return (value as VehicleWithRecalls).vehicle !== undefined;
}
