import { Injectable } from "@angular/core";
import { ArtourApiService } from "src/app/shared-module/services/artour.api.service";

@Injectable()
export class MailService {
  constructor(private _artourApiService: ArtourApiService) {

  }

  sendResetLink = (email: string) => {
    return this._artourApiService.sendConfirmationEmail(email);
  }
}