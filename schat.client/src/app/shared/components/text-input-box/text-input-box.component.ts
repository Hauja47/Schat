import {Component, input} from '@angular/core';
import {
  ControlValueAccessor,
  NG_VALUE_ACCESSOR,
} from '@angular/forms';

export type InputBoxType = "text" | "password";

@Component({
  selector: 'app-text-input-box',
  imports: [],
  templateUrl: './text-input-box.component.html',
  styleUrl: './text-input-box.component.css',
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      multi: true,
      useExisting: TextInputBoxComponent,
    }
  ]
})
export class TextInputBoxComponent implements ControlValueAccessor {
  placeholder = input<string>('');
  type = input<InputBoxType>("text");
  name = input<string>('');
  required = input<boolean>(false);
  id = input<string>('');

  value: any = '';
  isDisabled = false;
  touched = false;

  onChange: (value: any) => void = () => {};
  onTouched: () => void = () => {};

  writeValue(value: any): void {
    this.value = value;
  }

  registerOnChange(fn: any): void {
    this.onChange = fn;
  }

  registerOnTouched(fn: any): void {
    this.onTouched = fn;
  }

  setDisabledState?(isDisabled: boolean): void {
    this.isDisabled = isDisabled;
  }

  onInput(event: Event) {
    this.markAsTouched();

    const value = (event.target as HTMLInputElement).value;
    this.value = value;
    this.onChange(value);
  }

  markAsTouched() {
    if (!this.touched) {
      this.onTouched()
      this.touched = true;
    }
  }
}
