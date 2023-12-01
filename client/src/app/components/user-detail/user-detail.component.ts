import { Component } from '@angular/core';
import { FitFormUser } from '../../models/fit-form-user.model';
import { ActivatedRoute } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatListModule } from '@angular/material/list';
import { MatChipsModule } from '@angular/material/chips';

@Component({
  selector: 'app-user-detail',
  standalone: true,
  imports: [MatCardModule, MatListModule, MatChipsModule],
  templateUrl: './user-detail.component.html',
  styleUrl: './user-detail.component.scss'
})
export class UserDetailComponent {
  userId!: string;
  userDetails!: FitFormUser;
  constructor(private activatedRoute: ActivatedRoute,) { }

  ngOnInit() {
    this.activatedRoute.params.subscribe(val => {
      this.userId = val['id'];
      this.fetchUserDetails(this.userId);
    })
  }

  fetchUserDetails(userId: string) {
    // this.fitnessForimService.getRegisteredUserId(userId)
    //   .subscribe({
    //     next: (res) => {
    //       this.userDetails = res;
    //     },
    //     error: (err) => {
    //       console.log(err);
    //     }
    //   })
  }
}
