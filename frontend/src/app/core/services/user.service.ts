import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { UserModel } from '../types/types';
import { TokenService } from './token/token.service';



@Injectable({
  providedIn: 'root'
})
export class UserService {

  private apiUrl : string = environment.apiUrl

  constructor(
    private httpClient : HttpClient,
    private tokenService: TokenService
  ) { }


  register(novoUser: UserModel): Observable<object> {
    return this.httpClient.post<object>(`${this.apiUrl}/Usuarios`, novoUser);
  }

}
