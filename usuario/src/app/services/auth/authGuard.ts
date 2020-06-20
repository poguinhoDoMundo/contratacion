import { Injectable } from "@angular/core";
import { CanActivate, Router } from '@angular/router';
import { SecurityService } from './security.service';



@Injectable({ providedIn:'root' })
export class authGuard implements CanActivate {

    constructor( private _sec: SecurityService, private _route:Router ){}

    canActivate(){
        if( this._sec.isAuthorized )
            return true;
        
        this._route.navigate(["/login"]);
        return false;
    }
}