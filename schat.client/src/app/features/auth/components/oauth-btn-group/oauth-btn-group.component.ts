import { Component } from '@angular/core';
import {ButtonComponent} from '../../../../shared/components/button/button.component';
import {NgIcon, provideIcons} from '@ng-icons/core';
import {faBrandFacebook, faBrandGithub, faBrandGoogle} from '@ng-icons/font-awesome/brands';

@Component({
  selector: 'app-oauth-btn-group',
  imports: [
    ButtonComponent,
    NgIcon
  ],
  templateUrl: './oauth-btn-group.component.html',
  styleUrl: './oauth-btn-group.component.css',
  providers: [
    provideIcons({faBrandGoogle, faBrandFacebook, faBrandGithub})
  ]
})
export class OauthBtnGroupComponent {

}
