import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { environment } from 'src/environments/environment';
import { API_BASE_URL } from './api/app.generated';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { MenuComponent } from './components/menu/menu.component';
import { LoginComponent } from './components/login/login.component';
import { RegistrationComponent } from './components/registration/registration.component';
import { BrowserComponent } from './components/browser/browser.component';
import { UploaderComponent } from './components/uploader/uploader.component';
import { CaffCardComponent } from './components/caff-card/caff-card.component';
import { ProfileComponent } from './components/profile/profile.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import { TokenInterceptorService } from './interceptors/token-interceptor.service';
import { TagInputModule } from 'ngx-chips';
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { CaffModalComponent } from './components/caff-modal/caff-modal.component';
import { CaffHolderComponent } from './components/caff-holder/caff-holder.component';
import { CommonModule } from '@angular/common';

@NgModule({
  declarations: [
    AppComponent,
    MenuComponent,
    LoginComponent,
    RegistrationComponent,
    BrowserComponent,
    UploaderComponent,
    CaffCardComponent,
    ProfileComponent,
    CaffModalComponent,
    CaffHolderComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    HttpClientModule,
    FontAwesomeModule,
    FormsModule,
    ReactiveFormsModule,
    TagInputModule
  ],
  providers: [
    {
      provide: API_BASE_URL,
      useValue: environment.apiRoot
    },
    { provide: HTTP_INTERCEPTORS, useClass: TokenInterceptorService, multi: true },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
