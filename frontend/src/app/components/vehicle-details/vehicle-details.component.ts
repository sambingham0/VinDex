import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { VehicleInfoDto } from '../../models/vehicle-info.model';

@Component({
  selector: 'app-vehicle-details',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './vehicle-details.component.html',
  styleUrl: './vehicle-details.component.css'
})
export class VehicleDetailsComponent {
  @Input({ required: true }) vehicle!: VehicleInfoDto;
}