import { Component, OnInit } from '@angular/core';
import { AccountService, IRegisterDto, RegisterDto } from 'src/app/api/app.generated';
import * as shajs from 'sha.js';
import { Router } from '@angular/router';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.scss']
})
export class RegistrationComponent implements OnInit {
  public emailAlert: boolean = false;
  public usernameAlert: boolean = false;
  public passwordAlert: boolean = false;
  public emptyInputAlert: boolean = false;
  public shortPasswordAlert: boolean = false;
  public serverAlert: boolean = false;

  public formModel: {
    name: string;
    email: string;
    username: string;
    password: string;
    repassword: string;
  } = { name: "", email: "", username: "", password: "", repassword: "" };

  constructor(private accountService: AccountService, private router: Router) {
  }

  ngOnInit(): void {
    this.resetAlerts();
  }

  private resetAlerts() {
    this.emailAlert = false;
    this.usernameAlert = false;
    this.passwordAlert = false;
    this.emptyInputAlert = false;
    this.shortPasswordAlert = false;
    this.serverAlert = false;
  }

  private validateFormModel(): boolean {
    this.resetAlerts();
    if (this.formModel.name == "" ||
      this.formModel.email == "" ||
      this.formModel.username == "" ||
      this.formModel.password == "" ||
      this.formModel.repassword == "") {
      this.emptyInputAlert = true;
      return false;
    }
    if (this.formModel.password != this.formModel.repassword) {
      this.passwordAlert = true;
      return false;
    }
    if (this.formModel.password.length < 8) {
      this.shortPasswordAlert = true;
      return false;
    }
    return true;
  }

  public onClickRegistration() {
    if (!this.validateFormModel())
      return;
    this.registration();
  }

  private generateRandomSalt(): string {
    return Math.random().toString(36).substring(2);
  }

  private getRegisterDto() {
    const salt: string = this.generateRandomSalt();
    const saltedPassword: string = salt + this.formModel.password;
    const hashedSaltedPassword: string = shajs('sha256').update(saltedPassword).digest('hex');

    const iRegisterDto: IRegisterDto = {
      userName: this.formModel.username,
      passwordHash: hashedSaltedPassword,
      salt: salt,
      fullName: this.formModel.name,
      email: this.formModel.email,
    };
    return new RegisterDto(iRegisterDto);
  }

  private registration() {
    this.accountService.register(this.getRegisterDto()).subscribe(
      () => {
        this.router.navigate(['login']);
      },
      (err) => {
        switch (err.status) {
          case 400:
            this.usernameAlert = true;
            break;
          default:
            this.serverAlert = true;
        }
      }
    );
  }
}