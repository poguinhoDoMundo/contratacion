import { Component, OnInit, Output, EventEmitter, Input, ɵCompiler_compileModuleAndAllComponentsAsync__POST_R3__ } from '@angular/core';
import { HttpEventType, HttpClient } from '@angular/common/http';

import { uploadService } from '../../services/uploadService'
import { Carga } from '../../../Models/Carga';

@Component({
  selector: 'app-upload',
  templateUrl: './upload.component.html',
  styleUrls: ['./upload.component.css']
})
export class UploadComponent implements OnInit {

  public progress: number;
  public message: string;
  public hasRevision:any;
  @Output() public onUploadFinished = new EventEmitter();
  @Input() docs;

  files: File[] = [];


  constructor(private http: HttpClient, private _serviceUpload:uploadService) { }

  ngOnInit() {  
    this.fillRevision();
  }

  onSelect(event) {

      if ( this.files.length >= 1 )
      {
        alert("Solo se puede cargar un archivo por documento...");
        return;
      }

      this.files.push(...event.addedFiles);

      const formData = new FormData();
  
      for (var i = 0; i < this.files.length; i++) { 
        formData.append("file[]", this.files[i]);
      }
 
      this._serviceUpload.subirImagen( formData ).subscribe(event => {
        
        if (event.type === HttpEventType.UploadProgress)
          this.progress = Math.round(100 * event.loaded / event.total);
        else if (event.type === HttpEventType.Response) {
          let response:any = event.body;
          this.message = 'Upload success.';
          this.onUploadFinished.emit(event.body);
          
          let carga:Carga = this.BuildModel( response.dbPath, this.docs.id_persona, this.docs.id );
          this._serviceUpload.add_carga(carga).subscribe(res=>{
          let result:any = res;
          if ( result.value == "OK" ) {
            alert("Documento cargado correctamente");
            this.fillRevision();   
          }
          else {
            alert("ha ocurrido un error " + result.value );
            this.files = null;  
          }
          });
        }
      });
  }

  BuildModel(  ruta, id_usuario, id_docs  ){
    let carga:Carga = {
        Id:0,
        Id_documento:id_docs,
        Path: ruta,
        Id_usuario : id_usuario,
        Id_estado:1
    }

    return carga;
  }

  fillRevision(){
      this._serviceUpload.hasRevision( this.docs.id_persona, this.docs.id).subscribe( res=>{
            this.hasRevision = res ;
      });   

  }

  onRemove(event) {
      this.files.splice(this.files.indexOf(event), 1);
  }


}