import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { Observable, map } from 'rxjs';
import { User } from '../models/user.model';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  http = inject(HttpClient);

  private readonly baseApiUrl = environment.apiUrl + 'user/';

  getAllUsers(): Observable<User[] | null> {
    return this.http.get<User[]>(this.baseApiUrl).pipe(
      map((users: User[]) => {
        if (users)
          return users;

        return null;
      })
    )
  }

  getUserById(): Observable<User | null> {
    return this.http.get<User>(this.baseApiUrl + 'get-by-id').pipe(
      map((user: User | null) => {
        if (user)
          return user;

        return null;
      })
    )
  }
}
