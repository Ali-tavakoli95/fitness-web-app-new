import { Injectable, inject } from '@angular/core';
import { FitFormUser } from '../models/fit-form-user.model';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class ListFormService {
  http = inject(HttpClient);

  private readonly baseApiUrl = environment.apiUrl + 'fitness/';

  getRegisteredUser() {
    return this.http.get<FitFormUser[]>(this.baseApiUrl + 'get-all');
  }

  updateRegisterUser(registerObj: FitFormUser, id: string) {
    return this.http.put<FitFormUser>(this.baseApiUrl + 'update/' + id, registerObj);
  }

  deleteRegistered(id: string) {
    return this.http.delete<FitFormUser>(this.baseApiUrl + 'delete/' + id);
  }

  getRegisteredUserId(id: string) {
    return this.http.get<FitFormUser>(this.baseApiUrl + 'get-fit-user-by-id/' + id);
  }
}
