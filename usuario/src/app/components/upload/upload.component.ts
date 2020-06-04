import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { HttpEventType, HttpClient } from '@angular/common/http';
import { FileItem, HttpClientUploadService } from '@wkoza/ngx-upload';

@Component({
  selector: 'app-upload',
  templateUrl: './upload.component.html',
  styleUrls: ['./upload.component.css']
})
export class UploadComponent implements OnInit {

  
  public progress: number;
  public message: string;
  @Output() public onUploadFinished = new EventEmitter();

  files: File[] = [];

  ngOnInit() {    }
  constructor(private http: HttpClient) { }
  
  onSelect(event) {

      if ( this.files.length >= 1 )
      {
        alert("Solo se puede cargar un archivo por documento...");
        return;
      }

      console.log(event);
      this.files.push(...event.addedFiles);

      const formData = new FormData();
  
      for (var i = 0; i < this.files.length; i++) { 
        formData.append("file[]", this.files[i]);
      }
 
      this.http.post('https://localhost:5001/api/carga', formData, {reportProgress: true, observe: 'events'})
      .subscribe(event => {
        if (event.type === HttpEventType.UploadProgress)
          this.progress = Math.round(100 * event.loaded / event.total);
        else if (event.type === HttpEventType.Response) {
          this.message = 'Upload success.';
          this.onUploadFinished.emit(event.body);
        }
      });

  }

  onRemove(event) {
      console.log(event);
      this.files.splice(this.files.indexOf(event), 1);
  }


}
 
 /*
  public uploadFile = (files) => {
    if (files.length === 0) {
      return;
    }
 
    let fileToUpload = <File>files[0];
    const formData = new FormData();
    formData.append('file', fileToUpload, fileToUpload.name);
    formData.append('id','3','id');

    this.http.post('https://localhost:5001/api/carga', formData, {reportProgress: true, observe: 'events'})
      .subscribe(event => {
        if (event.type === HttpEventType.UploadProgress)
          this.progress = Math.round(100 * event.loaded / event.total);
        else if (event.type === HttpEventType.Response) {
          this.message = 'Upload success.';
          this.onUploadFinished.emit(event.body);
        }
      });
  }
}*/