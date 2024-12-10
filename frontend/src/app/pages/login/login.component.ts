import { Component, signal, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { Router, RouterModule } from '@angular/router';
import { AuthService } from '../../core/services/autenticacao/auth/auth.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-login',
  imports: [MatCardModule, MatInputModule, MatIconModule, FormsModule, ReactiveFormsModule, MatButtonModule, RouterModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent implements OnInit {
  loginForm!: FormGroup;
  errorMessage = signal('');

  mostrarSenha: boolean = false;

  constructor(private authService: AuthService, private formBuilder: FormBuilder, private router: Router) {}
  ngOnInit(): void {
        this.loginForm = this.formBuilder.group({
        email: ['', [Validators.required, Validators.email]],
        senha: ['', [Validators.required,  Validators.minLength(5)]],
    });
  }

  alterMostrarSenha(){
    this.mostrarSenha = !this.mostrarSenha;
  }

  logar(){
    if(this.loginForm.valid){
      const novoUsuarioData = this.loginForm.getRawValue();
      const email = novoUsuarioData.email;
      const senha = novoUsuarioData.senha;

      this.authService.autenticar(email, senha).subscribe(()=>{
        Swal.fire({
          icon: 'success',
          title: 'Login realizado com sucesso',
          text: 'Seja bem vindo',
          showConfirmButton: false,
          timer: 1000,
          timerProgressBar: true,
          backdrop: true,
          toast: true,
          position: 'top-end',
          }).then(() => {
          this.router.navigate([''])
        })

      }, (error) => {
        Swal.fire({
          icon: 'error',
          title: 'Erro ao realizar login',
          text: error.error.message || 'Verifique suas credenciais e tente novamente.',
          showConfirmButton: true,
        });
      })

    }
  }
}
