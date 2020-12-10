import { Component, OnInit, TemplateRef } from '@angular/core';
import { Evento } from '../_models/Evento';
import { EventoService } from '../services/evento.service';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { FormGroup,  Validators, FormBuilder } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { BsLocaleService } from 'ngx-bootstrap/datepicker';
import { defineLocale } from 'ngx-bootstrap/chronos';
import { ptBrLocale } from 'ngx-bootstrap/locale';
defineLocale('pt-br', ptBrLocale);


@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.css'],
})
export class EventosComponent implements OnInit {

  _filtroLista: string = '';

  get filtroLista():string {
    return this._filtroLista;
  }

  set filtroLista(value: string){
    this._filtroLista = value;
    this.eventosFiltrados = this.filtroLista ? this.filtrarEventos(this.filtroLista) : this.eventos;
  }

  titulo = 'Eventos';
  eventosFiltrados: Evento[];
  eventos: Evento[];
  evento: Evento;
  modoSalvar = 'post';
  imagemLargura = 50;
  imagemMargem = 2;
  mostrarImagem = false;
  registerForm: FormGroup;
  bodyDeletarEvento = '';

  constructor(
     private eventoService: EventoService,
     private modalService: BsModalService,
     private fb: FormBuilder,
     private localeService: BsLocaleService,
     private toastr: ToastrService
     ) {
        this.localeService.use('pt-br');
      }

  openModal(template: any){
    this.registerForm.reset();
    template.show();
  }

  ngOnInit() {
    this.validation();
    this.getEventos();
  }

  alterarImagem(){
    this.mostrarImagem = !this.mostrarImagem;
  }

  validation(){
    this.registerForm = this.fb.group({
      tema: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(255)]] ,
      local: ['', Validators.required],
      dataEvento: ['', Validators.required] ,
      qtdPessoas: ['',[Validators.required, Validators.max(120000)]] ,
      imagemURL: ['', Validators.required] ,
      email: ['', [Validators.required, Validators.email]] ,
      telefone: ['', Validators.required],
    });
  }

  editarEvento(evento: Evento, template: any){
    this.modoSalvar = 'put';
    this.openModal(template);
    this.evento = evento;
    this.registerForm.patchValue(evento);
  }

  novoEvento(template: any){
    this.openModal(template);
  }

  salvarAlteracao(template: any){
    if (this.registerForm.valid){
      if(this.modoSalvar === 'post'){
          this.evento = Object.assign({}, this.registerForm.value);
          this.eventoService.postEvento(this.evento).subscribe(
            (novoEvento: Evento) => {
              console.log(novoEvento);
              template.hide();
              this.getEventos();
              this.toastr.success('Inserido com sucesso!');
            }, error => {
              this.toastr.error('Erro ao tentar inserir! Erro:'+ error);
              console.log(error);
            }
          );
      }
      else {
        this.evento = Object.assign({id: this.evento.id}, this.registerForm.value);
        this.eventoService.putEvento(this.evento).subscribe(
            () => {
              template.hide();
              this.getEventos();
              this.toastr.success('Editado com sucesso!');
            }, error => {
              this.toastr.error('Erro ao tentar editar! Erro:'+ error);
              console.log(error);
            }
          );
      }
    }
  }


  excluirEvento(evento: Evento, template: any) {
    this.openModal(template);
    this.evento = evento;
    this.bodyDeletarEvento = `Tem certeza que deseja excluir o Evento: ${evento.tema}, Código: ${evento.id}`;
  }

  confirmeDelete(template: any) {
    this.eventoService.deleteEvento(this.evento.id).subscribe(
      () => {
          template.hide();
          this.getEventos();
          this.toastr.success('Deletado com sucesso!');
        }, error => {
          this.toastr.error('Erro ao tentar deletar! Erro:'+ error);
          console.log(error);
        }
    );
  }

  filtrarEventos(filtrarpor: string): Evento[]{
    filtrarpor = filtrarpor.toLocaleLowerCase();
    return this.eventos.filter(
      evento => evento.tema.toLocaleLowerCase().indexOf(filtrarpor) !== -1
    );
  }

  getEventos(){
    this.eventoService.getAllEvento().subscribe(
        (_eventos: Evento[]) =>
        {
          this.eventos = _eventos;
          this.eventosFiltrados = this.eventos;
        },
        error =>
        {
          this.toastr.error('Erro ao tentar carregar eventos! Erro:'+ error);
        }
    );
  }
}
