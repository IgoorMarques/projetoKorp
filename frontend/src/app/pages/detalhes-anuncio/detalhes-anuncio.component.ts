import { Component } from '@angular/core';
import {MatGridListModule} from '@angular/material/grid-list';
import {MatTableModule} from '@angular/material/table';

export interface Tile {
  color: string;
  cols: number;
  rows: number;
  text: string;
}

export interface PeriodicElement {
  tamanho: string;
  idade: string;
  especie: string;
}

const ELEMENT_DATA: PeriodicElement[] = [
  { tamanho: 'Grande', idade: '2 anos', especie: 'Canina' }
];

@Component({
  selector: 'app-detalhes-anuncio',
  imports: [MatGridListModule, MatTableModule],
  templateUrl: './detalhes-anuncio.component.html',
  styleUrl: './detalhes-anuncio.component.scss'
})
export class DetalhesAnuncioComponent {

  displayedColumns: string[] = ['tamanho', 'idade', 'especie'];
  columnsToDisplay: string[] = [...this.displayedColumns];
  data: PeriodicElement[] = ELEMENT_DATA;

  tiles: Tile[] = [
    {text: 'Foto Principal', cols: 5, rows: 4, color: 'lightblue'},
    {text: 'Foto 2', cols: 1, rows: 1, color: 'lightgreen'},
    {text: 'Foto 3', cols: 1, rows: 1, color: 'lightpink'},
    {text: 'Foto 4', cols: 1, rows: 1, color: '#DDBDF1'},
    {text: 'Foto ', cols: 1, rows: 1, color: '#DDBDF1'},
  ];
}
