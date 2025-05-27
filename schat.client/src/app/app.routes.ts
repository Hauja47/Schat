import { Routes } from '@angular/router';
import {SignInComponent} from './features/auth/pages/sign-in/sign-in.component';

export const routes: Routes = [
  {
    path: 'auth',
    children: [
      {
        title: "Login",
        path: 'sign-in',
        component: SignInComponent
      }
    ]
  }
];
