import {Component, computed, signal} from '@angular/core';
import {FormControl, FormGroup, ReactiveFormsModule, Validators} from '@angular/forms';
import {LayoutComponent} from '../../components/layout/layout.component';
import {TextInputBoxComponent} from '../../../../shared/components/text-input-box/text-input-box.component';
import {ButtonComponent} from '../../../../shared/components/button/button.component';
import {ErrorsListComponent} from '../../../../shared/components/errors-list/error-list.component';

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
  isEmailValid = signal<boolean>(false)
  isButtonDisabled = computed(() => !this.isEmailValid);

  forgotPasswordForm = new FormGroup({
    email: new FormControl('', [
      Validators.required,
      Validators.email
    ])
  })

  onSubmit() {
    console.error("error: ", this.email?.errors);
    console.info(this.forgotPasswordForm.value.email);
  }

  get email() {
    return this.forgotPasswordForm.get('email');
  }

  onInput(event: Event) {
    if (this.email?.value) {
      this.isEmailValid.set(!this.email.hasError('email'));
    }
  }
}
