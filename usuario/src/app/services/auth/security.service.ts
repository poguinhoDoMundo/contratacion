import { Injectable } from '@angular/core';
import {Subject } from 'rxjs';

import { StorageService} from './storage.service';
import { environment } from  '../../../environments/environment.prod';
 
@Injectable({
  providedIn: 'root'
})
export class SecurityService {

  public isAuthorized:boolean=false;
  private authSource = new Subject<boolean>();
  public authObs$ = this.authSource.asObservable();
  private aData= environment.authData;
  private isA = environment.isAuthorized;

  constructor( private _storage:StorageService ) {

    if (  _storage.retrive(this.isA) !== '' ){  
        this.isAuthorized = _storage.retrive( this.isA  );
        this.authSource.next( _storage.retrive(this.isA) );
    }
  }

  getToken():any{
    return this._storage.retrive( this.aData );
  }

  resetAuthData(){
     this._storage.drop( this.aData );
     this.isAuthorized = false;
     this._storage.set( this.isA,false ); 
     this.authSource.next(false); 
  } 

  setAuthData(token:string){
    this._storage.set( this.aData,token );
    this.isAuthorized = true;
    this._storage.set( this.isA,true );
    this.authSource.next(true);
  }

  logOut(){
    this.resetAuthData();
  }

}

