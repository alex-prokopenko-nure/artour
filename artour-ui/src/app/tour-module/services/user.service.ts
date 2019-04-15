import { Injectable } from "@angular/core";
import { ArtourApiService, ResetPasswordViewModel } from "src/app/shared-module/services/artour.api.service";

@Injectable()
export class UserService {
  constructor(private _artourApiService: ArtourApiService) {

  }

  resetAndChangePassword = (model: ResetPasswordViewModel) => {
    return this._artourApiService.resetAndChangePassword(model);
  }
}