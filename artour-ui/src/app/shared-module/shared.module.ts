import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ArtourApiService, ARTOUR_API_BASE_URL } from './services/artour.api.service';
import { environment } from 'src/environments/environment';

@NgModule({
  declarations: [],
  providers: [
    ArtourApiService,
    { provide: ARTOUR_API_BASE_URL, useValue: environment.ARTOUR_API_BASE_URL } 
  ],
  imports: [
    CommonModule
  ]
})
export class SharedModule { }
