import { Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { CreateFormRegistrationComponent } from './components/create-form-registration/create-form-registration.component';
import { RegistrationFormListComponent } from './components/registration-form-list/registration-form-list.component';
import { RegisterComponent } from './components/account/register/register.component';
import { LoginComponent } from './components/account/login/login.component';
import { NotFoundComponent } from './components/not-found/not-found.component';

export const routes: Routes = [
    { path: '', pathMatch: 'full', redirectTo: 'home' },
    { path: 'home', component: HomeComponent },
    { path: 'account/register', component: RegisterComponent },
    { path: 'account/login', component: LoginComponent },
    { path: 'register-form', component: CreateFormRegistrationComponent },
    { path: 'list-form', component: RegistrationFormListComponent },
    { path: '**', component: NotFoundComponent }
];
