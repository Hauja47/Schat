import {Component, signal} from '@angular/core';
import {FormControl, FormGroup, ReactiveFormsModule, Validators} from '@angular/forms';
import {LayoutComponent} from '../../components/layout/layout.component';
import {TextInputBoxComponent} from '../../../../shared/components/text-input-box/text-input-box.component';
import {ButtonComponent} from '../../../../shared/components/button/button.component';
import {ErrorsListComponent} from '../../../../shared/components/errors-list/error-list.component';
import {Errors} from '../../../../core/models/errors.model';
import {toError} from '../../../../core/utils/to-error';

@Component({
  selector: 'app-forgot-password',
  imports: [
    LayoutComponent,
    TextInputBoxComponent,
    ButtonComponent,
    ErrorsListComponent,
    ReactiveFormsModule
  ],
  templateUrl: './forgot-password.component.html',
  styleUrl: './forgot-password.component.css'
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

  get email() {
    return this.forgotPasswordForm.get('email');
  }

  onEmailInput(event: Event) {
    this.emailErrors.set(toError(this.email?.errors));

    console.error("error: ", this.email?.errors);
    console.info(this.forgotPasswordForm.value.email);
  }
}
