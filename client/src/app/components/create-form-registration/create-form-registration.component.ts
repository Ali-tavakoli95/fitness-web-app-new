import { Component, Inject, OnInit } from '@angular/core';
import { FitFormUser } from '../../models/fit-form-user.model';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatIconModule } from '@angular/material/icon';
import { MatRadioModule } from '@angular/material/radio';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { NgToastModule, NgToastService } from 'ng-angular-popup';
import { NgConfirmModule } from 'ng-confirm-box';
import { CreateFormService } from '../../services/create-form.service';
import { ActivatedRoute, Router } from '@angular/router';
import { ListFormService } from '../../services/list-form.service';


@Component({
  selector: 'app-create-form-registration',
  standalone: true,
  imports: [MatFormFieldModule, MatInputModule, MatIconModule,
    MatRadioModule, MatButtonModule, MatSelectModule,
    MatDatepickerModule, MatNativeDateModule, ReactiveFormsModule,
    NgToastModule, NgConfirmModule],
  templateUrl: './create-form-registration.component.html',
  styleUrl: './create-form-registration.component.scss'
})
export class CreateFormRegistrationComponent implements OnInit {
  // private fb = Inject(FormBuilder);
  // private createFormService = Inject(CreateFormService);
  // private toastService = Inject(NgToastService);
  // selectedGender!: string;

  genders: string[] = ["مرد", "زن"];
  trainerOpts: string[] = ["آره", "نه"];
  packages: string[] = ["ماهانه", "سه ماه یکبار", "سالانه"];
  importantList: string[] = [
    "کاهش چربی سمی",
    "انرژی و استقامت",
    "ساخت عضلات لاغر",
    "سیستم گوارش سالم تر",
    "بدن هوس قند",
    "تناسب اندام"
  ];
  beenGyms: string[] = ["آره", "نه"];

  registerForm!: FormGroup;
  private userIdToUpdate!: string;
  public isUpdateActive: boolean = false;

  constructor(private fb: FormBuilder, private createFormService: CreateFormService, private listFormService: ListFormService, private toastService: NgToastService, private activatedRoute: ActivatedRoute, private router: Router) { }

  ngOnInit(): void {
    this.registerForm = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', Validators.required],
      mobile: ['', Validators.required],
      weight: ['', Validators.required],
      height: ['', Validators.required],
      bmi: ['', Validators.required],
      bmiResult: ['', Validators.required],
      gender: ['', Validators.required],
      requireTrainer: ['', Validators.required],
      package: ['', Validators.required],
      important: ['', Validators.required],
      haveGymBefore: ['', Validators.required],
      enquiryDate: ['', Validators.required]
    });

    this.registerForm.controls['height'].valueChanges.subscribe(res => {
      this.calculateBmi(res);
    });

    this.activatedRoute.params.subscribe(val => {
      this.userIdToUpdate = val['id'];
      this.listFormService.getRegisteredUserId(this.userIdToUpdate)
        .subscribe(res => {
          this.isUpdateActive = true;
          this.FillFormToUpdate(res);
        })
    })
  }

  submit() {
    this.createFormService.postRegistration(this.registerForm.value)
      .subscribe(res => {
        this.toastService.success({ detail: "موفقیت", summary: "فرم اضافه شد", duration: 3000 });
        this.registerForm.reset();
      })
  }

  update() {
    this.listFormService.updateRegisterUser(this.registerForm.value, this.userIdToUpdate)
      .subscribe(res => {
        this.toastService.success({ detail: "موفقیت", summary: "فرم به روز شد", duration: 3000 });
        this.registerForm.reset();
        this.router.navigate(['list-form'])
      })
  }

  calculateBmi(heightValue: number) {
    const weight = this.registerForm.value.weight;
    const height = heightValue;
    const bmi = weight / (height * height);
    this.registerForm.controls['bmi'].patchValue(bmi);
    switch (true) {
      case bmi < 18.5:
        this.registerForm.controls['bmiResult'].patchValue("کمبود وزن");
        break;
      case (bmi >= 18.5 && bmi < 25):
        this.registerForm.controls['bmiResult'].patchValue("طبیعی");
        break;
      case (bmi >= 25 && bmi < 30):
        this.registerForm.controls['bmiResult'].patchValue("اضافه وزن");
        break;

      default:
        this.registerForm.controls['bmiResult'].patchValue("چاق");
        break;
    }
  }


  FillFormToUpdate(user: FitFormUser) {
    this.registerForm.setValue({
      firstName: user.firstName,
      lastName: user.lastName,
      email: user.email,
      mobile: user.mobile,
      weight: user.weight,
      height: user.height,
      bmi: user.bmi,
      bmiResult: user.bmiResult,
      gender: user.gender,
      requireTrainer: user.requireTrainer,
      package: user.package,
      important: user.important,
      haveGymBefore: user.haveGymBefore,
      enquiryDate: user.enquiryDate
    })
  }
}
