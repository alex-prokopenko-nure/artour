import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../shared-module/services/auth.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {

  constructor(private router: Router, private authService: AuthService) { }

  ngOnInit() {
  }

  navigate = (path: string) => {
    this.router.navigateByUrl(`/tours/${path}`)
  }

  signedIn = () => {
    return this.authService.signedIn();
  }

  adminOrCustomer = () => {
    return this.authService.adminOrCustomer();
  }

  logout = () => {
    this.authService.logout();
  }
}
