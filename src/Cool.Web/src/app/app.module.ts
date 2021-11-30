import { HttpClientModule } from '@angular/common/http';
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

@NgModule({
  declarations: [
    AppComponent,
    MenuComponent,
    LoginComponent,
    RegistrationComponent,
    BrowserComponent,
    UploaderComponent,
    CaffCardComponent,
    ProfileComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FontAwesomeModule
  ],
  providers: [
    {
      provide: API_BASE_URL,
      useValue: environment.apiRoot
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
