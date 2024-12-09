import { Routes } from '@angular/router';
import { LoginComponent } from './pages/login/login.component';
import { CriarContaComponent } from './pages/criar-conta/criar-conta.component';
import { DetalhesAnuncioComponent } from './pages/detalhes-anuncio/detalhes-anuncio.component';

export const routes: Routes = [
  {
    path: '',
    component: DetalhesAnuncioComponent
  }
];
