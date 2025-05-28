import { Component } from '@angular/core';
import {RouterLink, RouterLinkActive} from '@angular/router';
import {NgIcon, provideIcons} from '@ng-icons/core';
import { faBrandGoogle, faBrandFacebook, faBrandGithub } from '@ng-icons/font-awesome/brands';

import {TextInputBoxComponent} from '../../../../shared/components/text-input-box/text-input-box.component';
import {ButtonComponent} from '../../../../shared/components/button/button.component';
import {LayoutComponent} from '../../components/layout/layout.component';
import {OrSeparatorComponent} from '../../components/or-separator/or-separator.component';
import {OauthBtnGroupComponent} from '../../components/oauth-btn-group/oauth-btn-group.component';

@Component({
  selector: 'app-sign-in',
  imports: [
    TextInputBoxComponent,
    ButtonComponent,
    LayoutComponent,
    RouterLink,
    RouterLinkActive,
    OrSeparatorComponent,
    OauthBtnGroupComponent
  ],
  templateUrl: './sign-in.component.html',
  styleUrl: './sign-in.component.css',
  providers: []
})
export class SignInComponent {
}
