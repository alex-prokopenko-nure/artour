import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { VisitService } from '../services/visit.service';
import { VisitViewModel } from 'src/app/shared-module/services/artour.api.service';
import { FileService } from '../services/file.service';
import * as moment from 'moment';
import { MatBottomSheet } from '@angular/material';
import { ShareButtonsComponent } from '../share-buttons/share-buttons.component';
import { AuthService } from 'src/app/shared-module/services/auth.service';

@Component({
  selector: 'app-visit-details',
  templateUrl: './visit-details.component.html',
  styleUrls: ['./visit-details.component.scss']
})
export class VisitDetailsComponent implements OnInit, OnDestroy {
  sub: any;
  visitId: string;
  visit: VisitViewModel;

  constructor(
    private route: ActivatedRoute,
    private visitService: VisitService,
    private fileService: FileService,
    private authService: AuthService,
    private bottomSheet: MatBottomSheet
  ) { }

  ngOnInit(): void {
    this.sub = this.route.params.subscribe((params) => {
      this.visitId = params['visitId'];
    });
    this.visitService.getVisit(this.visitId).subscribe(
      result => {
        this.visit = result;
      }
    );
  }

  ngOnDestroy(): void {
    this.sub.unsubscribe();
  }

  getSightImage = (sightId: number) => {
    let images = this.visit.tour.sights.find(x => x.sightId == sightId).images.sort((a, b) => a.order - b.order);
    if (images && images.length > 0) {
      return this.fileService.getSightImageData(images[0].sightImageId);
    }
    return "../../../assets/images/noimage.png";
  }

  getSightTitle = (sightId: number) => {
    return this.visit.tour.sights.find(x => x.sightId == sightId).title;
  }

  getTitle = () => {
    return `${this.visit.user.username} visited ${this.visit.tour.title} at ${this.visit.startDate.format('MM/DD/YYYY HH:mm:ss')}`;
  }

  getTourTime = () => {
    return moment.utc(this.visit.endDate.diff(this.visit.startDate)).format("HH:mm:ss");
  }

  share = () => {
    this.bottomSheet.open(ShareButtonsComponent);
  }

  usersTour = () => {
    return this.visit.tour.ownerId == this.authService.currentUserId;
  }
}
