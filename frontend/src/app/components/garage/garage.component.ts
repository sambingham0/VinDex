import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { SavedVehicleDto, VehicleService } from '../../services/vehicle.service';
import { VehicleDetailsComponent } from '../vehicle-details/vehicle-details.component';

@Component({
  selector: 'app-garage',
  standalone: true,
  imports: [CommonModule, VehicleDetailsComponent],
  templateUrl: './garage.component.html',
  styleUrl: './garage.component.css'
})
export class GarageComponent implements OnInit {
  loading = true;
  error = '';
  savedVehicles: SavedVehicleDto[] = [];

  constructor(
    private readonly authService: AuthService,
    private readonly router: Router,
    private readonly vehicleService: VehicleService
  ) {}

  ngOnInit(): void {
    this.loadGarage();
  }

  goToSearch(vehicleVin?: string, recalls?: boolean): void {
    this.router.navigate(['/search'],
      { state: { vin: vehicleVin, recalls: recalls } }
    );
  }

  logout(): void {
    this.authService.clearToken();
    this.router.navigate(['/login']);
  }

  private loadGarage(): void {
    this.loading = true;
    this.error = '';

    this.vehicleService.getGarageVehicles().subscribe({
      next: (vehicles) => {
        this.savedVehicles = vehicles;
        this.loading = false;
      },
      error: () => {
        this.error = 'Could not load your garage.';
        this.loading = false;
      }
    });
  }

  deleteVehicle(vin: string): void {
    this.vehicleService.deleteVehicle(vin).subscribe({
      next: () => {
        this.savedVehicles = this.savedVehicles.filter(v => v.vehicle.vin !== vin);
      },
      error: () => {
        this.error = 'Could not delete the vehicle.';
      }
    });
  }

  goToMaintenance(vin: string): void {
    this.router.navigate(['/maintenance', vin]);
  }
}