import { Injectable } from "@angular/core";
import { HttpClient,HttpHeaders } from "@angular/common/http";
import { Observable, throwError } from 'rxjs';
import { catchError, retry } from 'rxjs/operators';

import { environment } from '../../environments/environment.prod';

@Injectable({ providedIn: 'root' })
export class documentosService{

    constructor ( private _http: HttpClient ){}

    baseUrl =  environment.baseUrl +  "documentos/";
    httpHeaders= new HttpHeaders() ;
    
    getDocumentos( id_persona ){
        // this.httpHeaders = this.httpHeaders.append( "Authorization", "Bearer " + this.token );

        return this._http.get( this.baseUrl + 'getDocsPersona/' + id_persona, { headers:this.httpHeaders } )
               .pipe( retry(1), catchError(this.errorHandl) );
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