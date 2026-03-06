import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, map, tap } from 'rxjs';
import { environment } from '../../environments/environment';

interface GoogleLoginResponse {
  token?: string;
  Token?: string;
}

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private readonly tokenStorageKey = 'vindex_auth_token';

  constructor(private http: HttpClient) {}

  loginWithGoogleCredential(credential: string): Observable<string> {
    return this.http.post<GoogleLoginResponse>(`${environment.authApiUrl}/google-login`, { credential }).pipe(
      map(response => response.token ?? response.Token ?? ''),
      tap(token => {
        if (!token) {
          throw new Error('API did not return a token.');
        }
        this.setToken(token);
      })
    );
  }

  setToken(token: string): void {
    localStorage.setItem(this.tokenStorageKey, token);
  }

  getToken(): string | null {
    return localStorage.getItem(this.tokenStorageKey);
  }

  clearToken(): void {
    localStorage.removeItem(this.tokenStorageKey);
  }

  isAuthenticated(): boolean {
    return !!this.getToken();
  }
}
