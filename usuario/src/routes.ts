import { Routes } from '@angular/router';

import { UploadComponent } from './app/components/upload/upload.component';
import { CargaDocumentosComponent } from './app/components/carga-documentos/carga-documentos.component'
import {  VCargaComponent } from "./app/components/views/v-carga/v-carga.component";
import { LoginComponent } from './app/components/login/login.component';
import { authGuard } from './app/services/auth/authGuard';



 export const appRoutes: Routes=[
    {  path:"upload", component:UploadComponent, canActivate:[authGuard] },
    {  path:"carga", component:CargaDocumentosComponent, canActivate:[ authGuard ] },
    {  path:"vcarga", component:VCargaComponent, canActivate:[authGuard] },
    {  path:"login", component:LoginComponent },

    { path:"",redirectTo:"login", pathMatch:"full" }
 ]