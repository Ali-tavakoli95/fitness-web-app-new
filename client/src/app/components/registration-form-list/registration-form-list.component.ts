import { Component, OnInit, ViewChild } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { FitFormUser } from '../../models/fit-form-user.model';
import { MatSort } from '@angular/material/sort';
import { NgToastService } from 'ng-angular-popup';
import { NgConfirmService } from 'ng-confirm-box';
import { MatNativeDateModule } from '@angular/material/core';
import { CommonModule } from '@angular/common';





@Component({
  selector: 'app-registration-form-list',
  standalone: true,
  imports: [CommonModule, MatTableModule, RouterModule, MatFormFieldModule,
    MatButtonModule, MatInputModule, MatPaginatorModule,
    MatNativeDateModule, MatSortModule],
  templateUrl: './registration-form-list.component.html',
  styleUrl: './registration-form-list.component.scss'
})
export class RegistrationFormListComponent implements OnInit {
  public users!: FitFormUser[];
  dataSource!: MatTableDataSource<FitFormUser>;

  displayedColumns: string[] = ['firstName', 'lastName', 'email', 'mobile', 'bmiResult', 'gender', 'package', 'enquiryDate', 'action'];

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;
  constructor(private router: Router, private confirmService: NgConfirmService, private toastService: NgToastService) { }

  ngOnInit() {
    this.getUsers();
  }

  getUsers() {
    // this.apiService.getRegisteredUser()
    //   .subscribe({
    //     next: (res) => {
    //       this.users = res;
    //       this.dataSource = new MatTableDataSource(this.users);
    //       this.dataSource.paginator = this.paginator;
    //       this.dataSource.sort = this.sort;
    //     },
    //     error: (err) => {
    //       console.log(err);
    //     }
    //   })
  }

  edit(id: string) {
    this.router.navigate(['update', id])
  }

  deleteUser(id: string) {
    // this.confirmService.showConfirm("آیا مطمئن هستید که می خواهید حذف کنید؟",
    //   () => {
    //     //your logic if Yes clicked
    //     this.apiService.deleteRegistered(id)
    //       .subscribe({
    //         next: (res) => {
    //           this.toastService.success({ detail: 'موفقیت', summary: 'با موفقیت حذف شد', duration: 3000 });
    //           this.getUsers();
    //         },
    //         error: (err) => {
    //           this.toastService.error({ detail: 'خطا', summary: 'مشکلی پیش آمد!', duration: 3000 });
    //         }
    //       })
    //   },
    //   () => {
    //     //yor logic if No clicked
    //   })
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }
}
