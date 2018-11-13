import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from './services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'app';

  constructor(
    private router: Router,
    private authService: AuthService) { }

  navigateToHome() {

    this.router.navigate(["/"]);

  }

  navigateToSecureArea() {

    this.router.navigate(["/secure"]);

  }

  logout() {

    this.authService.logout();

  }
}