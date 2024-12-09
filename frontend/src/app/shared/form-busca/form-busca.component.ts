import { Component } from '@angular/core';
import {MatButtonToggleModule} from '@angular/material/button-toggle';
import {MatIconModule} from '@angular/material/icon';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatChipsModule} from '@angular/material/chips';
import { MatButtonModule } from '@angular/material/button';
import {MatCardModule} from '@angular/material/card';

@Component({
  selector: 'app-form-busca',
  imports: [MatButtonToggleModule, MatIconModule, MatFormFieldModule, MatChipsModule, MatButtonModule, MatCardModule],
  templateUrl: './form-busca.component.html',
  styleUrl: './form-busca.component.scss'
})
export class FormBuscaComponent {

}
