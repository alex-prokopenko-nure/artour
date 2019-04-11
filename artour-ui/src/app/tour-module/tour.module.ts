import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TourRoutingModule } from './tour-routing.module';
import { FormsModule } from "@angular/forms";
import { HomeComponent } from './home/home.component';
import { ProfileComponent } from './profile/profile.component';
import { ManageComponent } from './manage/manage.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { SetPasswordComponent } from './set-password/set-password.component';

@NgModule({
  declarations: [HomeComponent, ProfileComponent, ManageComponent, LoginComponent, RegisterComponent, SetPasswordComponent],
  imports: [
    CommonModule,
    TourRoutingModule,
    FormsModule
  ],
})
export class TourModule { }
