import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/shared-module/services/auth.service';
import { UserViewModel } from 'src/app/shared-module/services/artour.api.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {
  user: UserViewModel = new UserViewModel();

  constructor(private authService: AuthService) { }

  ngOnInit() {
    this.user = this.authService.user;
  }

}
