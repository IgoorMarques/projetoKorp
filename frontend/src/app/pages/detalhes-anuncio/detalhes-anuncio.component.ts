import { UsuarioService } from './../../core/services/autenticacao/usuario/usuario.service';
import { Component, OnInit } from '@angular/core';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatTableModule } from '@angular/material/table';
import { Anuncio } from '../../core/services/adocao/models/adocao';
import { ActivatedRoute, Router } from '@angular/router';
import { AnuncioService } from '../../core/services/adocao/adocao.service';
import { CommonModule } from '@angular/common';
import { MatButton } from '@angular/material/button';
import { FormsModule } from '@angular/forms';
import {MatFormFieldModule} from '@angular/material/form-field';

export interface Imagens {
  color: string;
  cols: number;
  rows: number;
  url: string;
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
  anuncio!: Anuncio
  data: DetalhesAnimal[] = [];

  imgPrincipal: string = 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQxpVVQbKCREmtSdmiC6ghyZIkDHQZcDpRjUSEQmRZp1O8DYJyR5wGsGvG68qTll0g6NHM&usqp=CAU'

  constructor(
    private route: ActivatedRoute,
    private anuncioService: AnuncioService,
    private userService: UsuarioService,
    private router: Router
  ) {}

  chatAberto = false;
  novaMensagem = '';
  mensagens: string[] = [];
  indexImgSelecionada: number = 0;

  // Simula a abertura do chat
  abrirChat() {
    if(this.userService.estaLogado()){
      this.chatAberto = true;
    }else{
      this.router.navigate(['login'])
    }

  }

  // Simula o envio de uma mensagem
  enviarMensagem() {
    if (this.novaMensagem.trim() !== '') {
      this.mensagens.push(this.novaMensagem)
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
      this.carregarAnuncio(anuncioId);
    });
  }


  carregarAnuncio(anuncioId:number){
    this.anuncioService.getByAnuncios(anuncioId).subscribe((res) => {
      this.anuncio = res;
      if(this.anuncio.midias.length > 0){
        this.imgPrincipal = this.anuncio.midias[0].urlMidia
      }
    });
  }

  selecionarImagem(index: number): void {
    this.imgPrincipal = this.anuncio.midias[index].urlMidia;
    this.indexImgSelecionada = index;
  }

}
