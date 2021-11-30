import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import * as shajs from 'sha.js';
import { AccountService, ILoginDto, LoginDto } from 'src/app/api/app.generated';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  public errorAlert: boolean = false;
  public serverAlert: boolean = false;

  public formModel: {
    username: string;
    password: string;
  } = { username: "", password: "" };

  constructor(private accountService: AccountService, private router: Router) { }

  ngOnInit(): void {
  }

  private resetAlerts() {
    this.errorAlert = false;
    this.serverAlert = false;
  }

  public onClickLogin() {
    this.resetAlerts();
    this.login();
  }

  private getLoginDto(salt: string) {
    const saltedPassword: string = salt + this.formModel.password;
    const hashedSaltedPassword: string = shajs('sha256').update(saltedPassword).digest('hex');

    const iLoginDto: ILoginDto = {
      userName: this.formModel.username,
      passwordHash: hashedSaltedPassword,
    };
    return new LoginDto(iLoginDto);
  }

  private login() {
    this.accountService.getSaltForUser(this.formModel.username).subscribe(
      //Salt
      (response) => {
        let loginDto = this.getLoginDto(response);
        this.accountService.login(loginDto).subscribe(
          (response) => {
            localStorage.setItem("username", this.formModel.username);
            localStorage.setItem("token", response);
            this.router.navigate(['browser']);
          },
          (err) => {
            switch (err.status) {
              case 400:
                this.errorAlert = true;
                break;
              default:
                this.serverAlert = true;
            }
          }
        );
      },
      //Error handling by alerts
      (err) => {
        switch (err.status) {
          case 404:
            this.errorAlert = true;
            break;
          default:
            this.serverAlert = true;
        }
      }
    );
  }

}
