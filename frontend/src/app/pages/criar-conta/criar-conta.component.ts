import { ChangeDetectionStrategy, Component, signal } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { FormControl, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import {MatFormFieldModule} from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { merge } from 'rxjs';
import {MatCheckboxModule} from '@angular/material/checkbox';

@Component({
  selector: 'app-criar-conta',
  standalone: true,
  imports: [MatFormFieldModule, MatInputModule, MatIconModule, ReactiveFormsModule, MatButtonModule, MatCheckboxModule],
  templateUrl: './criar-conta.component.html',
  styleUrls: ['./criar-conta.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class CriarContaComponent {

  readonly email = new FormControl('', [Validators.required, Validators.email]);
  errorMessage = signal('');
  hide = signal(true);
  constructor() {
    merge(this.email.statusChanges, this.email.valueChanges)
      .pipe(takeUntilDestroyed())
      .subscribe(() => this.updateErrorMessage());
  }

  updateErrorMessage() {
    if (this.email.hasError('required')) {
      this.errorMessage.set('Informe seu email');
    } else if (this.email.hasError('email')) {
      this.errorMessage.set('Email inválido');
    } else {
      this.errorMessage.set('');
    }
  }
}
