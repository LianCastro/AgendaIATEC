import { Participant } from "./participant"

export interface Event {
    id: string
    name: string
    description: string
    date: Date
    place: string
    hostUsername: string
    participants: Participant[]
  }
  
