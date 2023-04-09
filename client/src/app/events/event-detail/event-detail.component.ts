import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Event } from 'src/app/_models/event';
import { EventService } from 'src/app/_services/event.service';

@Component({
  selector: 'app-event-detail',
  templateUrl: './event-detail.component.html',
  styleUrls: ['./event-detail.component.css']
})
export class EventDetailComponent implements OnInit{
  event: Event | undefined;
  
  constructor(private eventService: EventService, private route: ActivatedRoute) {}

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) this.loadEvent(id);
  }

  loadEvent(id: string) {
    this.eventService.getEvent(id).subscribe({
      next: event => this.event = event
    });
  }
}
