import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { FitFormUser } from '../models/fit-form-user.model';
import { environment } from '../../environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class CreateFormService {

  constructor(private http: HttpClient){}
  
  private readonly baseApiUrl = environment.apiUrl + 'fitnessForm/';

  postRegistration(registerObj: FitFormUser) {
    return this.http.post<FitFormUser>(this.baseApiUrl + 'register', registerObj);
  }
}
