import { Component, OnInit } from "@angular/core";
import { AuthService } from "src/app/shared-module/services/auth.service";
import { Router } from "@angular/router";
import { FormGroup, FormBuilder, Validators } from "@angular/forms";
import { LoginViewModel } from "src/app/shared-module/services/artour.api.service";
import { Action } from "src/app/shared-module/enums/action.enum";
import { MailService } from "../services/mail.service";
import { MatSnackBar } from "@angular/material";

@Component({
  selector: "app-login",
  templateUrl: "./login.component.html",
  styleUrls: ["./login.component.scss"]
})
export class LoginComponent implements OnInit {
  loginFormGroup: FormGroup;
  loginModel: LoginViewModel = new LoginViewModel();
  resetMode: boolean = false;
  resetEmail: string = "";

  constructor(
    private authService: AuthService,
    private router: Router,
    private formBuilder: FormBuilder,
    private mailService: MailService,
    private snackBar: MatSnackBar
  ) {
    this.loginModel.remember = false;
  }

  ngOnInit() {
    this.loginFormGroup = this.formBuilder.group({
      login: ["", Validators.required],
      password: ["", Validators.required],
      remember: [false]
    });
  }

  login = async () => {
    if (this.loginFormGroup.valid) {
      this.loginModel.login = this.loginFormGroup.controls["login"].value;
      this.loginModel.password = this.loginFormGroup.controls["password"].value;
      this.loginModel.remember = this.loginFormGroup.controls["remember"].value;
      let result = await this.authService.login(this.loginModel);
      if (result == Action.Success) {
        this.router.navigateByUrl("/tours/home");
      }
    }
  };

  toggleResetPassword = () => {
    this.resetMode = !this.resetMode;
  };

  resetPassword = () => {
    this.mailService.sendResetLink(this.resetEmail).subscribe(
      result => {
        this.resetMode = false;
        this.snackBar.open("Email has been sent to your inbox!", "Close", { duration: 5000 })
      }
    )
  };
}
