import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { EventService } from 'src/app/_services/event.service';
import { ReloadComponent } from 'src/app/_util/reload.component';

@Component({
  selector: 'app-event-new',
  templateUrl: './event-new.component.html',
  styleUrls: ['./event-new.component.css']
})
export class EventNewComponent extends ReloadComponent implements OnInit {
  createForm: FormGroup = new FormGroup({});

  constructor(private eventService: EventService, private formBuilder: FormBuilder, private toastr: ToastrService, protected override router: Router) {
    super(router);
  }

  override ngOnInit(): void {
    this.initializeForm();
  }

  initializeForm() {
    this.createForm = this.formBuilder.group({
      name: ['', Validators.required],
      description: ['', Validators.required],
      place: ['', Validators.required],
      date: ['', Validators.required]
    });
  }

  saveEvent() {
    this.eventService.saveEvent(this.createForm?.value).subscribe({
      next: () => {
        this.toastr.success("Evento criado com sucesso!");
        this.createForm?.reset(this.createForm.value);
        this.reloadComponent(false, 'event/list');
      }
    });
  }
}
