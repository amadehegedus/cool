import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BrowserComponent } from "./components/browser/browser.component";
import { UploaderComponent } from "./components/uploader/uploader.component";
import { ProfileComponent } from "./components/profile/profile.component";
import { RegistrationComponent } from './components/registration/registration.component';
import { LoginComponent } from './components/login/login.component';
import { RoleGuardService } from './services/role-guard.service';

const routes: Routes = [
  {
    path: 'registration',
    component: RegistrationComponent,
  },
  {
    path: 'login',
    component: LoginComponent,
  },
  {
    path: 'browser',
    component: BrowserComponent,
    canActivate: [RoleGuardService],
  },
  {
    path: 'upload',
    component: UploaderComponent,
    canActivate: [RoleGuardService],
  },
  {
    path: 'profile',
    component: ProfileComponent,
    canActivate: [RoleGuardService],
  },
  {
    path: '**',
    redirectTo: 'browser',
    pathMatch: 'full',
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
