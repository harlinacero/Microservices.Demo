import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule } from "@angular/forms";
import { Router } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { IUserLogin } from '../models/iuser-login';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  loginForm!: FormGroup;

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private authService: AuthService
  ) { }

  ngOnInit(): void {
    this.buildForm();
  }

  buildForm() {
    this.loginForm = this.formBuilder.group({
      Username: ['', []],
      Password: ['', []]
    });
  }

  submit({ value, valid }: { value: IUserLogin, valid: boolean }) {
    this.authService.login(value)
      .subscribe(() => {
    
        if (this.authService.isAuthenticated) {     
          if (this.authService.redirectUrl) {
            const redirectUrl = this.authService.redirectUrl;
            this.authService.redirectUrl = '';
            this.router.navigate([redirectUrl]);
          } else {
            this.router.navigate(['/']);
          }
        } else {
          const loginError = 'Unable to login';
          console.error(loginError);
        }
      });
  }
}
