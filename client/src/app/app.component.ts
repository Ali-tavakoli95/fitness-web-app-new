import { Component, OnInit, PLATFORM_ID, inject } from '@angular/core';
import { CommonModule, isPlatformBrowser } from '@angular/common';
import { RouterOutlet } from '@angular/router';
import { HeaderComponent } from './components/header/header.component';
import { NgToastModule } from 'ng-angular-popup';
import { NgConfirmModule } from 'ng-confirm-box';
import { AccountService } from './services/account.service';
import { User } from './models/user.model';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, RouterOutlet, HeaderComponent, NgToastModule, NgConfirmModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent implements OnInit {
  accountService = inject(AccountService);
  platformId = inject(PLATFORM_ID); // used to test if we are in the client(browser) or server. We must be on the client to access localStorage!

  allUsers: User[] | undefined;

  ngOnInit(): void {
    console.log('PlatformId in OnInit:', this.platformId);
    this.getLocalStorageCurrentValues();
  }

  getLocalStorageCurrentValues(): void {
    let userString: string | null = null;

    // if (isPlatformServer(this.platformId))
    if (isPlatformBrowser(this.platformId)) { // this code is ran on the browser now
      console.log('PlatformId in method:', this.platformId);
      userString = localStorage.getItem('user');
    }

    if (userString) {
      const user: User = JSON.parse(userString); // convert string to JSON before sending to method

      this.accountService.setCurrentUser(user);
    }
  }
}
