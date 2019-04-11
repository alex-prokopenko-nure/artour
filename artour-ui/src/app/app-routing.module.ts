import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

const routes: Routes = [
  {
    path: "tours",
    loadChildren: "./tour-module/tour.module#TourModule"
  },
  { path: "", pathMatch: 'full', redirectTo: "tours" },
  { path: "*", redirectTo: "tours" }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
