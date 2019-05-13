import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ArtourApiService, ARTOUR_API_BASE_URL } from './services/artour.api.service';
import { environment } from 'src/environments/environment';
import { TranslateModule, TranslateService, TranslateStore, TranslateLoader } from '@ngx-translate/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule, HttpClient } from '@angular/common/http';
import {TranslateHttpLoader} from '@ngx-translate/http-loader';

export function HttpLoaderFactory(http: HttpClient) {
  return new TranslateHttpLoader(http);
}

@NgModule({
  declarations: [],
  providers: [
    ArtourApiService,
    { provide: ARTOUR_API_BASE_URL, useValue: environment.ARTOUR_API_BASE_URL },
  ],
  imports: [
    CommonModule,
    HttpClientModule,
    TranslateModule.forRoot({
        loader: {
            provide: TranslateLoader,
            useFactory: HttpLoaderFactory,
            deps: [HttpClient]
        }
    })
  ]
})
export class SharedModule { }
