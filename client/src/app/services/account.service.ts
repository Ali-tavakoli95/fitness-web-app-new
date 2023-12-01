import { HttpClient } from '@angular/common/http';
import { Injectable, PLATFORM_ID, inject } from '@angular/core';
import { Router } from '@angular/router';
import { environment } from '../../environments/environment.development';
import { BehaviorSubject, Observable, map } from 'rxjs';
import { User } from '../models/user.model';
import { RegisterUser } from '../models/register-user.model';
import { isPlatformBrowser } from '@angular/common';
import { LoginUser } from '../models/login-user.model';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  http = inject(HttpClient);
  router = inject(Router);
  platformId = inject(PLATFORM_ID);

  private readonly baseApiUrl = environment.apiUrl + 'account/';

  private currentUserSource = new BehaviorSubject<User | null>(null);
  currentUser$ = this.currentUserSource.asObservable();

  registerUser(userInput: RegisterUser): Observable<User | null> {
    return this.http.post<User>(this.baseApiUrl + 'register', userInput).pipe(
      map(userResponse => {
        if (userResponse) {
          this.setCurrentUser(userResponse);

          return userResponse;
        } // false

        return null;
      })
    );
  }

  loginUser(userInput: LoginUser): Observable<User | null> {
    return this.http.post<User>(this.baseApiUrl + 'login', userInput).pipe(
      map(userResponse => {
        if (userResponse) {
          this.setCurrentUser(userResponse);

          return userResponse;
        }

        return null;
      })
    );
  }

  setCurrentUser(user: User): void {
    this.currentUserSource.next(user);

    if (isPlatformBrowser(this.platformId)) // we make sure this code is ran on the browser and NOT server
      localStorage.setItem('user', JSON.stringify(user));

    this.router.navigateByUrl('');
  }

  logoutUser(): void {
    this.currentUserSource.next(null);

    if (isPlatformBrowser(this.platformId))
      localStorage.removeItem('user');

    this.router.navigateByUrl('account/login');
  }
}
