import {ValidationErrors} from '@angular/forms';
import {Errors} from '../models/errors.model';

export type ErrorTypes =
  | 'required'
  | 'email'
  | 'minlength'
  | 'invalidDate'
  | 'invalidYear';

export const ERROR_MESSAGES: { [key: string]: (...args: any) => string } = {
  required: (formControlName: string) => `${formControlName} is required.`,
  email: () => `This is not a valid email address.`
};

export function toError(validationErrors: ValidationErrors | null | undefined, ...args: any): Errors {
  let errors: Errors = {
    errors: {}
  };

  if (validationErrors != null) {
    Object.keys(validationErrors).forEach(key => {
      errors.errors[key] = ERROR_MESSAGES[key](args);
    })
  }

  return errors;
}

