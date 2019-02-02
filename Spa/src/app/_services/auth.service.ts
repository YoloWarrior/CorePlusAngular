import {Injectable} from "@angular/core";
import {HttpClient} from "@angular/common/http";
import {map} from 'rxjs/operators';
import {JwtHelperService} from '@auth0/angular-jwt';
@Injectable({
	providedIn:'root'
})
export class AuthService {
	
	someurl = 'http://localhost:5000/api/auth';
    jwt = new JwtHelperService();
    decodeToken:any;
	constructor(private http:HttpClient) {
		// code...
	}
loggedIn(){
	const token = localStorage.getItem('token');
	return !this.jwt.isTokenExpired(token);
}
	login(model:any){
		return this.http.post(this.someurl + '/login',model).pipe(map((response:any)=>{
			const user = response;
			if(user){
				localStorage.setItem('token',user.token);
				this.decodeToken = this.jwt.decodeToken(user.token);
				console.log(this.decodeToken);
			}
		}))
			
	}
	Register(model:any){
		return this.http.post(this.someurl+'/register',model);
	}
	IsConfirm(model:any){
		return this.http.post(this.someurl +'/confirmed',model);
	}
}