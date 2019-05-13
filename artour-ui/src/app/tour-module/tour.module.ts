import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TourRoutingModule } from './tour-routing.module';
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { HomeComponent } from './home/home.component';
import { ProfileComponent } from './profile/profile.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { SetPasswordComponent } from './set-password/set-password.component';
import {
  MatCheckboxModule,
  MatButtonModule,
  MatInputModule,
  MatAutocompleteModule,
  MatDatepickerModule,
  MatFormFieldModule,
  MatRadioModule,
  MatSelectModule,
  MatSliderModule,
  MatSlideToggleModule,
  MatMenuModule,
  MatSidenavModule,
  MatToolbarModule,
  MatListModule,
  MatGridListModule,
  MatCardModule,
  MatStepperModule,
  MatTabsModule,
  MatExpansionModule,
  MatButtonToggleModule,
  MatChipsModule,
  MatIconModule,
  MatProgressSpinnerModule,
  MatProgressBarModule,
  MatDialogModule,
  MatTooltipModule,
  MatSnackBarModule,
  MatTableModule,
  MatSortModule,
  MatPaginatorModule,
  MatBottomSheetModule
} from '@angular/material';
import { MatMomentDateModule, MAT_MOMENT_DATE_ADAPTER_OPTIONS } from '@angular/material-moment-adapter';
import { MailService } from './services/mail.service';
import { UserService } from './services/user.service';
import { TourService } from './services/tour.service';
import { LocationsService } from './services/locations.service';
import { FileService } from './services/file.service';
import { DeleteDialogComponent } from './delete-dialog/delete-dialog.component';
import { DetailsComponent } from './details/details.component';
import { SightDetailsComponent } from './sight-details/sight-details.component';
import { DragDropModule } from '@angular/cdk/drag-drop';
import { SightService } from './services/sight.service';
import { ImageDialogComponent } from './image-dialog/image-dialog.component';
import { SwiperModule, SWIPER_CONFIG, SwiperConfigInterface } from 'ngx-swiper-wrapper';
import { ShareButtonsComponent } from './share-buttons/share-buttons.component';
import { VisitDetailsComponent } from './visit-details/visit-details.component';
import { VisitService } from './services/visit.service';
import { ShareButtonsModule } from '@ngx-share/buttons';
import { SharedModule } from '../shared-module';
import { BrowserModule } from '@angular/platform-browser';
import { SystemComponent } from './system/system.component';

const DEFAULT_SWIPER_CONFIG: SwiperConfigInterface = {
  direction: 'horizontal',
  slidesPerView: 1,
  observer: true
};

@NgModule({
  declarations: [HomeComponent, ProfileComponent, LoginComponent, RegisterComponent, SetPasswordComponent, DeleteDialogComponent, DetailsComponent, SightDetailsComponent, ImageDialogComponent, ShareButtonsComponent, VisitDetailsComponent, SystemComponent],
  imports: [
    BrowserModule,
    CommonModule,
    TourRoutingModule,
    FormsModule,
    SharedModule,
    MatBottomSheetModule,
    ReactiveFormsModule,
    MatCheckboxModule,
    MatButtonModule,
    MatInputModule,
    MatAutocompleteModule,
    MatDatepickerModule,
    MatFormFieldModule,
    MatRadioModule,
    MatSelectModule,
    MatSliderModule,
    MatSlideToggleModule,
    MatMenuModule,
    MatSidenavModule,
    MatToolbarModule,
    MatListModule,
    MatGridListModule,
    MatCardModule,
    MatStepperModule,
    MatTabsModule,
    MatExpansionModule,
    MatButtonToggleModule,
    MatChipsModule,
    MatIconModule,
    MatProgressSpinnerModule,
    MatProgressBarModule,
    MatDialogModule,
    MatTooltipModule,
    MatSnackBarModule,
    MatTableModule,
    MatSortModule,
    MatPaginatorModule,
    MatMomentDateModule,
    DragDropModule,
    SwiperModule,
    ShareButtonsModule
  ],
  providers: [
    { provide: MAT_MOMENT_DATE_ADAPTER_OPTIONS, useValue: { useUtc: true } },
    MailService,
    UserService,
    TourService,
    LocationsService,
    FileService, 
    SightService,
    VisitService,
    {
      provide: SWIPER_CONFIG,
      useValue: DEFAULT_SWIPER_CONFIG
    }
  ],
  entryComponents: [SetPasswordComponent, DeleteDialogComponent, SightDetailsComponent, ImageDialogComponent, ShareButtonsComponent]
})
export class TourModule { }
