import { Component, OnInit, ViewChild, Inject } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { SightImageViewModel } from 'src/app/shared-module/services/artour.api.service';
import { DomSanitizer } from '@angular/platform-browser';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { FileService } from '../services/file.service';

@Component({
  selector: 'app-image-dialog',
  templateUrl: './image-dialog.component.html',
  styleUrls: ['./image-dialog.component.scss']
})
export class ImageDialogComponent implements OnInit {
  @ViewChild("image")
  fileImage;
  
  imageForm: FormGroup;
  uploadedFile: File = undefined;
  imgSrcToShow;
  imageId: number;
  sightId: number;
  imageToEdit: SightImageViewModel;
  errorMessage: string;
  clicked: boolean = false;

  constructor(
    private sanitizer: DomSanitizer,
    public dialogRef: MatDialogRef<ImageDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    builder: FormBuilder,
    private fileService: FileService
    ) { 
      if (data) {
        this.imageId = data.imageId;
        this.sightId = data.sightId;
        if (this.imageId) {
          fileService.getImage(this.imageId).subscribe(
            result => {
              this.imageToEdit = result;
              this.imageForm.controls['description'].setValue(result.description);
            }
          );
          this.imgSrcToShow = fileService.getSightImageData(this.imageId) + "?" + Date.now().toString();
        } else {
          this.imgSrcToShow = "../../../assets/images/noimage.png";
        }
      }
      this.imageForm = builder.group({ 
          description: ["", Validators.required]
      });
    }

  ngOnInit() {
  }

  close = () => {
    this.dialogRef.close();
  }

  addFile = () => {
    this.fileImage.nativeElement.click();
  }

  onFileAdded() {
    if (!this.validateFileInput(this.fileImage.nativeElement.files[0])) {
      return;
    }
    this.uploadedFile = this.fileImage.nativeElement.files[0];
    this.imgSrcToShow = this.sanitizer.bypassSecurityTrustUrl(URL.createObjectURL(this.uploadedFile));
  }

  validateFileInput = (image: any) => {
    const _validFileExtensions = [".jpg", ".jpeg", ".bmp", ".gif", ".png"];
     // tslint:disable-next-line:prefer-const
     let sFileName = image.name;
     if (sFileName.length > 0) {
       let blnValid = false;
       for (let j = 0; j < _validFileExtensions.length; j++) {
         const sCurExtension = _validFileExtensions[j];
         if (
           sFileName
             .substr(
               sFileName.length - sCurExtension.length,
               sCurExtension.length
             )
             .toLowerCase() === sCurExtension.toLowerCase()
         ) {
           blnValid = true;
           break;
         }
       }

       if (!blnValid) {
         alert(
           "Sorry, " +
             sFileName +
             " is invalid, allowed extensions are: " +
             _validFileExtensions.join(", ")
         );
         image.value = "";
         return false;
       }
     }
     return true;
  }

  saveImage = () => {
    this.errorMessage = '';
    if (this.imageForm.valid && (this.uploadedFile || this.imageToEdit) && !this.clicked) {
      this.clicked = true;
      const val = this.imageForm.value;
      if (this.imageToEdit) {
        if (this.uploadedFile) {
          this.fileService.updateImage(this.imageId, {data: this.uploadedFile, fileName: this.uploadedFile.name}).subscribe(
            res => {
              this.dialogRef.close(true);
            }
          );
        } else {
          this.dialogRef.close(true)
        }
      } else {
        this.fileService.createImage({data: this.uploadedFile, fileName: this.uploadedFile.name}, val.description, this.sightId).subscribe(
          () => this.dialogRef.close(true)
        );
      }
    } else {
      this.errorMessage = "Fill image description please";
    }
  }
}
