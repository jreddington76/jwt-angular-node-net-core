import { Component, OnInit } from '@angular/core';
import { UserService } from '../services/user.service';

@Component({
  selector: 'app-secure',
  templateUrl: './secure.component.html',
  styleUrls: ['./secure.component.css']
})
export class SecureComponent implements OnInit {

  constructor(private userService:UserService) { }

  ngOnInit() {
    this.userService.getUser()
      .subscribe(
        () => {
          console.log("User is logged in");
        }
      );
  }

}
