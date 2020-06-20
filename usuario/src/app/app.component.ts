import { Component } from '@angular/core';

import { SecurityService } from './services/auth/security.service'
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {

  isAuthenticated:any;
  authSus$: Subscription;
  title = 'Carga tus documento!!!';

  constructor( private _sec:SecurityService ){
      this.isAuthenticated = this._sec.isAuthorized;

      this.authSus$ = this._sec.authObs$.subscribe(  (isAuth)=>{
          this.isAuthenticated = isAuth;
      }   );
  }
}
