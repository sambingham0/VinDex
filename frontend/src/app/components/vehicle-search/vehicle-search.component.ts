import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { VehicleDetailsComponent } from '../vehicle-details/vehicle-details.component';
import { VehicleService, VehicleWithRecalls } from '../../services/vehicle.service';
import { VehicleInfoDto } from '../../models/vehicle-info.model';
import { RecallDto } from '../../models/recall.model';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-vehicle-search',
  standalone: true,
  imports: [CommonModule, FormsModule, VehicleDetailsComponent],
  templateUrl: './vehicle-search.component.html',
  styleUrl: './vehicle-search.component.css'
})
export class VehicleSearchComponent {
  private readonly authService = inject(AuthService);
  private readonly router = inject(Router);

  vin = '';
  withRecalls = false;
  loading = false;
  saving = false;
  vehicleInfoVisible = false;
  error = '';
  saveMessage = '';
  saveError = '';
  vehicleSaved = false;
  vehicle?: VehicleInfoDto;
  recalls: RecallDto[] = [];

  constructor(private vehicleService: VehicleService) {}

  logout(): void {
    this.authService.clearToken();
    this.router.navigate(['/login']);
  }

  openGarage(): void {
    this.router.navigate(['/garage']);
  }

  search(): void {
    let vin = this.vin.trim().toUpperCase();
    
    // Basic VIN Validation: 17 chars, alphanumeric, no I, O, Q
    const vinRegex = /^[A-HJ-NPR-Z0-9]{17}$/;
    
    if (!vin) {
      this.error = 'Please enter a VIN.';
      return;
    }

    if (!vinRegex.test(vin)) {
      this.error = 'Invalid VIN format. Must be 17 alphanumeric characters.';
      return;
    }

    this.error = '';
    this.loading = true;
    this.saving = false;
    this.saveMessage = '';
    this.saveError = '';
    this.vehicleSaved = false;
    this.vehicleInfoVisible = false;
    this.vehicle = undefined;
    this.recalls = [];

    this.vehicleService.getVehicleInfo(vin, this.withRecalls).subscribe({
      next: (res) => {
        setTimeout(() => {
          if (isVehicleWithRecalls(res)) {
            this.vehicle = res.vehicle;
            this.recalls = res.recalls ?? [];
          } else {
            this.vehicle = res;
          }
          this.loading = false;

          setTimeout(() => {
            this.vehicleInfoVisible = true;
          }, 100); // slight delay to trigger animation
        }, 1200); // load delay for wheel animation
      },
      error: () => {
        this.error = 'Could not find that VIN.';
        this.loading = false;
      }
    });
  }

  saveVehicle(): void {
    if (!this.vehicle || this.saving) {
      return;
    }

    this.saving = true;
    this.saveMessage = '';
    this.saveError = '';

    this.vehicleService.saveVehicle(this.vehicle.vin).subscribe({
      next: (response) => {
        this.vehicleSaved = true;
        this.saveMessage = response.alreadySaved
          ? 'This vehicle is already in your garage.'
          : 'Vehicle saved to your garage.';
        this.saving = false;
      },
      error: () => {
        this.saveError = 'Could not save this vehicle right now.';
        this.saving = false;
      }
    });
  }
}

function isVehicleWithRecalls(value: VehicleInfoDto | VehicleWithRecalls): value is VehicleWithRecalls {
  return (value as VehicleWithRecalls).vehicle !== undefined;
}
