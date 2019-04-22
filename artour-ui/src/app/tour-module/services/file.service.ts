import { Injectable, Inject } from "@angular/core";
import { ArtourApiService, ARTOUR_API_BASE_URL, FileParameter } from "src/app/shared-module/services/artour.api.service";

@Injectable()
export class FileService {
  _baseUrl: string;
  constructor(private _artourApiService: ArtourApiService, @Inject(ARTOUR_API_BASE_URL) baseUrl?: string) {
    this._baseUrl = baseUrl;
  }

  getImage = (imageId: number) => {
    return this._artourApiService.getImageById(imageId);
  }

  getSightImageData = (sightImageId: number) => {
    return `${this._baseUrl}/api/sight-images/${sightImageId}/data`;
  }

  createImage = (image: FileParameter, description: string, sightId: number) => {
    return this._artourApiService.createSightImage(description, sightId, image);
  };

  changeOrder = (previousIndex: number, currentIndex: number, sightId: number) => {
    return this._artourApiService.changeOrder(previousIndex, currentIndex, sightId);
  }

  updateImage = (id: number, image: FileParameter) => {
    return this._artourApiService.updateImage(id, image);
  }

  deleteImage = (imageId: number) => {
    return this._artourApiService.deleteImage(imageId);
  }
}