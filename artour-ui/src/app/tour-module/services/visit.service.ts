import { Injectable } from "@angular/core";
import { ArtourApiService } from "src/app/shared-module/services/artour.api.service";

@Injectable()
export class VisitService {
  constructor(private _artourApiService: ArtourApiService) {

  }

  getVisit = (visitId: string) => {
    return this._artourApiService.getVisit(visitId);
  }
}