import { Injectable } from "@angular/core";
import { HttpClient, HttpEvent, HttpErrorResponse, HttpEventType } from  '@angular/common/http';  
import { map } from  'rxjs/operators';


@Injectable({ providedIn: 'root' })
export class documentoService{

    constructor( private httpClient:HttpClient ){}


}