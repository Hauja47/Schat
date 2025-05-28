import { Component } from '@angular/core';
import {LayoutComponent} from '../../components/layout/layout.component';
import {TextInputBoxComponent} from '../../../../shared/components/text-input-box/text-input-box.component';
import {ErrorsListComponent} from '../../../../shared/components/errors-list/error-list.component';
import {Errors} from '../../../../core/models/errors.model';
import {ButtonComponent} from '../../../../shared/components/button/button.component';
import {RouterLink, RouterLinkActive} from '@angular/router';
import {OrSeparatorComponent} from '../../components/or-separator/or-separator.component';
import {OauthBtnGroupComponent} from '../../components/oauth-btn-group/oauth-btn-group.component';

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
    OauthBtnGroupComponent
  ],
  templateUrl: './sign-up.component.html',
  styleUrl: './sign-up.component.css'
})
export class SignUpComponent {
  emailErrors: Errors | undefined;
  usernameErrors: Errors | undefined;
  confirmPasswordErrors: Errors | undefined;
  passwordErrors: Errors | undefined;
}
