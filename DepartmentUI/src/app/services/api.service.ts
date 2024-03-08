import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  url="https://localhost:44391/api/Account"
  constructor(private http:HttpClient) { }
  getAllUsers(){
   return this.http.get<any>(this.url);
  }

  getAllStudents(){
    return this.http.get<any>(`${this.url}/allStudent`)
  }

  getResult(id:any){
    return this.http.get<any>(`${this.url}/result/${id}`)
  }

  updateData(std:any){
    console.log("service"+std);
    return this.http.put<any>(`${this.url}/UpdateUserDetails`,std)
  }
}
