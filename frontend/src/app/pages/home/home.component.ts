import { Component } from '@angular/core';
import { BannerComponent } from '../../shared/banner/banner.component';
import { ContainerComponent } from '../../shared/container/container.component';
import { CardComponent } from '../../shared/card/card.component';
import { FormBuscaComponent } from '../../shared/form-busca/form-busca.component';

@Component({
  selector: 'app-home',
  imports: [BannerComponent, ContainerComponent, CardComponent, FormBuscaComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent {

}
