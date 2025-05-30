import {Routes} from '@angular/router';
import {SignInComponent} from './features/auth/pages/sign-in/sign-in.component';
import {SignUpComponent} from './features/auth/pages/sign-up/sign-up.component';
import {ResetPasswordComponent} from './features/auth/pages/reset-password/reset-password.component';

export const routes: Routes = [
  {
    path: 'auth',
    children: [
      {
        title: "Login",
        path: 'sign-in',
        component: SignInComponent
      },
      {
        title: "Register",
        path: 'sign-up',
        component: SignUpComponent
      },
      {
        title: "Reset Password",
        path: 'reset-password',
        component: ResetPasswordComponent,
      }
    ]
  },
  {
    path: '**',
    redirectTo: '/auth/sign-in',
  },
];
