import { Component, inject } from '@angular/core';
import jsPDF from 'jspdf';
import html2canvas from 'html2canvas';
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

  ngOnInit() {
    // Check if we have state from garage navigation
    const vinFromState = history.state?.vin;
    const recallsFromState = history.state?.recalls;
    if (vinFromState) {
      this.vin = vinFromState;
      this.withRecalls = recallsFromState ?? false;
      this.search();
    }
  }

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

  async downloadPdf(): Promise<void> {
    // Find the vehicle info section
    const vehicleInfoElement = document.querySelector('.vehicle-info');
    if (!vehicleInfoElement) {
      return;
    }
    // Hide the 'Save to Garage' button before rendering
    const saveBtn = vehicleInfoElement.querySelector('.save-button') as HTMLElement;
    let originalDisplay = '';
    if (saveBtn) {
      originalDisplay = saveBtn.style.display;
      saveBtn.style.display = 'none';
    }
    // Use html2canvas to render the element
    const canvas = await html2canvas(vehicleInfoElement as HTMLElement);
    // Restore the button's display property
    if (saveBtn) {
      saveBtn.style.display = originalDisplay;
    }
    const imgData = canvas.toDataURL('image/png');
    const pdf = new jsPDF({ orientation: 'portrait', unit: 'pt', format: 'a4' });
    // Calculate width/height for A4
    const pageWidth = pdf.internal.pageSize.getWidth();
    const pageHeight = pdf.internal.pageSize.getHeight();
    // Fit image to width, keep aspect ratio
    const imgWidth = pageWidth - 40;
    const imgHeight = (canvas.height * imgWidth) / canvas.width;
    pdf.addImage(imgData, 'PNG', 20, 20, imgWidth, imgHeight);
    // Add recalls if present
    if (this.recalls && this.recalls.length) {
      let y = 40 + imgHeight;
      pdf.setFontSize(16);
      pdf.setTextColor(40, 40, 90);
      pdf.text('Recalls:', 20, y);
      y += 24;
      this.recalls.forEach((recall, idx) => {
        if (y > pageHeight - 80) {
          pdf.addPage();
          y = 40;
        }
        // Bold for recall title
        pdf.setFont('helvetica', 'bold');
        pdf.setFontSize(12);
        pdf.setTextColor(30, 30, 30);
        pdf.text(`${idx + 1}. ${recall.campaignNumber} — ${recall.component}`, 24, y);
        y += 16;
        // Regular font for summary
        if (recall.summary) {
          pdf.setFont('helvetica', 'normal');
          pdf.setFontSize(11);
          // Split summary into lines for wrapping
          const summaryLines = pdf.splitTextToSize(recall.summary, pageWidth - 60);
          summaryLines.forEach((line: string) => {
            pdf.text(line, 34, y);
            y += 13;
          });
        }
        // Italic for date
        if (recall.reportReceivedDate) {
          pdf.setFont('helvetica', 'italic');
          pdf.setFontSize(10);
          pdf.setTextColor(80, 80, 80);
          pdf.text(`Reported: ${new Date(recall.reportReceivedDate).toLocaleDateString()}`, 34, y);
          y += 13;
        }
        y += 10; // Extra space between recalls
      });
      // Reset font
      pdf.setFont('helvetica', 'normal');
      pdf.setFontSize(11);
      pdf.setTextColor(0, 0, 0);
    }
    pdf.save(`vehicle-info-${this.vehicle?.vin || 'download'}.pdf`);
  }
}

function isVehicleWithRecalls(value: VehicleInfoDto | VehicleWithRecalls): value is VehicleWithRecalls {
  return (value as VehicleWithRecalls).vehicle !== undefined;
}
