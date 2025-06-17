import {ValidationErrors} from '@angular/forms';
import {Errors} from '../models/errors.model';
import {ERROR_MESSAGES} from '../constant/error-message';

export type ErrorTypes =
  | 'required'
  | 'email'
  | 'minlength'
  | 'invalidDate'
  | 'invalidYear';

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

