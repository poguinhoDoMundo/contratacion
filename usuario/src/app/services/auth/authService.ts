import { Injectable } from "@angular/core";
import { HttpClient, HttpEvent, HttpErrorResponse, HttpEventType, HttpHeaders } from  '@angular/common/http';  
import { Observable, throwError } from 'rxjs';
import { catchError, retry } from 'rxjs/operators';

import { environment } from '../../../environments/environment';
 
@Injectable( { providedIn:'root' })
export class authService{
    constructor( private _http:HttpClient ){}

    getToken(data:any){
        return this._http.post( environment.baseUrl + "account/login", data, { observe:'response' } )
                .pipe(retry(1), catchError( this.errorHandl ) );
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