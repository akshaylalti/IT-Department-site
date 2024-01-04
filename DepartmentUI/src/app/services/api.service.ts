import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  url="https://localhost:44391/api/Account"
  constructor(private http:HttpClient) { }
  getAllUser(){
   return this.http.get<any>(this.url);
  }


}
