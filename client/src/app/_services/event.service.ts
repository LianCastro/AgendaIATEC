import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Event } from '../_models/event';

@Injectable({
  providedIn: 'root'
})
export class EventService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getEvents() {
    return this.http.get<Event[]>(this.baseUrl + 'events')
  }

  getEvent(id: string) {
    return this.http.get<Event>(this.baseUrl + 'events/' + id)
  }

  deleteEvent(id: string) {
    return this.http.delete(this.baseUrl + 'events/' + id)
  }
}
