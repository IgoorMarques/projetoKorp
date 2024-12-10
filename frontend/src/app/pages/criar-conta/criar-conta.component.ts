import { UserService } from './../../core/services/user.service';
import { UserModel } from './../../core/types/types';
import { ChangeDetectionStrategy, Component, OnInit, signal } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import {MatFormFieldModule} from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import {MatCheckboxModule} from '@angular/material/checkbox';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-criar-conta',
  standalone: true,
  imports: [MatFormFieldModule, MatInputModule, MatIconModule, ReactiveFormsModule, MatButtonModule, MatCheckboxModule, CommonModule],
  templateUrl: './criar-conta.component.html',
  styleUrls: ['./criar-conta.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class CriarContaComponent  implements OnInit {
  novoUsuarioFormGroup!: FormGroup
  carregando: boolean = false

  constructor(private formBuilder: FormBuilder, private userService: UserService, private router:Router) {
  }

  ngOnInit(): void {
    this.novoUsuarioFormGroup = this.formBuilder.group({
        email: ['', [Validators.required, Validators.email]],
        confirmEmail: ['', [Validators.required, Validators.email]],
        nome: ['', [Validators.required, Validators.minLength(5)]],
        senha: ['', [Validators.required,  Validators.minLength(5)]],
        confirSenha: ['', [Validators.required, Validators.minLength(5)]]
    });
  }


  cadastrar(){
    if(this.novoUsuarioFormGroup.valid){
      const novoUsuarioData = this.novoUsuarioFormGroup.getRawValue();
      const nome = novoUsuarioData.nome;
      const email = novoUsuarioData.email;
      const senha = novoUsuarioData.senha;

      const novoUsuario: UserModel = {
        nome: nome,
        email: email,
        senha: senha
      };

      this.userService.register(novoUsuario).subscribe(()=>{
        Swal.fire({
          icon: 'success',
          title: 'Conta criada com sucesso!',
          text: 'Faça login para acessar o sistema',
          showConfirmButton: false,
          timer: 1000,
          timerProgressBar: true,
          backdrop: true,
          toast: true,
          position: 'top-end',
          }).then(() => {
          this.router.navigate(['login'])
        })
      },(error) => {
        Swal.fire({
          icon: 'error',
          title: 'Erro ao criar conta',
          text: error.error.message || 'Falha no servidor, tente novamente mais tarde.',
          showConfirmButton: true,
        });
      })
  }
}

}
