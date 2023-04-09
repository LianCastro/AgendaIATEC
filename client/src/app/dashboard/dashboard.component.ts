import { Component, Input, OnInit } from '@angular/core';
import { EventService } from '../_services/event.service';
import { Event } from '../_models/event';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
  filterForm: FormGroup = new FormGroup({});
  events: Event[] | undefined;
  
  constructor(private eventService: EventService, private formBuilder: FormBuilder) {
  }

  ngOnInit(): void {
    this.loadEvents();
    this.initializeForm();
  }

  loadEvents(startDate?: Date, endDate?: Date) {
    this.eventService.getEvents(true, false, startDate, endDate).subscribe({
      next: events => {
        if (events) this.events = events;
      }
    })
  }

  initializeForm() {
    this.filterForm = this.formBuilder.group({
      startDate: [''],
      endDate: [''],
    });
  }

  filterEvents() {
    this.eventService.getEvents(true, false, this.filterForm?.value['startDate'], this.filterForm?.value['endDate']).subscribe({
      next: events => {
        if (events) this.events = events;
      }
    });
  }
}
