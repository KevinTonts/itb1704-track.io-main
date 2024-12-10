import { WeekDay } from "./nutrition";

export interface Exercise {
  id: number;
  title: string;
  description: string;
  date?: string;
  burnedCalories: number;
  exerciseFinished: boolean;
  weekDay: DayOfTheWeek;
}

export interface State {
  exercises: Exercise[];
}

export enum DayOfTheWeek{
  Monday = "Esmaspäev",
  Tuesday = "Teisipäev",
  Wednesday = "Kolmapäev",
  Thursday = "Neljapäev",
  Friday = "Reede",
  Saturday = "Laupäev",
  Sunday = "Pühapäev"
}
