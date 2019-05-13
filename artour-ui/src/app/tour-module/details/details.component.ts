import { Component, OnInit, OnDestroy } from '@angular/core';
import { TourService } from '../services/tour.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { TourViewModel, TourStatisticsViewModel, CityViewModel, SightViewModel } from 'src/app/shared-module/services/artour.api.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { LocationsService } from '../services/locations.service';
import { AuthService } from 'src/app/shared-module/services/auth.service';
import { MatDialog } from '@angular/material';
import { SightDetailsComponent } from '../sight-details/sight-details.component';
import { SightService } from '../services/sight.service';
import { FileService } from '../services/file.service';
import { DeleteDialogComponent } from '../delete-dialog/delete-dialog.component';

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
  editMode: boolean = false;
  editForm: FormGroup;
  cities: CityViewModel[] = [];

  constructor(
    private authService: AuthService,
    private tourService: TourService,
    private sightService: SightService,
    private fileService: FileService,
    private locationsService: LocationsService,
    private route: ActivatedRoute,
    private router: Router,
    private builder: FormBuilder,
    private dialog: MatDialog
  ) { 
    this.editForm = builder.group({
      title: ["", Validators.required],
      cityId: [, Validators.required],
      description: ["", Validators.required]
    })
  }

  async ngOnInit() {
    this.sub = this.route.params.subscribe((params) => {
      this.tourId = +params['tourId'];
    });
    await this.getTour(this.tourId);
  }

  ngOnDestroy(): void {
    this.sub.unsubscribe();
  }

  getTour = async (tourId: number) => {
    await this.tourService.getTour(tourId).toPromise().then(
      result => {
        this.tour = result;
        for (let i = 0; i < this.tour.sights.length; ++i) {
          this.tour.sights[i].images = this.tour.sights[i].images.sort((a, b) => a.order - b.order);
        }
      }
    );
    await this.tourService.getTourStatistics(tourId).toPromise().then(
      result => {
        this.tourStatistics = result;
      }
    );
    await this.locationsService.getAllCities().toPromise().then(
      result => {
        this.cities = result;
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

  openEditMode = () => {
    this.editMode = true;
    this.editForm.controls['title'].setValue(this.tour.title);
    this.editForm.controls['cityId'].setValue(this.tour.cityId);
    this.editForm.controls['description'].setValue(this.tour.description);
  }

  closeEditMode = () => {
    this.editMode = false;
  }

  saveChanges = () => {
    this.closeEditMode();
    this.tour.title = this.editForm.controls['title'].value;
    this.tour.cityId = this.editForm.controls['cityId'].value;
    this.tour.description = this.editForm.controls['description'].value;

    this.tourService.updateTour(this.tourId, this.tour).subscribe();
  }

  getCity = (cityId: number) => {
    let city = this.cities.find(x => x.cityId == cityId)
    return city.name;
  }

  usersTour = () => {
    return this.authService.currentUserId == this.tour.ownerId;
  }

  admin = () => {
    return this.authService.admin();
  }

  addSight = async () => {
    let sight = new SightViewModel();
    sight.title = "Sight Title";
    sight.description = "Sight Description";
    sight.tourId = this.tourId;

    let sightId = 0;
    await this.sightService.createSight(sight).toPromise().then(
      result => {
        sightId = result.sightId;
      }
    );
    const dialogRef = this.dialog.open(SightDetailsComponent, {data: {sightId: sightId, tour: this.tour}});
    dialogRef.afterClosed().subscribe(
      result => {
        this.getTour(this.tourId);
      }
    )
  }

  showSight = (sightId: number) => {
    const dialogRef = this.dialog.open(SightDetailsComponent, {data: {sightId: sightId, tour: this.tour}});
    dialogRef.afterClosed().subscribe(
      result => {
        this.getTour(this.tourId);
      }
    )
  }

  getSightImage = (sight: SightViewModel) => {
    if (sight.images && sight.images.length > 0) {
      return this.fileService.getSightImageData(sight.images[0].sightImageId);
    }
    return "../../../assets/images/noimage.png"
  }

  deleteSight = (sightId: number) => {
    const dialogRef = this.dialog.open(DeleteDialogComponent);
    dialogRef.afterClosed().subscribe(
      result => {
        if (result) {
          this.sightService.deleteSight(sightId).subscribe(
            () => this.getTour(this.tourId)
          );
        }
      }
    );
  }
}
