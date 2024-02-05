import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterOutlet } from '@angular/router';
import { MaterialModule } from './shared/material.module';
import { AuthService } from './core/auth/auth.service';
import { MainModule } from './main/main.module';

@Component({
  selector: 'app-root',
  standalone: true,
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
  imports: [
    CommonModule,
    RouterOutlet,
    MainModule,
    MaterialModule
  ]
})
export class AppComponent implements OnInit {
  title = 'DevLinkerClient';
  theme: string;
  sideBarOpen: boolean = true;

  constructor(private _authService: AuthService) {
    this.theme = localStorage.getItem('theme') || 'theme--light';
  }

  ngOnInit(): void {
    document.body.setAttribute('class', this.theme);
    this._authService.getUserInfo().subscribe();
  }

  sideBarToggler() {
    this.sideBarOpen = !this.sideBarOpen;
  }
}
