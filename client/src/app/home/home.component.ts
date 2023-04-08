import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  registerMode = false;
  events: any;

  constructor(private http: HttpClient, public accountService: AccountService) {}

  ngOnInit(): void {
    this.getEvents();
  }

  getEvents() {
    this.http.get('https://localhost:7025/api/events').subscribe({
      next: response => this.events = response,
      error: error => console.log(error),
      complete: () => console.log('Request completed')
    });
  }

  registerToggle() {
    this.registerMode = !this.registerMode;
  }

  cancelRegisterMode(e: boolean) {
    this.registerMode = e;
  }
}
