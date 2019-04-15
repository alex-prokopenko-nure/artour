import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { UserService } from '../services/user.service';
import { Action } from 'src/app/shared-module/enums/action.enum';
import { ResetPasswordViewModel } from 'src/app/shared-module/services/artour.api.service';

@Component({
  selector: 'app-set-password',
  templateUrl: './set-password.component.html',
  styleUrls: ['./set-password.component.scss']
})
export class SetPasswordComponent implements OnInit {
  changePasswordForm: FormGroup;
  queryToken: string;
  error: boolean = false;
  clicked: boolean = false;


  successText: string = "";
  errorText: string = "";
  
  constructor(
    public dialogRef: MatDialogRef<SetPasswordComponent>,
    @Inject(MAT_DIALOG_DATA) public data: string,
    formBuilder: FormBuilder,
    private userService: UserService
    ) {

    this.changePasswordForm = formBuilder.group({
      password: ["", Validators.compose([Validators.required, Validators.minLength(6)]) ],
      confirmPassword: ["", Validators.required]
    }, {
      validator: this.EqualityValidator
    });

    this.queryToken = data;
  }

  EqualityValidator(control: FormGroup) {
    const val = control.value;
    let condition = val.password == val.confirmPassword;
    if (!condition) {
      return {
        EqualityValidator: 'passwords do not match'
      }
    }
    return null;
  }
  
  ngOnInit() {
  }

  close = () => {
    this.dialogRef.close();
  }

  setPassword = async () => {
    this.clicked = true;
    if (this.changePasswordForm.valid) {
      this.error = false;
      const formValue = this.changePasswordForm.value;
      let changePasswordStatus: Action; 
      let resetPasswordModel: ResetPasswordViewModel = new ResetPasswordViewModel();
      resetPasswordModel.token = this.queryToken;
      resetPasswordModel.password = formValue.password;
      await this.userService.resetAndChangePassword(resetPasswordModel).toPromise()
        .then(() => {
          changePasswordStatus = Action.Success
        },
        error => {
          changePasswordStatus = Action.Failed
          this.successText = "";
          this.errorText = "New password setup failed"
        }
      );
      if (changePasswordStatus == Action.Success) {
        this.dialogRef.close(changePasswordStatus);
      }
    } else {
      if (this.changePasswordForm.value.password != this.changePasswordForm.value.confirmPassword) {
        this.successText = "";
        this.errorText = "Passwords do not match!"
      }
    }
  }
}
