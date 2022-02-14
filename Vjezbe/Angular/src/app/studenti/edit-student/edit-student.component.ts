import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {FormBuilder, FormGroup} from "@angular/forms";
import {HttpClient} from "@angular/common/http";
import {StudentService} from "../../services/student.service";
import {MojConfig} from "../../moj-config";

@Component({
  selector: 'app-edit-student',
  templateUrl: './edit-student.component.html',
  styleUrls: ['./edit-student.component.css']
})
export class EditStudentComponent implements OnInit {

  formGroup: FormGroup;
  opstine: any;
  @Input() editStudent:any;
  @Output() closeModal = new EventEmitter<void>();
  @Output() refresh = new EventEmitter<void>();

  constructor(private builder: FormBuilder, private http: HttpClient, private studentService: StudentService) { }

  ngOnInit(): void {
    this.formGroup = this.builder.group(
      {
        Id: [this.editStudent.id],
        Ime: [''],
        Prezime: [''],
        OpstinaRodjenjaId: [0]
      });

    this.getOpstine();
    this.patchValue();

  }

  patchValue()
  {
    console.log(this.editStudent);
    this.formGroup.get('Ime').patchValue(this.editStudent.ime);
    this.formGroup.get('Prezime').patchValue(this.editStudent.prezime);
    this.formGroup.get('OpstinaRodjenjaId').patchValue(this.editStudent.opstina_rodjenja_id);
  }

  getOpstine()
  {
    this.http.get('https://localhost:44326/opstina/GetByAll', MojConfig.http_opcije()).subscribe((x:any)=>
    {
      this.opstine = x;
      console.log(this.opstine);
    })
  }

  update()
  {
    console.log(this.formGroup.value);
    this.studentService.update(this.formGroup.value).subscribe(()=>
    {
      this.closeMod();
    });
  }


  closeMod()
  {
    this.closeModal.emit();
    this.refresh.emit();
  }
}
