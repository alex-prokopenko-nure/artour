import { Component, OnInit } from "@angular/core";
import { AuthService } from "src/app/shared-module/services/auth.service";
import { UserViewModel, UserStatisticsViewModel, VisitInfoViewModel } from "src/app/shared-module/services/artour.api.service";
import { UserService } from "../services/user.service";
import { MatBottomSheet } from "@angular/material";
import { DeleteDialogComponent } from "../delete-dialog/delete-dialog.component";
import { ShareButtonsComponent } from "../share-buttons/share-buttons.component";
import { Router } from "@angular/router";

@Component({
  selector: "app-profile",
  templateUrl: "./profile.component.html",
  styleUrls: ["./profile.component.scss"]
})
export class ProfileComponent implements OnInit {
  user: UserViewModel = new UserViewModel();
  userStatistics: UserStatisticsViewModel = new UserStatisticsViewModel();
  recentVisits: VisitInfoViewModel[] = [];
  displayedColumns: string[] = ["Tour", "City", "Duration", "Actions"];

  constructor(
    private authService: AuthService,
    private userService: UserService,
    private router: Router
  ) {}

  async ngOnInit() {
    if (this.authService.user) {
      this.user = this.authService.user;
    }
    if (!this.user.userId) {
      await this.userService.getUser(this.authService.currentUserId).toPromise().then(
        result => {
          this.user = result;
        }
      )
    }
    this.userService.getUserStatistics(this.user.userId).subscribe(
      result => {
        this.userStatistics = result;
        this.recentVisits = this.userStatistics.visits && this.userStatistics.visits.length >= 5 ? this.userStatistics.visits.slice(0, 5) : this.userStatistics.visits;
      }
    );
  }

  getFormattedTime = (seconds: number) => {
    let fullMinutes = Math.floor(seconds / 60);
    let fullHours = Math.floor(fullMinutes / 60);
    let result = "";
    if (fullHours) result += `${fullHours}:`;
    if (fullMinutes || fullHours) result += `${fullMinutes - fullHours * 60}:`;
    result += `${seconds - fullMinutes * 60}`;
    return result; 
  }

  showVisit = (visitId: string) => {
    this.router.navigateByUrl(`tours/visit/${visitId}`)
  }
}
