import {AbstractControl, FormGroup} from '@angular/forms';

export function getControlName(control: AbstractControl): string | undefined {
  if (control.parent instanceof FormGroup) {
    const parentFormGroup = control.parent;
    return Object.keys(parentFormGroup.controls).find(
      key => parentFormGroup.get(key) === control
    );
  }

  return undefined;
}
