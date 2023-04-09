import { Component, OnInit } from '@angular/core';
import { Event } from 'src/app/_models/event';
import { EventService } from 'src/app/_services/event.service';

@Component({
  selector: 'app-event-participate',
  templateUrl: './event-participate.component.html',
  styleUrls: ['./event-participate.component.css']
})
export class EventParticipateComponent implements OnInit {
  events: Event[] = [];
  
  constructor(private eventService: EventService) {
  }

  ngOnInit(): void {
    this.loadEvents()
  }

  loadEvents() {
    this.eventService.getEvents(false, false).subscribe({
      next: events => {
        if (events) this.events = events;
      }
    })
  }

}
