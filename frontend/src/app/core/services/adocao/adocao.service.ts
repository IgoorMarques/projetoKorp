import { Injectable } from '@angular/core';

import { HttpClient, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Anuncio } from './models/adocao';
import { environment } from '../../../../environments/environment.development';
import { CriarAnuncio } from './models/criarAnuncio';

@Injectable({
  providedIn: 'root'
})
export class AnuncioService {

  private apiUrl : string = environment.apiUrl;

  constructor(private httpClient: HttpClient) {
  }


  getAnuncios(): Observable<Anuncio[]> {
    return this.httpClient.get<Anuncio[]>(`${this.apiUrl}/AnuncioAnimal`);
  }

  getByAnuncios(anuncioId:number): Observable<Anuncio> {
    return this.httpClient.get<Anuncio>(`${this.apiUrl}/AnuncioAnimal/${anuncioId}`);
  }

  createAnuncio(ad: CriarAnuncio): Observable<any> {
    const formData = new FormData();
    formData.append('titulo', ad.titulo);
    formData.append('descricao', ad.descricao);
    formData.append('nomeAnimal', ad.nomeAnimal);
    formData.append('tamanho', ad.tamanho);
    formData.append('idade', ad.idade.toString());
    formData.append('especie', ad.especie);
    formData.append('anuncianteId', ad.anuncianteId);

  ad.imagens.forEach((file, index) => {
    console.log(`Adicionando arquivo ${index}:`, file);

    formData.append('imagens', file, file.name);
  });

  formData.forEach((value, key) => {
    console.log(key, value);
  });
    return this.httpClient.post<any>(`${this.apiUrl}/AnuncioAnimal`, formData);
  }
}
