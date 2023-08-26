import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { map, catchError } from 'rxjs/operators';
import { ICreateOfferCommand } from './icreate-offer-command';
import { Observable, throwError } from 'rxjs';
import { ICreateOfferResult } from './icreate-offer-result';

@Injectable({
  providedIn: 'root'
})
export class OffersService {
  port = '44399';
  //baseUrl = `${this.window.location.protocol}//${this.window.location.hostname}:${this.port}`;
  baseUrl = `http://${this.window.location.hostname}:${this.port}`;
  offersApiUrl = this.baseUrl + '/api/offers';

  constructor(
    private http: HttpClient,
    @Inject('Window') private window: Window
  ) { }

  calculatePrice(createOfferCommand: ICreateOfferCommand): Observable<ICreateOfferResult> {
    return this.http.post<ICreateOfferResult>(this.offersApiUrl, createOfferCommand)
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

