import {Component, input} from '@angular/core';

export type InputBoxType = "text" | "password";

@Component({
  selector: 'app-text-input-box',
  imports: [],
  templateUrl: './text-input-box.component.html',
  styleUrl: './text-input-box.component.css'
})
export class TextInputBoxComponent {
  placeholder = input<string>('');
  type = input<InputBoxType>("text");
  name = input<string>('');
  required = input<boolean>(false)
}
