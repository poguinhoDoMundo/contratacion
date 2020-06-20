import { Injectable } from "@angular/core";
import { HttpClient, HttpEvent, HttpErrorResponse, HttpEventType, HttpHeaders } from  '@angular/common/http';  
import { Observable, throwError } from 'rxjs';
import { catchError, retry } from 'rxjs/operators';

import { environment } from '../../environments/environment';

@Injectable({ providedIn: 'root' })
export class uploadService{
    
    httpHeaders= new HttpHeaders(  ) ;
    constructor( private httpClient:HttpClient ){}
    basicUrl=  environment.baseUrl + "carga/";

    public hasRevision( user,docs  ){
      // if ( !this.httpHeaders.has("Authorization") )
      //   this.httpHeaders = this.httpHeaders.append( "Authorization", "Bearer " +this.token );
      
      return this.httpClient.get( this.basicUrl + "hasRevision/" + user +"/"+docs )
               .pipe(retry(1),catchError(this.errorHandl))   ;
    }  

    public subirImagen( formData  ){
      // if ( !this.httpHeaders.has("Authorization") )
      //     this.httpHeaders = this.httpHeaders.append("Authorization", "Bearer "+ this.token );
        return this.httpClient.post( this.basicUrl + 'postPdf', formData, 
        { reportProgress: true, observe: 'events' })
    }

    public add_carga( data:any ){
      // if ( !this.httpHeaders.has("Authorization") )
      //   this.httpHeaders = this.httpHeaders.append("Authorization", "Bearer "+ this.token );
      return this.httpClient.post( this.basicUrl, data )
               .pipe(retry(1), catchError( this.errorHandl )  ); 
    }

    errorHandl(error) {
        let errorMessage = '';
        if(error.error instanceof ErrorEvent) {
          // Get client-side error
          errorMessage = error.error.message;
        } else {
          // Get server-side error
          errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
        }
        console.log(errorMessage);
        return throwError(errorMessage);
     }
}