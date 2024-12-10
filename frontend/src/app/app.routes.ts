import { Routes } from '@angular/router';
import { LoginComponent } from './pages/login/login.component';
import { CriarContaComponent } from './pages/criar-conta/criar-conta.component';
import { DetalhesAnuncioComponent } from './pages/detalhes-anuncio/detalhes-anuncio.component';
import { HomeComponent } from './pages/home/home.component';
import { CriarAnuncioComponent } from './pages/anuncio/criar-anuncio/criar-anuncio.component';

export const routes: Routes = [
  {
    path: '',
    component: HomeComponent
  },
  {
    path: 'create-account',
    component: CriarContaComponent
  },
  {
    path: 'login',
    component: LoginComponent
  },
  {
    path: 'details',
    component: DetalhesAnuncioComponent
  },
  {
    path: 'create-anuncio',
    component: CriarAnuncioComponent
  }
];
