import { Injectable } from "@angular/core";
import { ArtourApiService, ResetPasswordViewModel } from "src/app/shared-module/services/artour.api.service";

@Injectable()
export class UserService {
  constructor(private _artourApiService: ArtourApiService) {

  }

  resetAndChangePassword = (model: ResetPasswordViewModel) => {
    return this._artourApiService.resetAndChangePassword(model);
  }

  getUserStatistics = (userId: number) => {
    return this._artourApiService.getUserStatistics(userId);
  }

  getUser = (userId: number) => {
    return this._artourApiService.getUser(userId);
  }
}