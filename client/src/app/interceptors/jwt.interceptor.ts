import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { take } from 'rxjs';
import { User } from '../models/user.model';
import { AccountService } from '../services/account.service';

export const jwtInterceptor: HttpInterceptorFn = (req, next) => {
  inject(AccountService).currentUser$.pipe(take(1)).subscribe({
    next: (currentUser: User | null) => {
      if (currentUser) {
        req = req.clone({
          setHeaders: {
            Authorization: `Bearer ${currentUser.token}`
          }
        });
      }
    }
  });

  return next(req);
};
