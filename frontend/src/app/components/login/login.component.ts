import { AfterViewInit, Component, OnInit, NgZone } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { environment } from '../../../environments/environment';

declare global {
  interface Window {
    google?: {
      accounts: {
        id: {
          initialize: (options: {
            client_id: string;
            callback: (response: { credential: string }) => void;
          }) => void;
          renderButton: (element: HTMLElement, options: Record<string, unknown>) => void;
        };
      };
    };
  }
}

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent implements OnInit, AfterViewInit {
  loading = false;
  error = '';

  constructor(
    private authService: AuthService,
    private router: Router,
    private ngZone: NgZone
  ) {}

  ngOnInit(): void {
    if (this.authService.isAuthenticated()) {
      this.router.navigate(['/search']);
    }
  }

  async ngAfterViewInit(): Promise<void> {
    if (!environment.googleClientId) {
      this.error = 'Google client ID is not configured in environment.ts.';
      return;
    }

    try {
      await this.ensureGoogleScript();
      this.initializeGoogleButton();
    } catch {
      this.error = 'Google Sign-In script failed to load.';
    }
  }

  private ensureGoogleScript(): Promise<void> {
    if (window.google?.accounts?.id) {
      return Promise.resolve();
    }

    return new Promise((resolve, reject) => {
      const existingScript = document.getElementById('google-gsi-script') as HTMLScriptElement | null;
      if (existingScript) {
        existingScript.addEventListener('load', () => resolve(), { once: true });
        existingScript.addEventListener('error', () => reject(new Error('load failed')), { once: true });
        return;
      }

      const script = document.createElement('script');
      script.id = 'google-gsi-script';
      script.src = 'https://accounts.google.com/gsi/client';
      script.async = true;
      script.defer = true;
      script.onload = () => resolve();
      script.onerror = () => reject(new Error('load failed'));
      document.head.appendChild(script);
    });
  }

  private initializeGoogleButton(): void {
    const buttonContainer = document.getElementById('google-signin-button');
    if (!buttonContainer || !window.google?.accounts?.id) {
      this.error = 'Google Sign-In is unavailable.';
      return;
    }

    window.google.accounts.id.initialize({
      client_id: environment.googleClientId,
      callback: (response: { credential: string }) => this.handleCredential(response.credential)
    });

    buttonContainer.innerHTML = '';
    window.google.accounts.id.renderButton(buttonContainer, {
      theme: 'outline',
      size: 'large',
      text: 'signin_with',
      shape: 'pill'
    });
  }

  private handleCredential(credential: string): void {
    this.loading = true;
    this.error = '';

    this.authService.loginWithGoogleCredential(credential).subscribe({
      next: () => {
        this.loading = false;
        this.ngZone.run(() => this.router.navigate(['/search']));
      },
      error: () => {
        this.loading = false;
        this.error = 'Login failed. Please try again.';
      }
    });
  }
}
