import { Participant } from "./participant"

export interface Event {
    id: string
    name: string
    description: string
    date: Date
    place: string
    hostUsername: string
    hostDisplayname: string
    participants: Participant[]
  }
  
export interface NewEvent {
    name: string
    description: string
    date: Date
    place: string
}