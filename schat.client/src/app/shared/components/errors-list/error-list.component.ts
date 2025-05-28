import {Component, computed, input} from '@angular/core';
import {Errors} from '../../../core/models/errors.model';

@Component({
  selector: 'app-errors-list',
  imports: [],
  templateUrl: './error-list.component.html',
  styleUrl: './error-list.component.css'
})
export class ErrorsListComponent {
  readonly errorList= input<Errors>();

  readonly errors = computed(() => {
    const curErrors = this.errorList();

    return curErrors ? Object.keys(curErrors.errors || {}).map(
      (key) => `${curErrors.errors[key]}`,
    ) : [];
  })
}
