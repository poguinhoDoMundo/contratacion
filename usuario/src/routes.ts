import { Routes } from '@angular/router';

import { UploadComponent } from './app/components/upload/upload.component';


 export const appRoutes: Routes=[
    {  path:"upload", component:UploadComponent },

  
    { path:"",redirectTo:"", pathMatch:"full" }
 ]