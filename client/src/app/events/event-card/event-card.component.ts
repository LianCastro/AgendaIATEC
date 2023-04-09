import { Component, Input, OnInit } from '@angular/core';
import { take } from 'rxjs';
import { Event } from 'src/app/_models/event';
import { EventService } from 'src/app/_services/event.service';

@Component({
  selector: 'app-event-card',
  templateUrl: './event-card.component.html',
  styleUrls: ['./event-card.component.css']
})
export class EventCardComponent implements OnInit {
  @Input() event: Event | undefined;

  constructor (private eventService: EventService) {}

  ngOnInit(): void {
  }

  deleteEvent(id: string) {
    this.eventService.deleteEvent(id).pipe(take(1)).subscribe();
  }
}
