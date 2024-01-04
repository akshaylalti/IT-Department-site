import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { ApiService } from '../services/api.service';
@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {

  users:any=[];
  loginUser:any = {};
  val1:boolean =true;
  val2:boolean = false;
  val3:boolean = false;
  constructor(private route:Router,
    public auth:AuthService ,
    private api:ApiService
    ) { }

  ngOnInit(): void {
    // this.api.getAllUser().subscribe(
    //   res=>{this.users=res
    //   console.log(res);
    //   },
    //   err=>{console.log("User can not accessable")}
    // )

    this.auth.getLoginUserDetails().subscribe(
      res=>this.loginUser=res,
      err=>console.log("login user can not accesible")
    )
  }

  signOut(){
    this.auth.signOut();
  }

  Class(){
    this.val1=true;
    this.val2=false;
    this.val3 =false;
  }
  lab(){
    this.val1=false;
    this.val2=true;
    this.val3 =false;
  }
  Excilence(){
    this.val1=false;
    this.val2=false;
    this.val3 =true;
  }
}
