import { Injectable } from '@angular/core';

import { HttpClient, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Anuncio } from './adocao';
import { environment } from '../../../../environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class AdocaoService {

  private apiUrl : string = environment.apiUrl;

  constructor(private httpClient: HttpClient) {
  }


  getAnuncios(): Observable<Anuncio[]> {
    return this.httpClient.get<Anuncio[]>(`${this.apiUrl}/AnuncioAnimal`);
  }

  getByAnuncios(anuncioId:number): Observable<Anuncio> {
    return this.httpClient.get<Anuncio>(`${this.apiUrl}/AnuncioAnimal/${anuncioId}`);
  }
}
