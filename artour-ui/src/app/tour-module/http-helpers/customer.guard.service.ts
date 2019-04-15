import { Injectable } from "@angular/core";
import { Router, CanActivate } from "@angular/router";
import { AuthService } from "src/app/shared-module/services/auth.service";

@Injectable()
export class CustomerGuardService implements CanActivate {

  constructor(public auth: AuthService, public router: Router) { }

  canActivate(): boolean {
    if (!this.auth.adminOrCustomer()) {
      this.router.navigateByUrl("/tours/home");
      return false;
    } 
    return true;
  }
}