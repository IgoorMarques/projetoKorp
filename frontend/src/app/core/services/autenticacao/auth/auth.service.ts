import { Injectable } from '@angular/core';
import { UsuarioService } from '../usuario/usuario.service';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { Observable, tap } from 'rxjs';
import { environment } from '../../../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl : string = environment.apiUrl;

  constructor(private usuarioService: UsuarioService, private httpClient: HttpClient) {
  }

  autenticar(email: string, senha: string) : Observable<HttpResponse<any>>{
    return this.httpClient.post(`${this.apiUrl}/Auth/Login`,
    {
      username: email,
      password: senha
    },
    {observe: 'response'}
    ).pipe(
      tap(
        (response)=>{
          console.log(response.body)
          const authToken = response.body['token'] ?? ''
          this.usuarioService.salvaToken(authToken);
        }
      )
    )
  }
}
