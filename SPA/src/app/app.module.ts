import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import {HttpClientModule} from '@angular/common/http'
import { AppRoutingModule } from './app-routing.module';
import {FormsModule} from '@angular/forms';
import { AppComponent } from './app.component';
import {RegisterComponent} from './Register/register.component';
import {NavComponent} from './Nav/nav.component';
import {AuthService} from './_services/auth.service';
import {ErrorInceptProvide} from './_services/error.interceptor';
@NgModule({
  declarations: [
    AppComponent,
    RegisterComponent,
    NavComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule
  ],
  providers: [AuthService,ErrorInceptProvide],
  bootstrap: [AppComponent]
})
export class AppModule { }
