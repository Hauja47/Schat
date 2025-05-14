import { Component } from '@angular/core';
import {NgIcon, provideIcons} from '@ng-icons/core';
import { faCalendar } from '@ng-icons/font-awesome/regular';

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
    provideIcons({faCalendar})
  ]
})
export class SignInComponent {
}
