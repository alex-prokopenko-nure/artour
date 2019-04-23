import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { ProfileComponent } from './profile/profile.component';
import { UnauthGuardService } from './http-helpers/unauth.guard.service';
import { AuthGuardService } from './http-helpers/auth.guard.service';
import { DetailsComponent } from './details/details.component';
import { VisitDetailsComponent } from './visit-details/visit-details.component';

const routes: Routes = [
  {
    path: 'home',
    component: HomeComponent
  },
  {
    path: 'login',
    component: LoginComponent,
    canActivate: [UnauthGuardService]
  },
  {
    path: 'register',
    component: RegisterComponent,
    canActivate: [UnauthGuardService]
  },
  {
    path: 'profile',
    component: ProfileComponent,
    canActivate: [AuthGuardService]
  },
  {
    path: ':tourId',
    component: DetailsComponent
  },
  {
    path: 'visit/:visitId',
    component: VisitDetailsComponent
  },
  { path: "**", redirectTo: "home" }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class TourRoutingModule { }
