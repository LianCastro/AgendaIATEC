import { Component, Input, OnInit } from '@angular/core';
import { EventService } from '../_services/event.service';
import { Event } from '../_models/event';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
  events: Event[] | undefined;
  
  constructor(private eventService: EventService) {
  }

  ngOnInit(): void {
    this.loadEvents()
  }

  loadEvents() {
    this.eventService.getEvents(true, false).subscribe({
      next: events => {
        if (events) this.events = events;
      }
    })
  }
}
