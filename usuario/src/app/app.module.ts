import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import {FormsModule, ReactiveFormsModule  } from '@angular/forms';
import { NgxDropzoneModule } from 'ngx-dropzone';

import {RouterModule} from '@angular/router';
import { appRoutes} from '../routes'
import { GroupByPipe } from '../pipes/pipes';

import { AppComponent } from './app.component';
import { UploadComponent } from './components/upload/upload.component';
import { CargaDocumentosComponent } from './components/carga-documentos/carga-documentos.component';
import { VCargaComponent } from './components/views/v-carga/v-carga.component';
import { NavbarComponent } from './components/menu/navbar/navbar.component';
import { LoginComponent } from './components/login/login.component';
import { jwtInterceptor } from './services/auth/jwtInterceptor';
import { UsuarioComponent } from './components/crud/usuario/usuario.component';


@NgModule({
  declarations: [
    AppComponent,
    UploadComponent,
    CargaDocumentosComponent,
    VCargaComponent,
    GroupByPipe,
    NavbarComponent,
    LoginComponent,
    ComponentssComponent,
    UsuarioComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    RouterModule.forRoot(appRoutes),
    NgxDropzoneModule
  ],
  providers: [{
      provide: HTTP_INTERCEPTORS,
      useClass: jwtInterceptor,
      multi:true 
  }],
  bootstrap: [AppComponent]
})
export class AppModule { }
