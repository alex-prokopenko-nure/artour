import { Injectable } from "@angular/core";
import { ArtourApiService, TourViewModel } from "../../shared-module/services/artour.api.service";

@Injectable()
export class TourService {
  constructor(private _artourApiService: ArtourApiService) {

  }

  getAllTours = () => {
    return this._artourApiService.getAllTours();
  }

  getTour = (tourId: number) => {
    return this._artourApiService.getTour(tourId);
  }

  getTourStatistics = (tourId: number) => {
    return this._artourApiService.getTourStatistics(tourId);
  }

  getUsersTours = (userId: number) => {
    return this._artourApiService.getUsersTours(userId);
  } 

  createTour = (tour: TourViewModel) => {
    return this._artourApiService.createTour(tour);
  }

  deleteTour = (tourId: number) => {
    return this._artourApiService.deleteTour(tourId);
  }

  updateTour = (tourId: number, tour: TourViewModel) => {
    return this._artourApiService.updateTour(tourId, tour);
  }
}