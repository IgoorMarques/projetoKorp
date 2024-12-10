import { Anuncio } from './../../core/services/adocao/adocao';
import { Component, OnInit } from '@angular/core';
import { BannerComponent } from '../../shared/banner/banner.component';
import { ContainerComponent } from '../../shared/container/container.component';
import { CardComponent } from '../../shared/card/card.component';
import { FormBuscaComponent } from '../../shared/form-busca/form-busca.component';
import { AdocaoService } from '../../core/services/adocao/adocao.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-home',
  imports: [BannerComponent, ContainerComponent, CardComponent, FormBuscaComponent, CommonModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent implements OnInit {
  anuncios: Anuncio[] = [];

  constructor(private adocaoService: AdocaoService) {}

  ngOnInit(): void {
    this.adocaoService.getAnuncios().subscribe((res) => {
      this.anuncios = res;
    });
  }
}

