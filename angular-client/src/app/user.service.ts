import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { User } from './User';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient) {
  }

  getUser() {
    return this.http.get<User>('http://localhost:5000/home/getuserdetails');
  }
}
