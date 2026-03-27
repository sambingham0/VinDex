export interface MaintenanceRecord {
  id: number;
  vehicleVin: string;
  type: string;
  date: string; // ISO string
  mileage: number;
  notes?: string;
}
