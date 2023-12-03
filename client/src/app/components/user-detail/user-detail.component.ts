import { Component, inject } from '@angular/core';
import { FitFormUser } from '../../models/fit-form-user.model';
import { ActivatedRoute } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatListModule } from '@angular/material/list';
import { MatChipsModule } from '@angular/material/chips';
import { ListFormService } from '../../services/list-form.service';
import { MatButtonModule } from '@angular/material/button';
// import { RouterModule } from '@angular/router';


@Component({
  selector: 'app-user-detail',
  standalone: true,
  imports: [ MatCardModule, MatListModule, MatChipsModule, MatButtonModule],
  templateUrl: './user-detail.component.html',
  styleUrl: './user-detail.component.scss'
})
export class UserDetailComponent {
  listFormService = inject(ListFormService)
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
    this.listFormService.getRegisteredUserId(userId)
      .subscribe({
        next: (res) => {
          this.userDetails = res;
        },
        error: (err) => {
          console.log(err);
        }
      })
  }
}
