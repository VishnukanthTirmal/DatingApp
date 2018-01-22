
import { Http, Headers, RequestOptions } from '@angular/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';

import { User } from '../_models/User';
// tslint:disable-next-line:import-blacklist
import { Observable } from 'rxjs/Rx';

import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';

@Injectable()
export class UserService {
baseUrl = environment.apiUrl;
constructor(private http: Http) { }
getUsers(): Observable<User[]> {
  return this.http.get(this.baseUrl + 'users', this.jwt())
  .map(response => <User[]>response.json())
  .catch(this.handleError);
}
private jwt() {
        const token = localStorage.getItem('token');
        if (token) {
            const Headers1 = new Headers({'Authorization' : 'Bearer ' + token});
            Headers1.append('Content-type', 'application/json');
            return new RequestOptions({headers: Headers1});
        }
  }
  private handleError(error: any) {
    const applicationError = error.headers.get('Application-Error');
    if (applicationError) {
        return Observable.throw(applicationError);
    }
    const serverError = error.json();
    let modelStateErrors = '';
    if (serverError) {
        for (const key in serverError) {
            if (serverError[key]) {
                modelStateErrors += serverError[key] + '\n';
            }
        }
    }
    return Observable.throw (
        modelStateErrors || 'server error'
    );
 }

}


