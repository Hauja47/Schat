import {Component, signal} from '@angular/core';
import {FormControl, FormGroup, ReactiveFormsModule, Validators} from '@angular/forms';
import {LayoutComponent} from '../../components/layout/layout.component';
import {TextInputBoxComponent} from '../../../../shared/components/text-input-box/text-input-box.component';
import {ButtonComponent} from '../../../../shared/components/button/button.component';
import {ErrorsListComponent} from '../../../../shared/components/errors-list/error-list.component';
import {Errors} from '../../../../core/models/errors.model';
import {toError, getControlName} from '../../../../core/utils/';
import {NgIcon, provideIcons} from '@ng-icons/core';
import {faSolidChevronLeft} from '@ng-icons/font-awesome/solid'
import {RouterLink, RouterLinkActive} from '@angular/router';
import * as fp from 'lodash/fp';

@Component({
  selector: 'app-forgot-password',
  imports: [
    LayoutComponent,
    TextInputBoxComponent,
    ButtonComponent,
    ErrorsListComponent,
    ReactiveFormsModule,
    NgIcon,
    RouterLink,
    RouterLinkActive
  ],
  templateUrl: './forgot-password.component.html',
  styleUrl: './forgot-password.component.css',
  providers: [
    provideIcons({faSolidChevronLeft})
  ]
})
export class ForgotPasswordComponent {
  emailErrors = signal<Errors | undefined>({
    errors: {}
  });

  forgotPasswordForm = new FormGroup({
    email: new FormControl('', [
      Validators.required,
      Validators.email
    ])
  })

  onSubmit() {

  }

  onEmailInput() {
    const emailFormControlName = fp.startCase(getControlName(this.email!)!);

    this.emailErrors.set(toError(this.email?.errors, emailFormControlName))
  }

  get email() {
    return this.forgotPasswordForm.get('email');
  }
}
