import { Component, OnInit } from '@angular/core';

import { cargaService } from '../../../services/cargaService';

@Component({
  selector: 'app-v-carga',
  templateUrl: './v-carga.component.html',
  styleUrls: ['./v-carga.component.css']
})
export class VCargaComponent implements OnInit {

  id_usuario=1;
  carga:any=[];
  baseUrl="https://localhost:5001/";
  constructor(  private _cargaService:cargaService ) { }

  ngOnInit(): void {
    this.fillService();
  }

  fillService(){
    this._cargaService.getvCarga(this.id_usuario).subscribe( res=>{
      this.carga = res;
      console.log( this.baseUrl+this.carga.path);     
    } )
  }

}
