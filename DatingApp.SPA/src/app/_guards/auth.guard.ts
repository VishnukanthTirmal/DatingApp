import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { Observable } from 'rxjs/Observable';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';
import { Router } from '@angular/router';

@Injectable()

export class AuthGuard implements CanActivate {
  constructor(private authService: AuthService, private alertify: AlertifyService, private router: Router) {

  }
 canActivate(): Observable<boolean> | Promise<boolean> | boolean {
  if (this.authService.loggedIn())  {
    return true;
  }
  this.alertify.error('You need to be logged in to access this area');
  this.router.navigate(['/home']);
  return false;
  }
}
