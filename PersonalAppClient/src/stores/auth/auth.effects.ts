import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { TranslateService } from '@ngx-translate/core';
import { ToastrService } from 'ngx-toastr';
import { of } from 'rxjs';
import { map, catchError, tap, switchMap } from 'rxjs/operators';
import { AuthServices } from 'src/services/auth-services.service';

import * as authAction from "./auth.actions"

@Injectable()
export class AuthEffects {

  constructor(
    private actions$: Actions,
    private AuthService: AuthServices,
    private toastr: ToastrService,
    private router: Router,
    private translate: TranslateService,
  ) {}

  adminLogin$ = createEffect(() => this.actions$.pipe(
    ofType(authAction.ADMIN_LOGIN),
    switchMap((payLoad : any) =>
      this.AuthService.loginUser(payLoad.userLogin).pipe(
        map(token  =>{
            return new authAction.AdminLoginSuccessAction(token.token as any)
          }),
        catchError((error : any) =>
          of(new authAction.AdminLoginErrorAction(error.error)
        ))
      ))
  ));

  register$ = createEffect(() => this.actions$.pipe(
    ofType(authAction.REGISTER),
    switchMap((userRegister : any) =>
      this.AuthService.registerUser(userRegister.userRegister).pipe(
        map(()  =>
          {
            return new authAction.RegisterSuccessAction();
          }),
        catchError(error =>
          of(new authAction.RegisterErrorAction()
        ))
      ))
  ));

  registerSuccess$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(authAction.REGISTER_SUCCESS),
        tap(() => {
          this.toastr.success(
            this.translate.instant('authentication.RegisterSuccess')
          )
          this.router.navigate(['/login']);
        })
      ),
    { dispatch: false }
  );

  adminLoginSuccess$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(authAction.ADMIN_LOGIN_SUCCESS),
        tap((token) => {
          localStorage.setItem('token', Object.keys(token).map(key => token[key])[0])
          this.router.navigate(['/admin']);
        })
      ),
    { dispatch: false }
  );

  adminLoginError$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(authAction.ADMIN_LOGIN_ERROR),
        tap((error) => {
          this.toastr.error(
            error['error']
          )
        })
      ),
    { dispatch: false }
  );


}
