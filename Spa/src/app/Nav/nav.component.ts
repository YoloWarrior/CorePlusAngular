import {Component,OnInit} from '@angular/core';
import {AuthService} from '../_services/auth.service';
@Component({
	selector:'app-nav',
	templateUrl:'./nav.component.html',
	styleUrls: ['./nav.styles.css']
	
})
export class  NavComponent implements OnInit {
	model:any = {};
	constructor(public authSercice:AuthService) {
		// code...
	}
	ngOnInit(){

	}
	login(){
		this.authSercice.login(this.model).subscribe(next=>{
			console.log('Logged')
		},error=>{
			console.log(error);
		})
	}

	loggedIn(){
		return this.authSercice.loggedIn();
	}
	loggedOut(){
		localStorage.removeItem('token');
	}	
}
