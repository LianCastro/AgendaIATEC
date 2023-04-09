import { Component, OnInit } from '@angular/core';
import { Event } from 'src/app/_models/event';
import { EventService } from 'src/app/_services/event.service';

@Component({
  selector: 'app-event-list',
  templateUrl: './event-list.component.html',
  styleUrls: ['./event-list.component.css']
})
export class EventListComponent implements OnInit {
  events: Event[] = [];
  
  constructor(private eventService: EventService) {}

  ngOnInit(): void {
    this.loadEvents()
  }

  loadEvents() {
    this.eventService.getEvents().subscribe({
      next: events => this.events = events
    })
  }
}
