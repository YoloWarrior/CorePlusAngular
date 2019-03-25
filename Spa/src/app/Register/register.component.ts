import {Component,OnInit,EventEmitter,Output} from '@angular/core';
import {AuthService} from '../_services/auth.service';
@Component({
	selector:'app-register',
	templateUrl:'./register.component.html'
	
	
})
export class  RegisterComponent implements OnInit {
	model:any = {};
	@Output () cancelRegister = new EventEmitter();
	constructor(public authSercice:AuthService) {
		// code...
	}
	ngOnInit(){

	}
	register(){
		this.authSercice.Register(this.model).subscribe(next=>{
			console.log('Success')

		},error=>{
			console.log(error);
		})
	}
	cancel(){
		this.cancelRegister.emit(false);
	}
	loggedIn(){
    return this.authSercice.loggedIn();
	
}
	
}