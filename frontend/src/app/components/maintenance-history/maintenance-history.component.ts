import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { MaintenanceService } from '../../services/maintenance.service';
import { MaintenanceRecord } from '../../models/maintenance-record.model';
import { VehicleInfoDto } from '../../models/vehicle-info.model';
import { VehicleService } from '../../services/vehicle.service';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-maintenance-history',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './maintenance-history.component.html',
  styleUrl: './maintenance-history.component.css'
})
export class MaintenanceHistoryComponent implements OnInit {
  vin = '';
  vehicle?: VehicleInfoDto;
  records: MaintenanceRecord[] = [];
  loading = true;
  error = '';
  showForm = false;
  form: Partial<MaintenanceRecord> = { date: '', type: '', mileage: 0, notes: '' };
  formError = '';
  editingId: number | null = null;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private maintenanceService: MaintenanceService,
    private vehicleService: VehicleService
  ) {}

  goToGarage(): void {
    this.router.navigate(['/garage']);
  }

  logout(): void {
    this.router.navigate(['/login']);
  }

  ngOnInit(): void {
    this.vin = this.route.snapshot.paramMap.get('vin') || '';
    this.vehicleService.getVehicleInfo(this.vin).subscribe({
      next: (vehicle: any) => {
        this.vehicle = (vehicle && vehicle.vehicle) ? vehicle.vehicle : vehicle;
      },
      error: () => {
        this.vehicle = undefined;
      }
    });
    this.fetchRecords();
  }

  fetchRecords(): void {
    this.loading = true;
    this.maintenanceService.getByVin(this.vin).subscribe({
      next: (records) => {
        this.records = records;
        this.loading = false;
      },
      error: () => {
        this.error = 'Could not load maintenance records.';
        this.loading = false;
      }
    });
  }

  openForm(record?: MaintenanceRecord): void {
    this.showForm = true;
    if (record) {
      this.form = { ...record };
      this.editingId = record.id;
    } else {
      this.form = { date: '', type: '', mileage: 0, notes: '' };
      this.editingId = null;
    }
    this.formError = '';
  }

  closeForm(): void {
    this.showForm = false;
    this.formError = '';
    this.editingId = null;
  }

  submitForm(): void {
    if (!this.form.type || !this.form.date || !this.form.mileage) {
      this.formError = 'Type, date, and mileage are required.';
      return;
    }
    const record: MaintenanceRecord = {
      id: this.editingId ?? 0,
      vehicleVin: this.vin,
      type: this.form.type!,
      date: this.form.date!,
      mileage: this.form.mileage!,
      notes: this.form.notes || ''
    };
    const obs = this.editingId
      ? this.maintenanceService.update(record)
      : this.maintenanceService.add(record);
    obs.subscribe({
      next: () => {
        this.fetchRecords();
        this.closeForm();
      },
      error: () => {
        this.formError = 'Could not save record.';
      }
    });
  }

  editRecord(record: MaintenanceRecord): void {
    this.openForm(record);
  }

  deleteRecord(id: number): void {
    if (!confirm('Delete this maintenance record?')) return;
    this.maintenanceService.delete(id).subscribe({
      next: () => this.fetchRecords(),
      error: () => alert('Could not delete record.')
    });
  }
}
