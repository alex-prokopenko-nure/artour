import { Injectable } from "@angular/core";
import { ArtourApiService } from "src/app/shared-module/services/artour.api.service";

@Injectable()
export class LocationsService {
  constructor(private _artourApiService: ArtourApiService) {

  }

  getAllCities = () => {
    return this._artourApiService.getAllCities();
  }

  getAllCountries = () => {
    return this._artourApiService.getAllCountries();
  }

  getAllRegions = () => {
    return this._artourApiService.getAllRegions();
  }
}