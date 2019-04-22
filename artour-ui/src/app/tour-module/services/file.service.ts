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

  createImage = (image: FileParameter, title: string, sightId: number) => {
    return this._artourApiService.createSightImage(title, sightId, image);
  };

  changeOrder = (previousIndex: number, currentIndex: number) => {
    return this._artourApiService.changeOrder(previousIndex, currentIndex);
  }

  updateImage = (id: number, image: FileParameter) => {
    return this._artourApiService.updateImage(id, image);
  }

  deleteImage = (imageId: number) => {
    return this._artourApiService.deleteImage(imageId);
  }
}