import { Component } from '@angular/core';
import { UserService } from './user.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'app';

  constructor(private userService: UserService) { }

  getUserDetails() {

    this.userService.getUser()
      .subscribe(
        () => {
          console.log("User is logged in");
        }
      );

  }

}