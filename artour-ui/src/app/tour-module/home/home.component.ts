import { Component, OnInit } from '@angular/core';
import { MatDialog, MatSnackBar } from '@angular/material';
import { AuthService } from 'src/app/shared-module/services/auth.service';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { SetPasswordComponent } from '../set-password/set-password.component';
import { Action } from 'src/app/shared-module/enums/action.enum';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  constructor(   
    private authService: AuthService,
    private router: Router,
    private activatedRoute: ActivatedRoute,
    public dialog: MatDialog,
    private snackBar: MatSnackBar
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
  }

  showCongratulations = () => {
    this.snackBar.open("Password changed successfully", "Close", {duration: 5000});
  }

}
