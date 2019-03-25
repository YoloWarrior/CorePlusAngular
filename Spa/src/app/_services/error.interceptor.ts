import{Injectable}from "@angular/core";
import {HttpInterceptor,HttpRequest,HttpHandler,HttpErrorResponse,HttpEvent,HTTP_INTERCEPTORS} from "@angular/common/http";
import {Observable,throwError} from "rxjs";
import {catchError} from "rxjs/operators";

@Injectable()
export class ErrorIncept implements HttpInterceptor {
	
	intercept(req:HttpRequest<any>,next:HttpHandler):Observable<HttpEvent<any>>{
		return next.handle(req).pipe(
				catchError(error=>{
					if(error instanceof HttpErrorResponse){
						if(error.status ===401){
							 return throwError(error.statusText);
						}
						const appError = error.headers.get('Application-Error');
						if(appError){
							console.error(appError);
							return throwError(appError);
							
						}
						const serverError = error.error;
						let ModalStateError = '';
						if(serverError && typeof serverError ==="object"){
								for(const key in serverError){
									if(serverError[key]){
										ModalStateError +=serverError[key]+'\n';
									}
								}
						}
						return throwError(ModalStateError || serverError || "OHh, Some Bad Happends");
					}
				})
			);
	}
}

export const ErrorInceptProvide = {
	provide:HTTP_INTERCEPTORS,
	useClass:ErrorIncept,
	multi:true
}