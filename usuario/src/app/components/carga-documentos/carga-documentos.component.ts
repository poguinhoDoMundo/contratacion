import { Component, OnInit } from '@angular/core';

import { documentosService } from '../../services/documentosService';

@Component({
  selector: 'app-carga-documentos',
  templateUrl: './carga-documentos.component.html',
  styleUrls: ['./carga-documentos.component.css']
})
export class CargaDocumentosComponent implements OnInit {

  documentos:any=[];
  titulos:string[]=[];
  id_persona = '1';
  
  constructor( private docs:documentosService ) { }

  ngOnInit(): void {
    this.fillDocs(); 
  }

  fillDocs()
  {
      this.docs.getDocumentos(this.id_persona).subscribe( res =>{
        this.documentos = res;
      } )      
  }


}
