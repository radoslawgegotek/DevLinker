import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { LoginUserForm, RegisterUserForm, UserInfo } from "../../shared/models/account.model";
import { map, Observable, ReplaySubject } from "rxjs";
import { environment } from '../../../environments/environment.development';
import { Result, ResultValue } from '../../shared/models/response.model';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private userSource = new ReplaySubject<UserInfo | null>(1);
  public user$ = this.userSource.asObservable();

  constructor(private _http: HttpClient) { }

  public getUserInfo(): Observable<ResultValue<UserInfo>> {
    return this._http.get<ResultValue<UserInfo>>(`${environment.DevLinkerApiURL}/auth/Account/userinfo`, { withCredentials: true }).pipe(
      map((response) => {
        if (response.isSuccess)
          this.setUser(response.valueResult);
        else
          this.setUser(null);
        return response;
      })
    );
  }

  public register(form: RegisterUserForm): Observable<Result> {
    return this._http.post<Result>(`${environment.DevLinkerApiURL}/auth/Account/register`, form, { withCredentials: true });
  }

  public signIn(form: LoginUserForm): Observable<Result> {
    return this._http.post<Result>(`${environment.DevLinkerApiURL}/auth/Account/signin`, form, { withCredentials: true }).pipe(
      map((response) => {
        this.getUserInfo().subscribe();
        return response;
      })
    );
  }

  public signOut(): Observable<Result> {
    return this._http.post<Result>(`${environment.DevLinkerApiURL}/auth/Account/signout`, {}, { withCredentials: true }).pipe(
      map((response) => {
        this.setUser(null);
        return response;
      })
    );
  }

  private setUser(user: UserInfo | null) {
    this.userSource.next(user);
  }

}
