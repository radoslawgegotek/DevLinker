import { Component, EventEmitter, Output, signal } from '@angular/core';
import { AuthService } from "../../core/auth/auth.service";
import { AsyncPipe } from "@angular/common";
import { Router, RouterLink } from "@angular/router";
import { MaterialModule } from '../../shared/material.module';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [
    AsyncPipe,
    RouterLink,
    MaterialModule
  ],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.scss'
})
export class NavbarComponent {

  theme: string = localStorage.getItem('theme') || 'theme--light';

  @Output() sideBar: EventEmitter<any> = new EventEmitter();

  constructor(
    public authService: AuthService,
    private _router: Router) {
  }

  singOut() {
    this.authService.signOut().subscribe();
    this._router.navigate(['/']);
  }

  toggleTheme() {
    this.theme = (this.theme === "theme--light") ? "theme--dark" : "theme--light";
    localStorage.setItem('theme', this.theme);
    document.body.setAttribute('class', this.theme);
  }

  toggleSideBar() {
    this.sideBar.emit();
  }
}
