import {Component, signal, WritableSignal} from '@angular/core';
import {LayoutComponent} from '../../components/layout/layout.component';
import {TextInputBoxComponent} from '../../../../shared/components/text-input-box/text-input-box.component';
import {ErrorsListComponent} from '../../../../shared/components/errors-list/error-list.component';
import {Errors} from '../../../../core/models/errors.model';
import {ButtonComponent} from '../../../../shared/components/button/button.component';
import {RouterLink, RouterLinkActive} from '@angular/router';
import {OrSeparatorComponent} from '../../components/or-separator/or-separator.component';
import {OauthBtnGroupComponent} from '../../components/oauth-btn-group/oauth-btn-group.component';
import {AbstractControl, FormControl, FormGroup, ReactiveFormsModule, Validators} from '@angular/forms';
import {getControlName, toError} from '../../../../core/utils';
import * as fp from 'lodash/fp';

@Component({
  selector: 'app-sign-up',
  imports: [
    LayoutComponent,
    TextInputBoxComponent,
    ErrorsListComponent,
    ButtonComponent,
    RouterLink,
    RouterLinkActive,
    OrSeparatorComponent,
    OauthBtnGroupComponent,
    ReactiveFormsModule
  ],
  templateUrl: './sign-up.component.html',
  styleUrl: './sign-up.component.css'
})
export class SignUpComponent {
  emailErrors = signal<Errors | undefined>({
    errors: {}
  });
  usernameErrors = signal<Errors | undefined>({
    errors: {}
  });
  passwordErrors = signal<Errors | undefined>({
    errors: {}
  });
  confirmPasswordErrors = signal<Errors | undefined>({
    errors: {}
  });

  signUpForm = new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email]),
    username: new FormControl('', [Validators.required]),
    password: new FormControl('', [Validators.required]),
    confirmPassword: new FormControl('', [Validators.required]),
  })

  /* region Get FormControls */
  get email() {
    return this.signUpForm.get('email');
  }

  get username() {
    return this.signUpForm.get('username');
  }

  get password() {
    return this.signUpForm.get('password');
  }

  get confirmPassword() {
    return this.signUpForm.get('confirmPassword');
  }

  /* endregion */

  onEmailInput() {
    this.setErrors(this.email!, this.emailErrors);
  }

  onUsernameInput() {
    this.setErrors(this.username!, this.usernameErrors);
  }

  onPasswordInput() {
    this.setErrors(this.password!, this.passwordErrors);
  }

  onConfirmPasswordInput() {
    this.setErrors(this.confirmPassword!, this.confirmPasswordErrors);
  }

  setErrors(
    control: AbstractControl,
    errors: WritableSignal<Errors | undefined>,
  ) {
    const controlName = fp.startCase(getControlName(control)!);
    errors.set(toError(control.errors, controlName))
  }
}
