import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Event } from '../_models/event';
import { map } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class EventService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getEvents(isGoing: boolean, isHost: boolean, startDate?: Date) {
    let params = this.getParams(isGoing, isHost, startDate);
    return this.http.get<Event[]>(this.baseUrl + 'events', {observe: 'response', params: params}).pipe(
      map(response => {
        if (response) return response.body;
        return;
      })
    )
  }

  getEvent(id: string) {
    return this.http.get<Event>(this.baseUrl + 'events/' + id)
  }

  deleteEvent(id: string) {
    return this.http.delete(this.baseUrl + 'events/' + id)
  }

  participateEvent(id: string) {
    return this.http.post(this.baseUrl + 'events/' + id + '/participate', {})
  }

  editEvent(id: string, event: Event) {
    return this.http.put(this.baseUrl + 'events/' + id, event)
  }

  saveEvent(event: Event) {
    return this.http.post(this.baseUrl + 'events', event)
  }

  private getParams(isGoing: boolean, isHost: boolean, startDate?: Date) {
    let params = new HttpParams();
    params = params.append('isGoing', isGoing);
    params = params.append('isHost', isHost);
    if (startDate) params = params.append('startDate', startDate.toDateString())

    return params;
  }
}
