import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA, MatDialog } from '@angular/material';
import { SightService } from '../services/sight.service';
import { SightViewModel, SightImageViewModel } from 'src/app/shared-module/services/artour.api.service';
import { FileService } from '../services/file.service';
import { CdkDragDrop, moveItemInArray, transferArrayItem } from '@angular/cdk/drag-drop';
import { DeleteDialogComponent } from '../delete-dialog/delete-dialog.component';
import { ImageDialogComponent } from '../image-dialog/image-dialog.component';

@Component({
  selector: 'app-sight-details',
  templateUrl: './sight-details.component.html',
  styleUrls: ['./sight-details.component.scss']
})
export class SightDetailsComponent implements OnInit {
  sightId: number;
  sight: SightViewModel = new SightViewModel();
  imagesData = [];
  sources = [];

  constructor(
    private dialogRef: MatDialogRef<SightDetailsComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private sightService: SightService,
    private fileService: FileService,
    private dialog: MatDialog
  ) { 

  }

  ngOnInit() {
    this.sightId = this.data.sightId;
    this.fillLists();
  }

  fillLists = () => {
    this.sightService.getSight(this.sightId).subscribe(
      result => {
        this.sight = result;
        this.sight.images = this.sight.images.sort((a, b) => a.order - b.order);
        this.splitImagesToLists();
        for (let i = 0; i < this.sight.images.length; ++i) {
          this.sources[i] = this.fileService.getSightImageData(this.sight.images[i].sightImageId) + "?" + Date.now().toString();
        }
      }
    )
  }

  drop(event: CdkDragDrop<SightImageViewModel[]>) {
    let previousContainerIndex = this.findIndexInData(event.previousContainer.data);
    let containerIndex = this.findIndexInData(event.container.data);
    let previousOrder = previousContainerIndex * 4 + event.previousIndex;
    let currentOrder = containerIndex * 4 + event.currentIndex;
    if (event.previousContainer === event.container) {
      moveItemInArray(event.container.data, event.previousIndex, event.currentIndex);
      moveItemInArray(this.sources, previousOrder, currentOrder);
      moveItemInArray(this.sight.images, previousOrder, currentOrder);
    } else {     
      let pushUpMade = 0;
      transferArrayItem(event.previousContainer.data,
                        event.container.data,
                        event.previousIndex,
                        event.currentIndex);
      if (previousContainerIndex < containerIndex) {
        for (let i = containerIndex; i > previousContainerIndex; --i) {
          transferArrayItem(this.imagesData[i], this.imagesData[i - 1], 0, 4);
        }
        currentOrder -= 1;
      } else {
        for (let i = containerIndex; i < previousContainerIndex; ++i) {
          transferArrayItem(this.imagesData[i], this.imagesData[i + 1], 4, 0);
        }
      }
      moveItemInArray(this.sources, previousOrder, currentOrder);
      moveItemInArray(this.sight.images, previousOrder, currentOrder);
    }
    this.fileService.changeOrder(previousOrder, currentOrder, this.sightId).subscribe(
      () => console.log("success"),
      () => console.log("error")
    );
  }

  checkData = () => {
    for (let i = 0; i < this.imagesData.length - 1; ++i) {
      if (this.imagesData[i].length < 4) {
        this.pushUpLists(i);
        return 1;
      }
    }
    return 0;
  }

  pushUpLists = (index: number) => {
    for (let i = this.imagesData.length - 1; i > index; --i) {
      transferArrayItem(this.imagesData[i], this.imagesData[i - 1], 0, 4);
    }
  }

  findIndexInData = (arr: SightImageViewModel[]) => {
    for(let i = 0; i < this.imagesData.length; ++i) {
      if(this.imagesData[i].every(x => arr.find(y => y.sightImageId == x.sightImageId)))
        return i;
    }
    return -1;
  }

  openDialog = () => {
    const dialogRef = this.dialog.open(ImageDialogComponent, {data: {imageId: undefined, sightId: this.sightId}});
    dialogRef.afterClosed().subscribe(
      res => {
        if (res) {
          this.fillLists()
        }
      }
    );
  }

  splitImagesToLists = () => {
    this.imagesData = [];
    for (let i = 0; i < this.sight.images.length; i += 4) {
      this.imagesData[i / 4] = this.sight.images.slice(i, this.sight.images.length - i >= 4 ?  i + 4 : this.sight.images.length);
    }
  }

  updateImage = (imageId: number) => {
    const dialogRef = this.dialog.open(ImageDialogComponent,
      {data: {imageId: imageId, sightId: this.sightId}}
    );
    dialogRef.afterClosed().subscribe(
      res => {
        if (res) {
          let i = this.sight.images.findIndex(x => x.sightImageId == imageId);
          this.sources[i] = this.fileService.getSightImageData(imageId) + "?" + Date.now().toString();
        }
      }
    );
  }

  deleteImage = (imageId: number) => {
    const dialogRef = this.dialog.open(DeleteDialogComponent);
    dialogRef.afterClosed().subscribe(
      result => {
        if (result) {
          this.fileService.deleteImage(imageId).subscribe(
            () => this.fillLists()
          );
        }
      }
    );
  }
}
