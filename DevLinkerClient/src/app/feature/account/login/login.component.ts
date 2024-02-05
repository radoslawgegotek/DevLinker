import { Component, OnDestroy } from '@angular/core';
import { AuthService } from "../../../core/auth/auth.service";
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from "@angular/forms";
import { LoginUserForm } from "../../../shared/models/account.model";
import { Subject, takeUntil } from "rxjs";
import { Result } from '../../../shared/models/response.model';
import { Router } from '@angular/router';
import { MaterialModule } from '../../../shared/material.module';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [ReactiveFormsModule, MaterialModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent implements OnDestroy {

  form!: FormGroup;
  private _destroyed$ = new Subject<void>();

  constructor(
    private _authService: AuthService,
    private _fb: FormBuilder,
    private _router: Router
  ) {
    this.form = this.initLoginForm();
  }

  ngOnDestroy(): void {
    this._destroyed$.next();
    this._destroyed$.complete();
  }

  loginUser() {
    if (this.form.valid) {
      const data = this.form.value as LoginUserForm;

      this._authService.signIn(data)
        .pipe(takeUntil(this._destroyed$))
        .subscribe({
          next: (response: Result) => {
            if (response.isSuccess) {
              this._router.navigate(['']);
            }
          }
        });
    }
  }

  private initLoginForm(): FormGroup {
    return this._fb.group({
      username: this._fb.nonNullable.control('', [Validators.required, Validators.email]),
      password: this._fb.nonNullable.control('',
        [Validators.required, Validators.minLength(6), Validators.maxLength(16)])
    });
  }
}
