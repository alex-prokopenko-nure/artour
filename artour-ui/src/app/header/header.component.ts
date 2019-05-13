import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../shared-module/services/auth.service';
import { TranslateService } from '@ngx-translate/core';
import { LanguageService } from '../tour-module/services/language.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {

  constructor(
    private router: Router, 
    private authService: AuthService,
    private languageService: LanguageService,
    private translate: TranslateService
  ) { 
    // this language will be used as a fallback when a translation isn't found in the current language
    translate.setDefaultLang('en');

    // the lang to use, if the lang isn't available, it will use the current loader to get them
    translate.use('en');
  }

  ngOnInit() {
  }

  navigate = (path: string) => {
    this.router.navigateByUrl(`/tours/${path}`)
  }

  signedIn = () => {
    return this.authService.signedIn();
  }

  adminOrCustomer = () => {
    return this.authService.admin() || this.authService.customer();
  }

  logout = () => {
    this.authService.logout();
  }
}
