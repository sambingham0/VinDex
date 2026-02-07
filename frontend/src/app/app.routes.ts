import { Routes } from '@angular/router';
import { VehicleSearchComponent } from './components/vehicle-search/vehicle-search.component';

export const routes: Routes = [
	{ path: '', pathMatch: 'full', redirectTo: 'search' },
	{ path: 'search', component: VehicleSearchComponent },
	{ path: '**', redirectTo: 'search' }
];
