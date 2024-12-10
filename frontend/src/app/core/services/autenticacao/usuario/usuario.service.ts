import { Injectable } from '@angular/core';
import { TokenService } from '../../token/token.service';
import { BehaviorSubject } from 'rxjs';
import { Usuario } from './usuario';
import { jwtDecode } from "jwt-decode";



@Injectable({
  providedIn: 'root'
})
export class UsuarioService {

  private usuarioSubject = new BehaviorSubject<Usuario>({});
  constructor(private tokenService: TokenService) {
  }

  private decodificaJWT(){
    const token = this.tokenService.getToken();
    if(token != null){
      const usuario = jwtDecode(token) as Usuario;
      this.usuarioSubject.next(usuario);
    }
    this.usuarioSubject.next({});
  }

  retornaUsuario(){
    return this.usuarioSubject.asObservable();
  }

  salvaToken(token: string){
    this.tokenService.setToken(token);
    this.decodificaJWT();
  }

  logout(){
    this.tokenService.removeToken();
    this.usuarioSubject.next({});
  }

  estaLogado(){
    return this.tokenService.hasToken();
  }

}
