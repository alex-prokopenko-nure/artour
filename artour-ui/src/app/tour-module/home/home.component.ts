import { Component, OnInit } from '@angular/core';
import { MatDialog, MatSnackBar } from '@angular/material';
import { AuthService } from 'src/app/shared-module/services/auth.service';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { SetPasswordComponent } from '../set-password/set-password.component';
import { Action } from 'src/app/shared-module/enums/action.enum';
import { TourService } from '../services/tour.service';
import { TourViewModel, CityViewModel, CountryViewModel, RegionViewModel } from 'src/app/shared-module/services/artour.api.service';
import { LocationsService } from '../services/locations.service';
import { Order } from '../enums/order.enum';
import { FileService } from '../services/file.service';
import { FilterType } from '../enums/filter.enum';
import { DeleteDialogComponent } from '../delete-dialog/delete-dialog.component';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  tours: TourViewModel[] = [];
  cities: CityViewModel[] = [];
  countries: CountryViewModel[] = [];
  regions: RegionViewModel[] = [];
  orderType: Order = Order.Visits;
  filterType: FilterType = FilterType.AllTours;
  searchWord: string;

  toursToShow: TourViewModel[] = [];

  constructor(   
    private authService: AuthService,
    private router: Router,
    private activatedRoute: ActivatedRoute,
    public dialog: MatDialog,
    private snackBar: MatSnackBar,
    private tourService: TourService,
    private fileService: FileService
    ) { }

  ngOnInit() {
    this.activatedRoute.queryParams.subscribe((params: Params) => {
      let queryToken = params['token'];
      if (queryToken) {
        const dialogRef = this.dialog.open(SetPasswordComponent, {data: queryToken});
        dialogRef.afterClosed().subscribe(result => {
          if (result == Action.Success) {
            history.replaceState(null, null, 'http://' + location.host + this.router.url.split('?')[0]);
            this.showCongratulations();
          }
        });
      }
    });

    this.getTours();
  }

  getTours = () => {
    this.tourService.getAllTours().subscribe(result => {
      this.tours = result;
      for (let i = 0; i < result.length; ++i) {
        let city = result[i].city;
        if (this.cities.findIndex(x => x.cityId == city.cityId) == -1) this.cities.push(city);
      }
      this.filter();
    });
  }

  get Order() {
    return Order;
  }

  get FilterType() {
    return FilterType;
  }

  showCongratulations = () => {
    this.snackBar.open("Password changed successfully", "Close", {duration: 5000});
  }

  filter = () => {
    let filteredTours = this.tours;
    if (this.filterType == FilterType.MyTours) {
      filteredTours = filteredTours.filter(x => this.usersTour(x));
    }
    if (this.searchWord) {
      filteredTours = filteredTours.filter(x => x.title.search(new RegExp(this.searchWord, "i")) != -1 || this.getCity(x.cityId).search(new RegExp(this.searchWord, "i")) != -1)
    }
    filteredTours = filteredTours.sort((a, b) => {
      if (this.orderType == Order.Visits) {
        let avisits = a.visits ? a.visits.length : 0;
        let bvisits = b.visits ? b.visits.length : 0;
        return bvisits - avisits;
      } else {
        return a.title < b.title ? 1 : a.title > b.title ? -1 : 0;
      }
    });
    this.toursToShow = filteredTours;
  }

  filteringTypeChanged = () => {
    this.filter();
  }

  getCity = (cityId: number) => {
    return this.cities.find(x => x.cityId == cityId).name
  }

  getTourImage = (tour: TourViewModel) => {
    if (tour.sights && tour.sights.length > 0 &&
      tour.sights[0].images && tour.sights[0].images.length > 0) {
      return this.fileService.getSightImageData(tour.sights[0].images[0].sightImageId);
    }
    return "../../../assets/images/noimage.png"
  }

  admin = () => {
    return this.authService.admin();
  }

  customer = () => {
    return this.authService.customer();
  }

  usersTour = (tour: TourViewModel) => {
    return tour.ownerId == this.authService.currentUserId;
  }

  goToTourDetails = (tourId: number) => {
    this.router.navigateByUrl(`tours/${tourId}`);
  }

  addTour = () => {
    let tour = new TourViewModel();
    tour.title = "_New_Tour_Title";
    tour.description = "_New_Tour_Description";
    tour.ownerId = this.authService.currentUserId;
    tour.cityId = 1;
    this.tourService.createTour(tour).subscribe(
      result => {
        this.goToTourDetails(result.tourId);
      }
    )
  }

  deleteTour = (tourId: number) => {
    const dialogRef = this.dialog.open(DeleteDialogComponent);
    dialogRef.afterClosed().subscribe(
      result => {
        if (result) {
          this.tourService.deleteTour(tourId).subscribe(
            result => {
              this.getTours();
            }
          );
        }
      }
    )
  }
}
