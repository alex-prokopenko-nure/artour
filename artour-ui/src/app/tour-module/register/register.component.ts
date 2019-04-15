import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import * as moment from 'moment';
import { StepperSelectionEvent } from '@angular/cdk/stepper';
import { RegisterViewModel } from 'src/app/shared-module/services/artour.api.service';
import { AuthService } from 'src/app/shared-module/services/auth.service';
import { Action } from 'src/app/shared-module/enums/action.enum';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {
  isLinear: boolean = true;
  showError: boolean = false;
  usernameFormGroup: FormGroup;
  infoFormGroup: FormGroup;
  passwordFormGroup: FormGroup;
  minDate: moment.Moment = moment(moment.now()).utc().hours(0).minutes(0).seconds(0).milliseconds(0).subtract(100, 'years');
  maxDate: moment.Moment = moment(moment.now()).utc().hours(0).minutes(0).seconds(0).milliseconds(0).subtract(15, 'years');
  passwordFormClicked: boolean = false;
  userModel: RegisterViewModel = new RegisterViewModel();

  constructor(private _formBuilder: FormBuilder, private authService: AuthService, private router: Router) {}

  ngOnInit() {
    this.usernameFormGroup = this._formBuilder.group({
      username: ['', Validators.minLength(4)]
    });
    this.infoFormGroup = this._formBuilder.group({
      email: ['', [Validators.required, Validators.email]],
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      dateOfBirth: ['', Validators.required]
    });
    this.passwordFormGroup = this._formBuilder.group({
      password: ['', [Validators.required, Validators.min(6), Validators.max(21), Validators.pattern('[a-zA-Z0-9\!\.\&\%\$\#\@\^\*\,]+')]],
      confirmedPassword: ['', [Validators.required, Validators.min(6), Validators.max(21), Validators.pattern('[a-zA-Z0-9\!\.\&\%\$\#\@\^\*\,]+')]]
    }, { validator: this.EqualityValidator });
  }

  EqualityValidator = (control: FormGroup) => {
    const val = control.value;
    let condition = val.password == val.confirmedPassword;
    if (!condition) {
      return { 'differentPasswords': true }
    }
    return null;
  }

  get dateOfBirth() {
    if (this.userModel.dateOfBirth) {
      return this.userModel.dateOfBirth.format('MM/DD/YYYY');
    }
    return '';
  } 

  confirmPassword = () => {
    this.passwordFormClicked = true;
  }

  loadInformation = () => {
    this.userModel.username = this.usernameFormGroup.controls['username'].value;
    this.userModel.firstName = this.infoFormGroup.controls['firstName'].value;
    this.userModel.lastName = this.infoFormGroup.controls['lastName'].value;
    this.userModel.email = this.infoFormGroup.controls['email'].value;
    this.userModel.dateOfBirth = this.infoFormGroup.controls['dateOfBirth'].value;
    this.userModel.password = this.passwordFormGroup.controls['password'].value;
  }

  selectionChange = (event: StepperSelectionEvent) => {
    if (event.selectedIndex == 0) {
      this.disableError();
    }
    if (event.selectedIndex == 3) {
      this.loadInformation();
    }
  }

  register = async () => {
    let result = await this.authService.register(this.userModel);
    if (result == Action.Success) {
      this.router.navigateByUrl("/tours/login")
    } else {
      this.showError = true;
    }
  }

  disableError = () => {
    this.showError = false;
  }
}
