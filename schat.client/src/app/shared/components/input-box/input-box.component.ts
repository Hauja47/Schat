import {Component, input} from '@angular/core';
import {TextInputType} from '../../../core/enum/text-input-type';

@Component({
  selector: 'app-input-box',
  imports: [],
  templateUrl: './input-box.component.html',
  styleUrl: './input-box.component.css'
})
export class InputBoxComponent {
  placeholder = input<string>('');
  type = input<TextInputType>(TextInputType.text);
  name = input<string>('');
  required = input<boolean>(false)
}
