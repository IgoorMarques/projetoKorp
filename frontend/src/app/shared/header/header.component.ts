import { Component } from '@angular/core';
import {MatToolbarModule} from '@angular/material/toolbar';
import {MatButtonModule} from '@angular/material/button';
import { Router, RouterModule } from '@angular/router';
import { UsuarioService } from '../../core/services/autenticacao/usuario/usuario.service';
import { CommonModule } from '@angular/common';
import { Observable } from 'rxjs';
import { LogoComponent } from "./logo/logo.component";
import { DecodedJwtToken } from '../../core/services/autenticacao/usuario/models/decodedJwtToken';

@Component({
  selector: 'app-header',
  imports: [MatToolbarModule, MatButtonModule, RouterModule, CommonModule, LogoComponent],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss'
})
export class HeaderComponent {

  user$: Observable<DecodedJwtToken>;

  constructor(private userService: UsuarioService, private router: Router) {
    this.user$ = this.userService.retornaUsuario();
  }

  logout() {
    // Chama o método de logout do serviço e navega para a página de login
    this.userService.logout();
    this.router.navigate(['login']);
  }

  estaLogado(): boolean {
    // Retorna o resultado do método estaLogado do serviço
    return this.userService.estaLogado();
  }
}

