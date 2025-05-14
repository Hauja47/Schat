import { Component } from '@angular/core';
import {InputBoxComponent} from '../../../../shared/components/input-box/input-box.component';

@Component({
  selector: 'app-sign-in',
  imports: [
    InputBoxComponent
  ],
  templateUrl: './sign-in.component.html',
  styleUrl: './sign-in.component.css'
})
export class SignInComponent {

}
