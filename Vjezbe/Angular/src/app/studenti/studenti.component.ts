import { Component, OnInit } from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {MojConfig} from "../moj-config";
import {Router} from "@angular/router";
import {StudentService} from "../services/student.service";
import {startWith} from "rxjs";

declare function porukaSuccess(a: string):any;
declare function porukaError(a: string):any;

@Component({
  selector: 'app-studenti',
  templateUrl: './studenti.component.html',
  styleUrls: ['./studenti.component.css']
})
export class StudentiComponent implements OnInit {

  title: string = 'angularFIT2';
  showNewStudentModal = false;
  showEditStudentModal : any;
  studenti: any;
  filter = '';

  constructor(private studentService: StudentService, private router: Router) {
  }

  ngOnInit(): void {
   this.loadStudents();
  }

  loadStudents()
  {
    this.studentService.getAll().subscribe((x:any) => {
      this.studenti = x;
      console.log(this.studenti);
    })
  }
  setBoolNew()
  {
    this.showNewStudentModal = true;
  }

  update(s: any)
  {
    this.showEditStudentModal = s;
  }

  filtriraj()
  {
    return this.studenti.filter((x:any)=>
      (x.ime + ' ' + x.prezime).toLowerCase().startsWith(this.filter.toLowerCase()) ||
      (x.prezime + ' ' + x.ime).toLowerCase().startsWith(this.filter.toLowerCase()))
  };

  closing()
  {
    this.showNewStudentModal=false;
  }

}
