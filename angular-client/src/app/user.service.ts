import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { User } from './User';

//const url: string = 'http://localhost:3000/';
const url: string = 'https://localhost:5001/';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient) {
  }

  getUser() {
    return this.http.get<User>(url + 'api/values');
  }
}
