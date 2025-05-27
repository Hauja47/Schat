import { Component } from '@angular/core';
import {NgIcon, provideIcons} from '@ng-icons/core';
import { faBrandGoogle, faBrandFacebook, faBrandGithub } from '@ng-icons/font-awesome/brands';

import {TextInputBoxComponent} from '../../../../shared/components/text-input-box/text-input-box.component';
import {ButtonComponent} from '../../../../shared/components/button/button.component';

@Component({
  selector: 'app-sign-in',
  imports: [
    TextInputBoxComponent,
    ButtonComponent,
    NgIcon
  ],
  templateUrl: './sign-in.component.html',
  styleUrl: './sign-in.component.css',
  providers: [
    provideIcons({faBrandGoogle, faBrandFacebook, faBrandGithub})
  ]
})
export class SignInComponent {
}
