import { Component, OnInit, TemplateRef } from '@angular/core';
import { Evento } from '../models/Evento';
import { EventoService } from '../services/evento.service';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';


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

  set filtroLista(value :string){
    this._filtroLista = value;
    this.eventosFiltrados = this.filtroLista ? this.filtrarEventos(this.filtroLista) : this.eventos;
  }


  eventosFiltrados: Evento[];
  eventos: Evento[];
  evento: Evento;
  imagemLargura = 50;
  imagemMargem = 2;
  mostrarImagem = false;
  modalRef: BsModalRef;

  constructor(
     private eventoService: EventoService,
     private modalService: BsModalService
     ) { }

  openModal(template: TemplateRef<any>){
    this.modalRef = this.modalService.show(template);
  }

  ngOnInit() {
    this.getEventos();
  }

  alterarImagem(){
    this.mostrarImagem = !this.mostrarImagem;
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
          console.log(_eventos);
        },
        error =>
        {
          console.log(error)
        }
    );
  }
}
