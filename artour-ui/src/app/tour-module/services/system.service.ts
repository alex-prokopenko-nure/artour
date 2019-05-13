import { Injectable } from "@angular/core";
import { ArtourApiService } from "src/app/shared-module/services/artour.api.service";

@Injectable({
  providedIn: 'root'
})
export class SystemService {
  constructor(private _artourApiService: ArtourApiService) {

  }

  checkConnection = () => {
    return this._artourApiService.checkConnection();
  }

  restartApi = () => {
    return this._artourApiService.restart();
  }
}