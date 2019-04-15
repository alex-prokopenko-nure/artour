import { Injectable } from "@angular/core";
import { Router, CanActivate } from "@angular/router";
import { AuthService } from "src/app/shared-module/services/auth.service";

@Injectable()
export class AuthGuardService implements CanActivate {

  constructor(public auth: AuthService, public router: Router) { }

  canActivate(): boolean {
    if (!this.auth.signedIn()) {
      this.router.navigateByUrl("/tours/home");
      return false;
    } 
    return true;
  }
}
