import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { ModalModule } from 'ngx-bootstrap/modal';
import { TooltipModule } from 'ngx-bootstrap/tooltip';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';

import { AppRoutingModule } from './app-routing.module';


import { EventoService } from './services/evento.service';

import { AppComponent } from './app.component';
import { EventosComponent } from './eventos/eventos.component';
import { NavComponent } from './nav/nav.component';
import { CommonModule } from '@angular/common';

import { DataTimeFormatPipePipe } from './helps/DataTimeFormatPipe.pipe';

@NgModule({
  declarations: [
    NavComponent,
    AppComponent,
    EventosComponent,
    DataTimeFormatPipePipe
   ],
  imports: [
    BrowserModule,
    ModalModule.forRoot(),
    TooltipModule.forRoot(),
    BrowserAnimationsModule,
    BsDropdownModule.forRoot(),
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    CommonModule 
  ],
  providers: [
    EventoService
  ],
  bootstrap: [
    AppComponent
  ]
})
export class AppModule { }
