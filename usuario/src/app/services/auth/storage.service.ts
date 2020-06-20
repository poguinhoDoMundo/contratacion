import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class StorageService {

  storage:any;
  constructor() {
      this.storage = sessionStorage;
   }

   retrive( key:string ){
      const item = this.storage.getItem(key);
      if( item && item !== 'undefined' )
          return JSON.parse(item);      
      return;    
   }

   set( key:string, value:any ) {
      this.storage.setItem(key,  JSON.stringify(value));
   } 

   drop( key:string ){
      this.storage.removeItem(key);
   }

}
