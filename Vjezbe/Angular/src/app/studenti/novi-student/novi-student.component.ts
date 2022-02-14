import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {FormBuilder, FormGroup} from "@angular/forms";
import {HttpClient} from "@angular/common/http";
import {MojConfig} from "../../moj-config";
import {StudentService} from "../../services/student.service";

@Component({
  selector: 'app-novi-student',
  templateUrl: './novi-student.component.html',
  styleUrls: ['./novi-student.component.css']
})
export class NoviStudentComponent implements OnInit {

  formGroup: FormGroup;
  opstine: any;
  @Output() closeModal = new EventEmitter<void>();
  @Output() refresh = new EventEmitter<void>();


  constructor(private builder: FormBuilder, private http: HttpClient, private studentService: StudentService) { }

  ngOnInit(): void {

    this.formGroup = this.builder.group(
      {
        Ime: [''],
        Prezime: [''],
        OpstinaRodjenjaId: [0]
      }
    )

    this.getOpstine();
  }

  getOpstine()
  {
    this.http.get('https://localhost:44326/opstina/GetByAll', MojConfig.http_opcije()).subscribe((x:any)=>
    {
      this.opstine = x;
      console.log(this.opstine);
    })
  }

  closeMod()
  {
    this.closeModal.emit();
    this.refresh.emit();
  }

  add()
  {
    console.log(this.formGroup.value);
    this.studentService.addStudent(this.formGroup.value).subscribe(()=>
    {
      this.closeMod();
    });
  }

}
