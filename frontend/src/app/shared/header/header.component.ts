import { Component } from '@angular/core';
import {MatToolbarModule} from '@angular/material/toolbar';
import {MatButtonModule} from '@angular/material/button';
import { Router, RouterModule } from '@angular/router';
import { UsuarioService } from '../../core/services/autenticacao/usuario/usuario.service';
import { CommonModule } from '@angular/common';
import { Usuario } from '../../core/services/autenticacao/usuario/usuario';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-header',
  imports: [MatToolbarModule, MatButtonModule, RouterModule, CommonModule],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss'
})
export class HeaderComponent {

  user$: Observable<Usuario>;

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

