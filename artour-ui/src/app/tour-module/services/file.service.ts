import { Injectable, Inject } from "@angular/core";
import { ArtourApiService, ARTOUR_API_BASE_URL } from "src/app/shared-module/services/artour.api.service";

@Injectable()
export class FileService {
  _baseUrl: string;
  constructor(private _artourApiService: ArtourApiService, @Inject(ARTOUR_API_BASE_URL) baseUrl?: string) {
    this._baseUrl = baseUrl;
  }

  getSightImageData = (sightImageId: number) => {
    return `${this._baseUrl}/api/sight-images/${sightImageId}/data`;
  }
}