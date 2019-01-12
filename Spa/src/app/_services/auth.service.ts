import {Injectable} from "@angular/core";
import {HttpClient} from "@angular/common/http";
import {map} from 'rxjs/operators';

@Injectable({
	providedIn:'root'
})
export class AuthService {
	
	someurl = 'http://localhost:5000/api/auth';

	constructor(private http:HttpClient) {
		// code...
	}

	login(model:any){
		return this.http.post(this.someurl + '/login',model).pipe(map((response:any)=>{
			const user = response;
			if(user){
				localStorage.setItem('token',user.token)
			}
		}))
			
	}
}