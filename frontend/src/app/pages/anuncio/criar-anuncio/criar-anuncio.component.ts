import { UsuarioService } from './../../../core/services/autenticacao/usuario/usuario.service';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators, ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { CommonModule } from '@angular/common';
import Swal from 'sweetalert2';
import { AnuncioService } from '../../../core/services/adocao/adocao.service';
import { CriarAnuncio } from '../../../core/services/adocao/models/criarAnuncio';
import { Router } from '@angular/router';
import {MatProgressBarModule} from  '@angular/material/progress-bar' ;


@Component({
  selector: 'app-criar-anuncio',
  imports: [
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    ReactiveFormsModule,
    MatCardModule,
    MatButtonModule,
    CommonModule,
    MatIconModule,
    MatProgressBarModule
  ],
  templateUrl: './criar-anuncio.component.html',
  styleUrls: ['./criar-anuncio.component.scss']
})
export class CriarAnuncioComponent implements OnInit{
  adForm: FormGroup;
  imagens: File[] = [];
  imagePreviews: string[] = [];
  carregando: boolean = false;

  constructor(private fb: FormBuilder, private anuncioService: AnuncioService, private usuarioService:UsuarioService, private router: Router) {
    this.adForm = this.fb.group({
      titulo: ['', Validators.required],
      descricao: [''],
      nomeAnimal: ['', Validators.required],
      tamanho: ['', Validators.required],
      idade: [null, [Validators.required, Validators.pattern(/^\d+$/)]],
      especie: ['', Validators.required],
    });
  }
  ngOnInit(): void {
      console.log(this.usuarioService.getIdUserFromToken())
  }

  onSubmit() {
    this.carregando = true;
    if (this.adForm.valid) {
      console.log(this.carregando)
      var usuarioId = this.usuarioService.getIdUserFromToken()
        if(usuarioId){
        const adData: CriarAnuncio = {
          titulo: this.adForm.value.titulo,
          descricao: this.adForm.value.descricao,
          nomeAnimal: this.adForm.value.nomeAnimal,
          tamanho: this.adForm.value.tamanho,
          idade: this.adForm.value.idade,
          especie: this.adForm.value.especie,
          anuncianteId: usuarioId!,
          imagens: this.imagens
        };

        this.anuncioService.createAnuncio(adData).subscribe(()=>{
          Swal.fire({
            icon: 'success',
            title: 'Anuncio cadastrado com sucesso!',
              text: 'Seu anuncio já está disponível na plataforma',
            showConfirmButton: false,
            timer: 1000,
            timerProgressBar: true,
            backdrop: true,
            toast: true,
            position: 'top-end',
            }).then(() => {
              this.adForm.reset()
              this.imagePreviews = []
              this.imagens = []
            })
        },(error) => {
          Swal.fire({
            icon: 'error',
            title: 'Erro ao criar anuncio',
            text: error.error.message || 'Falha no servidor, tente novamente mais tarde.',
            showConfirmButton: true,
          });
        })

        }else{
          Swal.fire({
            icon: 'error',
            title: 'Sessão expirada',
            showConfirmButton: true,
          }).then(() => {
            this.router.navigate(['login']);
        })
      }
    this.carregando = false;

    }
  }

  removeImage(index: number): void {
    this.imagePreviews.splice(index, 1);
    this.imagens.splice(index, 1);
  }

  onImageSelect(event: Event): void {
    const input = event.target as HTMLInputElement;
    if (input.files) {
      for (let file of input.files) {
        this.imagens.push(file);

        const reader = new FileReader();
        reader.onload = (e) => {
          this.imagePreviews.push(e.target?.result as string);
        };
        reader.readAsDataURL(file);
      }
    }
  }
}
