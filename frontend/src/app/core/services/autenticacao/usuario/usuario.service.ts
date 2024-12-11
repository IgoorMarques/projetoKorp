import { Injectable } from '@angular/core';
import { TokenService } from '../../token/token.service';
import { BehaviorSubject } from 'rxjs';
import { jwtDecode } from "jwt-decode";
import { DecodedJwtToken } from './models/decodedJwtToken';



@Injectable({
  providedIn: 'root'
})
export class UsuarioService {


  private usuarioSubject = new BehaviorSubject<DecodedJwtToken>({});
  constructor(private tokenService: TokenService) {
  }

  private decodificaJWT() {
    const token = this.tokenService.getToken();
    if (token) {
      const usuario = jwtDecode(token) as DecodedJwtToken;
      this.usuarioSubject.next(usuario);
    } else {
      this.usuarioSubject.next({});
    }
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

  getIdUserFromToken(): string | null {
    const token = this.tokenService.getToken();
    if (token) {
      try {
        const decoded = jwtDecode<DecodedJwtToken>(token);
        return decoded.aud!;
      } catch (error) {
        console.error('Error decoding token:', error);
      }
    }
    return null; // Retorna null se não houver token ou se houver erro
  }

}
