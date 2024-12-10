export interface User {
    id: number;
    username: string;
    password: string;
    firstName: string;
    lastName: string;
    weight: number;
    height: number;
    age: number;
    gender: Gender
  }
  
  export interface AuthResponse {
    token: string;
  }
  
  export enum Gender{
    Male = 1,
    Female = 2,
    Other = 3
  }