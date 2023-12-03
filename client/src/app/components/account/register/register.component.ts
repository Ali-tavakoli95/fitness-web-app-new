import { Component, inject } from '@angular/core';
import { FormBuilder, FormControl, ReactiveFormsModule, Validators } from '@angular/forms';
import { RegisterUser } from '../../../models/register-user.model';
import { CommonModule } from '@angular/common';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { AccountService } from '../../../services/account.service';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [CommonModule, MatFormFieldModule, ReactiveFormsModule, MatInputModule, MatButtonModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss'
})
export class RegisterComponent {
  accountService = inject(AccountService);
  fb = inject(FormBuilder);

  passowrdsNotMatch: boolean | undefined;
  apiErrorMessage: string | undefined;

  registerFg = this.fb.group({
    emailCtrl: ['', [Validators.required, Validators.pattern(/^([\w\.\-]+)@([\w\-]+)((\.(\w){2,5})+)$/)]], // Use / instead of ' around RegEx
    passwordCtrl: ['', [Validators.required, Validators.minLength(7), Validators.maxLength(20)]],
    confirmPasswordCtrl: ['', [Validators.required, Validators.minLength(7), Validators.maxLength(20)]],
    ageCtrl: ['',[ Validators]],
    genderCtrl: ['', [Validators]],
    countryCtrl: ['', [Validators]],
    cityCtrl: ['', [Validators]]
  })

  get EmailCtrl(): FormControl {
    return this.registerFg.get('emailCtrl') as FormControl;
  }

  get PasswordCtrl(): FormControl {
    return this.registerFg.get('passwordCtrl') as FormControl;
  }

  get ConfirmPasswordCtrl(): FormControl {
    return this.registerFg.get('confirmPasswordCtrl') as FormControl;
  }

  get AgeCtrl(): FormControl {
    return this.registerFg.get('ageCtrl') as FormControl;
  }

  get GenderCtrl(): FormControl {
    return this.registerFg.get('genderCtrl') as FormControl;
  }

  get CountryCtrl(): FormControl {
    return this.registerFg.get('countryCtrl') as FormControl;
  }

  get CityCtrl(): FormControl {
    return this.registerFg.get('cityCtrl') as FormControl;
  }

  register(): void {
    this.apiErrorMessage = undefined;

    if (this.PasswordCtrl.value === this.ConfirmPasswordCtrl.value) {
      this.passowrdsNotMatch = false;

      let user: RegisterUser = {
        email: this.EmailCtrl.value,
        password: this.PasswordCtrl.value,
        confirmPassword: this.ConfirmPasswordCtrl.value,
        age: this.AgeCtrl.value,
        gender: this.GenderCtrl.value,
        country: this.CountryCtrl.value,
        city: this.CityCtrl.value
      }


      this.accountService.registerUser(user).subscribe({
        next: user => console.log(user),
        error: err => this.apiErrorMessage = err.error
      })
    }
    else {
      this.passowrdsNotMatch = true;
    }
  }

  getState(): void {
    console.log(this.registerFg);
  }


}

