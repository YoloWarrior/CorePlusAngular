import {Component,OnInit} from '@angular/core';
import {AuthService} from '../_services/auth.service';
@Component({
	selector:'app-nav',
	templateUrl:'./nav.component.html'
})
export class  NavComponent implements OnInit {
	model:any = {};
	constructor(private authSercice:AuthService) {
		// code...
	}
	ngOnInit(){

	}
	login(){
		this.authSercice.login(this.model).subscribe(next=>{
			console.log('Logged')
		},error=>{
			console.log("error");
		})
	}
	loggedIn(){
		const token = localStorage.getItem('token');
		return !!token;
	}
	loggedOut(){
		localStorage.removeItem('token');
	}	
}
