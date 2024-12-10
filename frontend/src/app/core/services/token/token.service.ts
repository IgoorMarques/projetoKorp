import { Injectable, Inject, PLATFORM_ID } from '@angular/core';
import { isPlatformBrowser } from '@angular/common';

const KEY = 'authToken';

@Injectable({
  providedIn: 'root',
})
export class TokenService {
  constructor(@Inject(PLATFORM_ID) private platformId: any) {}

  hasToken(): boolean {
    return !!this.getToken();
  }

  setToken(token: any): void {
    if (isPlatformBrowser(this.platformId)) {
      window.localStorage.setItem(KEY, token);
    }
  }

  getToken(): string | null {
    if (isPlatformBrowser(this.platformId)) {
      return window.localStorage.getItem(KEY);
    }
    return null;
  }

  removeToken(): void {
    if (isPlatformBrowser(this.platformId)) {
      window.localStorage.removeItem(KEY);
    }
  }
}
