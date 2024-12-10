import { Component, Input } from '@angular/core';
import {MatButtonModule} from '@angular/material/button';
import {MatCardModule} from '@angular/material/card';
import { Anuncio } from '../../core/services/adocao/adocao';
import { Router } from '@angular/router';

@Component({
  selector: 'app-card',
  imports: [MatCardModule, MatButtonModule],
  templateUrl: './card.component.html',
  styleUrl: './card.component.scss'
})
export class CardComponent {
  @Input() anuncio!: Anuncio;

  constructor(private router: Router){}


  irParaDetalhes(): void {
    this.router.navigate(['/details'], { queryParams: {"id": this.anuncio.anuncioId} });
  }

}
