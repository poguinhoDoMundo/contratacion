import { Component, OnInit,NgZone } from '@angular/core';
import { FormBuilder, Validators, FormArray, Form } from '@angular/forms';
import { Router} from '@angular/router'

import { userInfo } from '../../../Models/authUser';
import { authService } from '../../services/auth/authService';
import { SecurityService } from '../../services/auth/security.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  constructor(  private _formBuilder:FormBuilder, private _authService:authService,
                private _zone:NgZone, private _route:Router, private _sec:SecurityService ) {
    
                  this._sec.logOut(); 
  }

  ngOnInit(): void {
  }

  get user(){
    return this.registerForm.get("user") ;
  }

  get pass(){
    return this.registerForm.get("pass") ;
  }

  registerForm = this._formBuilder.group({
             user:['', Validators.required ], 
             pass:['', Validators.required]
  });

  onSubmit(){
      let user:any = this.buildUser();
      this._authService.getToken(user).subscribe( res=>{
        let result:any = res; 
        let token= result.body.value.token;
        if ( token == null  )
          alert( result.body.value );
        else{
          this._sec.setAuthData( token );
          this._route.navigate(["carga"]);
        }
      });
  }

  buildUser():userInfo  {
      let user: userInfo={
        user:this.registerForm.get("user").value,
        pass:this.registerForm.get("pass").value
      };

      return user;
  }

}