import { Component, OnInit } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { EventService } from '../_services/event.service';
import { Event } from '../_models/event';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  registerMode = false;

  constructor(public accountService: AccountService, private eventService: EventService) {}

  ngOnInit(): void {
  }

  registerToggle() {
    this.registerMode = !this.registerMode;
  }

  cancelRegisterMode(e: boolean) {
    this.registerMode = e;
  }
}
