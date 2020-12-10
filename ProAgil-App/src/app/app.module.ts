import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ModalModule } from 'ngx-bootstrap/modal';
import { TooltipModule } from 'ngx-bootstrap/tooltip';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { ToastrModule } from 'ngx-toastr';

import { AppRoutingModule } from './app-routing.module';


import { EventoService } from './services/evento.service';

import { AppComponent } from './app.component';
import { EventosComponent } from './eventos/eventos.component';
import { NavComponent } from './_nav/nav.component';
import { CommonModule } from '@angular/common';
import { DashboardComponent } from './dashboard/dashboard.component';
import { ContatosComponent } from './contatos/contatos.component';
import { PalestrantesComponent } from './palestrantes/palestrantes.component';
import { TituloComponent } from './_shared/titulo/titulo.component';

import { DataTimeFormatPipePipe } from './_helps/DataTimeFormatPipe.pipe';


@NgModule({
  declarations: [			
    NavComponent,
    AppComponent,
    EventosComponent,
    DashboardComponent,
    ContatosComponent,
    PalestrantesComponent,
    TituloComponent,  
    DataTimeFormatPipePipe
   ],
  imports: [
    BrowserModule,
    ModalModule.forRoot(),
    TooltipModule.forRoot(),
    BrowserAnimationsModule,
    BsDropdownModule.forRoot(),
    BsDatepickerModule.forRoot(),
    ToastrModule.forRoot({
      timeOut: 10000,
      positionClass: 'toast-bottom-right',
      preventDuplicates: true,
    }), 
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    CommonModule,
    ReactiveFormsModule 
  ],
  providers: [
    EventoService
  ],
  bootstrap: [
    AppComponent
  ]
})
export class AppModule { }
