import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';

import { SecurityService} from './security.service';
import { catchError } from 'rxjs/operators';


@Injectable()
export class jwtInterceptor implements HttpInterceptor{

    constructor( private _sec:SecurityService ){ }

    intercept( req: HttpRequest<any>, next: HttpHandler ): Observable<HttpEvent<any>>{
        const token= this._sec.getToken();  
        if (token){
            req= req.clone({ 
                    setHeaders:{ Authorization: 'Bearer ' + token ,
                                 'content-type': 'application/json'   
                                } 
                            });
        }

        return next.handle(req);
    }

}