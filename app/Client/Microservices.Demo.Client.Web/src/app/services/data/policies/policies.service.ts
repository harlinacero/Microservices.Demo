import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { IPolicy } from 'src/app/models/iproduct';
import { ICreatePolicyCommand } from './icreate-policy-command';
import { ICreatePolicyResult } from './icreate-policy-result';

@Injectable({
  providedIn: 'root'
})
export class PoliciesService {
  port = '44399';
  //baseUrl = `${this.window.location.protocol}//${this.window.location.hostname}:${this.port}`;
  baseUrl = `http://${this.window.location.hostname}:${this.port}`;
  policiesApiUrl = this.baseUrl + '/api/policies';
  policiesReportApiUrl = this.baseUrl + '/api/report/policies';

  constructor(
    private http: HttpClient,
    @Inject('Window') private window: Window
  ) { }

  CreatePolicy(createPolicyCommand: ICreatePolicyCommand): Observable<ICreatePolicyResult> {
    return this.http.post<ICreatePolicyResult>(this.policiesApiUrl, createPolicyCommand)
      .pipe(catchError(this.handleError));
  }

  getPolicies(): Observable<IPolicy[]> {
    return this.http.get<IPolicy[]>(this.policiesReportApiUrl)
      .pipe(catchError(this.handleError));
  }

  private handleError(error: HttpErrorResponse) {
    console.error('server error', error);
    if (error.error instanceof Error) {
      const erroMessage = error.message;
      return throwError(() => erroMessage);
    }
    return throwError(() => error || 'server error');
  }
}
