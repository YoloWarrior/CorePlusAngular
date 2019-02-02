  import { Component,OnInit } from '@angular/core';
import {AuthService} from './_services/auth.service';
import {JwtHelperService} from '@auth0/angular-jwt';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'Spa';
  registerMode = false;
 jwt = new JwtHelperService();
 model:any;
  constructor(private authService:AuthService){}
  ngOnInit(){
       	const token = localStorage.getItem('token');
       	if(token){
       		this.authService.decodeToken  = this.jwt.decodeToken(token);
       	}
       }
       RegisterMethod(){
         this.registerMode = true;
       }
       cancelRegisterMode(registerMode:boolean){
         this.registerMode = registerMode;
       }
       loggedIn(){
    return this.authService.loggedIn();
  }
      
  }

