import {Component, input} from '@angular/core';

export type ButtonType = "button" | "submit" | "reset";

@Component({
  selector: 'app-button',
  imports: [],
  templateUrl: './button.component.html',
  styleUrl: './button.component.css'
})
export class ButtonComponent {
  disabled = input<boolean>(false);
  type =  input<ButtonType>('button');
}
