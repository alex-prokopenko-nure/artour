import { Component, OnInit, OnDestroy } from '@angular/core';
import { TourService } from '../services/tour.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { TourViewModel, TourStatisticsViewModel } from 'src/app/shared-module/services/artour.api.service';

@Component({
  selector: 'app-details',
  templateUrl: './details.component.html',
  styleUrls: ['./details.component.scss']
})
export class DetailsComponent implements OnInit, OnDestroy {
  private sub: Subscription;
  tourId: number;
  tour: TourViewModel;
  tourStatistics: TourStatisticsViewModel;

  constructor(
    private tourService: TourService,
    private route: ActivatedRoute,
    private router: Router
  ) { 

  }

  ngOnInit(): void {
    this.sub = this.route.params.subscribe((params) => {
      this.tourId = +params['tourId'];
    });
    this.getTour(this.tourId);
  }

  ngOnDestroy(): void {
    this.sub.unsubscribe();
  }

  getTour = (tourId: number) => {
    this.tourService.getTour(tourId).subscribe(
      result => {
        this.tour = result;
      }
    );
    this.tourService.getTourStatistics(tourId).subscribe(
      result => {
        this.tourStatistics = result;
      }
    )
  }

  getAverageTime = (time: number) => {
    time = Math.floor(time);
    let fullSeconds  = Math.floor(time / 1000);
    let fullMinutes = Math.floor(fullSeconds / 60);
    let fullHours = Math.floor(fullMinutes / 60);
    let result = "";
    if (fullHours) result += `${fullHours} hour(s) `;
    if (fullMinutes || fullHours) result += `${fullMinutes - fullHours * 60} minute(s) `;
    result += `${fullSeconds - fullMinutes * 60} second(s)`;
    return result; 
  }
}
