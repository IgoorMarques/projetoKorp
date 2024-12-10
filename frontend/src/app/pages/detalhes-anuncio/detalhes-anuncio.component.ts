import { Component, OnInit } from '@angular/core';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatTableModule } from '@angular/material/table';
import { Anuncio } from '../../core/services/adocao/adocao';
import { ActivatedRoute } from '@angular/router';
import { AdocaoService } from '../../core/services/adocao/adocao.service';
import { CommonModule } from '@angular/common';
import { MatButton } from '@angular/material/button';
import { FormsModule } from '@angular/forms';
import {MatFormFieldModule} from '@angular/material/form-field';

export interface Tile {
  color: string;
  cols: number;
  rows: number;
  text: string;
}

export interface DetalhesAnimal {
  tamanho: string;
  idade: number;
  especie: string;
}

@Component({
  selector: 'app-detalhes-anuncio',
  standalone: true,
  imports: [MatGridListModule, MatTableModule, CommonModule, MatButton, FormsModule, MatFormFieldModule ],
  templateUrl: './detalhes-anuncio.component.html',
  styleUrls: ['./detalhes-anuncio.component.scss'] // Corrigido o nome da propriedade
})
export class DetalhesAnuncioComponent implements OnInit {
  displayedColumns: string[] = ['tamanho', 'idade', 'especie'];
  columnsToDisplay: string[] = [...this.displayedColumns];
  anuncio!: Anuncio;
  data: DetalhesAnimal[] = [];

  constructor(
    private route: ActivatedRoute,
    private anuncioService: AdocaoService
  ) {}

  chatAberto = false;
  novaMensagem = '';
  mensagens = [{'texto':''}];

  // Simula a abertura do chat
  abrirChat() {
    this.chatAberto = true;
  }

  // Simula o envio de uma mensagem
  enviarMensagem() {
    if (this.novaMensagem.trim() !== '') {

      this.novaMensagem = '';
    }
  }

  // Fecha o chat
  fecharChat() {
    this.chatAberto = false;
  }

  ngOnInit(): void {
    // Obtém os parâmetros da rota e carrega os dados do anúncio
    this.route.queryParams.subscribe((params) => {
      const anuncioId = params['id'];
      if (anuncioId) {
        this.anuncioService.getByAnuncios(anuncioId).subscribe((res) => {
          this.anuncio = res;
          this.data = [
            {
              tamanho: this.anuncio.tamanho || '',
              idade: this.anuncio.idade,
              especie: this.anuncio.especie || '',
            },
          ];
        });
      }
      console.log(this.anuncio);
    });
  }

  tiles: Tile[] = [
    { text: this.anuncio?.midias[0]?.urlMidia, cols: 5, rows: 4, color: 'lightblue' },
    { text: this.anuncio?.midias[0]?.urlMidia, cols: 1, rows: 1, color: 'lightgreen' },
    { text: this.anuncio?.midias[0]?.urlMidia, cols: 1, rows: 1, color: 'lightpink' },
    { text: this.anuncio?.midias[0]?.urlMidia, cols: 1, rows: 1, color: '#DDBDF1' },
    { text: this.anuncio?.midias[0]?.urlMidia , cols: 1, rows: 1, color: '#DDBDF1' },
  ];
}
