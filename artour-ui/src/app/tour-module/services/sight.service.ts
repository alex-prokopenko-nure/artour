import { Injectable } from "@angular/core";
import { ArtourApiService, SightViewModel } from "src/app/shared-module/services/artour.api.service";

@Injectable()
export class SightService {
  constructor(private _artourApiService: ArtourApiService) {

  }

  createSight = (sight: SightViewModel) => {
    return this._artourApiService.createSight(sight);
  }

  getSight = (sightId: number) => {
    return this._artourApiService.getSightById(sightId);
  }

  deleteSight = (sightId: number) => {
    return this._artourApiService.deleteSight(sightId);
  }
}