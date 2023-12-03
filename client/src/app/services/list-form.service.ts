import { Injectable, inject } from '@angular/core';
import { FitFormUser } from '../models/fit-form-user.model';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class ListFormService {
  http = inject(HttpClient);

  // private readonly baseApiUrl = environment.apiUrl + 'fitness/';

  getRegisteredUser() {
    return this.http.get<FitFormUser[]>("http://localhost:5000/api/fitness/get-all");
  }

  updateRegisterUser(registerObj: FitFormUser, id: string) {
    return this.http.put<FitFormUser>("http://localhost:5000/api/fitness/update/" + id, registerObj);
  }

  deleteRegistered(id: string) {
    return this.http.delete<FitFormUser>("http://localhost:5000/api/fitness/delete/" + id);
  }

  getRegisteredUserId(id: string) {
    return this.http.get<FitFormUser>("http://localhost:5000/api/fitness/get-fit-user-by-id/" + id);
  }
}
