import { Injectable } from '@angular/core';
import { ArtourApiService, LoginViewModel, UserViewModel, RegisterViewModel } from './artour.api.service';
import { Router } from '@angular/router';
import * as jwt_decode from 'jwt-decode';
import { routerNgProbeToken } from '@angular/router/src/router_module';
import { Action } from '../enums/action.enum';

@Injectable()
export class AuthService {
    TOKEN_NAME: string = 'artour_token';
    currentUserId: number;
    user: UserViewModel;
    constructor (private _artourApiService: ArtourApiService, private router: Router) {

    }

    login = async (loginModel: LoginViewModel) => {
        let status;
        await this._artourApiService.loginUser(loginModel).toPromise().then(
            async result => {
                localStorage.setItem(this.TOKEN_NAME, result);
                await this.getInfo();
                status = Action.Success;
            },
            error => {
                status = Action.Failed;
            }
        );
        return status;
    }

    getInfo = async () => {
        let token = localStorage.getItem(this.TOKEN_NAME);
        const decoded = jwt_decode(token);
        this.currentUserId = +decoded["sub"];
        await this._artourApiService.getUser(this.currentUserId).toPromise().then(
            result => {
                this.user = result;
            }
        );
    }
    
    signedIn = () => {
      return Date.now() < this.getExpirationTime();
    }
    
    getExpirationTime = () => {
      const token = localStorage.getItem(this.TOKEN_NAME);
    
      if (token) {
        const decoded = jwt_decode(token);
        const expirationTime = decoded["exp"] * 1000;
        return expirationTime;
      }
    
      return 0;
    }
    
    logout = () => {
      localStorage.removeItem(this.TOKEN_NAME);
      this.currentUserId = undefined;
      this.user = undefined;
      this.router.navigateByUrl("tours/home");
    }
    
    register = async (model: RegisterViewModel) => {
      let status;
      await this._artourApiService.registerUser(
        model
      ).toPromise().then(() => {
          status = Action.Success;
        },
        error => {
          status = Action.Failed;
        }
      );
      return status;
    }
}