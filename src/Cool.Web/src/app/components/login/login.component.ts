import { Component } from '@angular/core';
import { Router } from '@angular/router';
import shajs from 'sha.js';
import { AccountService, ILoginDto, LoginDto } from 'src/app/api/app.generated';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {
  public errorAlert: boolean = false;
  public serverAlert: boolean = false;

  public formModel: {
    username: string;
    password: string;
  } = { username: "", password: "" };

  constructor(private accountService: AccountService, private router: Router) { }

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
      (saltResponse) => {
        let loginDto = this.getLoginDto(saltResponse);
        this.accountService.login(loginDto).subscribe(
          (tokenResponse) => {
            localStorage.setItem("username", this.formModel.username);
            localStorage.setItem("token", tokenResponse);
            this.router.navigate(['browser']);
          },
          (err) => {
            if (err.status == 400) {
              this.errorAlert = true;
            }
            else {
              this.serverAlert = true;
            }
          }
        );
      },
      //Error handling by alerts
      (err) => {
        if (err.status == 404) {
          this.errorAlert = true;
        }
        else {
          this.serverAlert = true;
        }
      }
    );
  }

}
