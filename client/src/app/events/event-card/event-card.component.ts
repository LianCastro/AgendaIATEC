import { Component, Input, OnInit, Output } from '@angular/core';
import { Router, RouterEvent } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { take, timeout } from 'rxjs';
import { Event } from 'src/app/_models/event';
import { AccountService } from 'src/app/_services/account.service';
import { EventService } from 'src/app/_services/event.service';
import { ReloadComponent } from 'src/app/_util/reload.component';

@Component({
  selector: 'app-event-card',
  templateUrl: './event-card.component.html',
  styleUrls: ['./event-card.component.css']
})
export class EventCardComponent extends ReloadComponent implements OnInit {
  @Input() event: Event | undefined;
  @Input() fromDashboard: boolean = false;

  constructor(private eventService: EventService, private toastr: ToastrService, protected override router: Router, public accountService: AccountService) {
    super(router);
  }

  override ngOnInit(): void {
  }

  deleteEvent(id: string) {
    this.eventService.deleteEvent(id).subscribe({
      next: () => {
        this.toastr.success("Evento removido com sucesso!");
        this.reloadComponent(false, 'event/list');
      }
    });
  }

  participateEvent(id: string) {
    this.eventService.participateEvent(id).subscribe({
      next: () => {
        this.toastr.success("Está participando do evento!");
        this.reloadComponent(false, '/');
      }
    });
  }
}
