import { Routes } from '@angular/router';
import { VehicleSearchComponent } from './components/vehicle-search/vehicle-search.component';
import { LoginComponent } from './components/login/login.component';
import { authGuard } from './guards/auth.guard';

export const routes: Routes = [
	{ path: '', pathMatch: 'full', redirectTo: 'search' },
	{ path: 'login', component: LoginComponent },
	{ path: 'search', component: VehicleSearchComponent, canActivate: [authGuard] },
	{ path: '**', redirectTo: 'search' }
];
