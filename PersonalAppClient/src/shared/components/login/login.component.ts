import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import {  Store } from '@ngrx/store';
import { UserLogin } from 'src/app/model/User.interface';
import { validatePassword } from 'src/shared/validatePattern/validatePassword';
import { AdminloginAction } from '../../../app/stores/auth/auth.actions';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  patternValidatePassword = validatePassword.PATTERN_PASSWORD;
  title: string = 'Log in';
  userLogin! : UserLogin;
  loginForm!: FormGroup
  error: any;

  constructor(
    private store: Store,
    private formBuilder: FormBuilder,
  )
  {
  }

  ngOnInit(): void {
    this.loginForm = this.formBuilder.group({
      emailLogin: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.pattern]]
    })
  }

  onSubmit(){
    var emailLogin =this.loginForm.get('emailLogin')?.value;
    var pass =this.loginForm.get('password')?.value;
    this.userLogin = {
      email : emailLogin,
      password : pass,
    }

    this.store.dispatch(new AdminloginAction(this.userLogin));
  }

}