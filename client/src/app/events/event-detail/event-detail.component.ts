import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, NgForm, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Event } from 'src/app/_models/event';
import { EventService } from 'src/app/_services/event.service';
import { ReloadComponent } from 'src/app/_util/reload.component';

@Component({
  selector: 'app-event-detail',
  templateUrl: './event-detail.component.html',
  styleUrls: ['./event-detail.component.css']
})
export class EventDetailComponent extends ReloadComponent implements OnInit {
  editForm: FormGroup = new FormGroup({});
  //@ViewChild('editForm') editForm: NgForm | undefined;
  event: Event | undefined;

  constructor(private eventService: EventService, private formBuilder: FormBuilder, private route: ActivatedRoute, private toastr: ToastrService, protected override router: Router) {
    super(router);
  }

  override ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) this.loadEvent(id);
  }

  initializeForm(event: Event) {
    this.editForm = this.formBuilder.group({
      name: [event.name, Validators.required],
      description: [event.description, Validators.required],
      place: [event.place, Validators.required],
      date: [new Date(event.date), Validators.required]
    });
  }

  loadEvent(id: string) {
    this.eventService.getEvent(id).subscribe({
      next: event => {
        this.event = event;
        this.initializeForm(this.event);
      }
    });
  }

  editEvent(id: string) {
    this.eventService.editEvent(id, this.editForm?.value).subscribe({
      next: () => {
        this.toastr.success("Evento editado com sucesso!");
        this.editForm?.reset(this.event);
        this.reloadComponent(false, 'event/list');
      }
    });
  }
}
