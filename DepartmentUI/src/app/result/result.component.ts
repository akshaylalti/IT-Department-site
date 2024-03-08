import { Component, OnInit } from '@angular/core';
import { ApiService } from '../services/api.service';

@Component({
  selector: 'app-result',
  templateUrl: './result.component.html',
  styleUrls: ['./result.component.css']
})
export class ResultComponent implements OnInit {
  credential: { Id: string } = { Id: '' };
  search:boolean = true;
  result:boolean =false;
  marksheet:any;
  constructor(private api:ApiService) { }

  ngOnInit(): void {
  }
  onSubmit() {

    this.api.getResult(this.credential.Id).subscribe(
      (res)=>{
        this.marksheet=res
        this.result = true;
        this.search = false;
      },
      (err)=>alert("Invalid Roll No")
    )
    console.log('Submitted data:', this.credential);
  }
}
