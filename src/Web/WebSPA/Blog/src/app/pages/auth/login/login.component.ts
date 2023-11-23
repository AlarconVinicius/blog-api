import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { LocalStorageUtils } from 'src/app/helper/localstorage/localstorage';
import { Login, LoginResponse } from 'src/app/models/auth/login';
import { AuthService } from 'src/app/services/auth/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  public LocalStorage = new LocalStorageUtils();

  loginObj: Login = {} as Login;
  loginResponse = {} as LoginResponse;
  constructor(private authService: AuthService, private router: Router) { }

  ngOnInit(): void {
    this.isLoggedIn();
  }

  onLogin() {
    this.authService.login(this.loginObj).subscribe(data => {
      this.loginResponse = data
      this.authService.LocalStorage.saveLocalUserData(this.loginResponse);
      if(Boolean(this.authService.LocalStorage.getUserToken)){
        this.router.navigate(['admin']);
      }
    });
  }

  isLoggedIn(){
    if(this.LocalStorage.isLoggedIn()){
      this.router.navigate(['']);
    }
  }
}
