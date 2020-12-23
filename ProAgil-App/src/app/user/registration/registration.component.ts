import { Component, OnInit } from '@angular/core';
import { FormGroup,  FormBuilder, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { User } from 'src/app/_models/User';
import { AuthService } from 'src/app/services/auth.service';
import { Router } from '@angular/router';


@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.scss']
})
export class RegistrationComponent implements OnInit {

  registerForm : FormGroup;
  user: User;

  constructor(  private authservice: AuthService,
                public router: Router,
                private fb: FormBuilder,
                private toastr: ToastrService
    ) { }

  ngOnInit() {
    this.validation();
  }

  validation(){
    this.registerForm = this.fb.group({
      fullName : ['',Validators.required],
      email : ['',[Validators.required, Validators.email]],
      userName : ['',Validators.required],
      passwords : this.fb.group({
        password : ['',[Validators.required, Validators.minLength(4)]],
        confirmPassword : ['',Validators.required]
      }, {validador : this.compararSenhas})  
    });
  }

  compararSenhas(fb: FormGroup){
    const confirmaSenhaCtrl = fb.get('confirmaPassword');
    if (confirmaSenhaCtrl.errors == null || 'mismatch' in confirmaSenhaCtrl.errors){
      if (fb.get('password').value === confirmaSenhaCtrl.value){
        confirmaSenhaCtrl.setErrors({mismatch : true});
      }
      else
      {
        confirmaSenhaCtrl.setErrors(null);
      }
    }
  }

  cadastrarUsuario(){
    if (this.registerForm.valid){
      this.user = Object.assign(
        {password : this.registerForm.get('passwords.password').value}, 
                    this.registerForm.value);
      this.authservice.register(this.user).subscribe(
          () => {
              this.router.navigate(['/user/login']);
              this.toastr.success('Cadastro realizado com sucesso.')
          }, error => {
              const erro = error.error;
              erro.forEach(element => {
                switch(element.code){
                  case 'DuplicateUserName':
                      this.toastr.error('Usuário já cadastrado.');
                      break;
                  default:
                    this.toastr.error('Erro no cadastro! Erro: ${element.code}');
                    break;  
                }
              });
          }
      )              
    } 
  }
}
