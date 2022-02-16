import {Component, OnDestroy, OnInit} from '@angular/core';
import {ActivatedRoute} from "@angular/router";
import {StudentService} from "../services/student.service";
import {FormBuilder, FormGroup} from "@angular/forms";
import {MaticnaKnjigaService} from "../services/maticna-knjiga.service";

declare function porukaSuccess(a: string):any;
declare function porukaError(a: string):any;

@Component({
  selector: 'app-student-maticnaknjiga',
  templateUrl: './student-maticnaknjiga.component.html',
  styleUrls: ['./student-maticnaknjiga.component.css']
})
export class StudentMaticnaknjigaComponent implements OnInit, OnDestroy {

  sub: any;
  studentForm: FormGroup;
  godinaForm: FormGroup;
  studentId: number;
  student: any;
  studentGodine: any;
  showModal: boolean;
  akGodine: any;


  constructor(private studentService: StudentService, private aRoute: ActivatedRoute, private builder: FormBuilder,
              private maticnaKnjigaService: MaticnaKnjigaService) {}

  ngOnInit() {


      this.getStudentInfo();
      this.getAkGodine();
      this.createStudentInfoForm();
      this.createAddGodinaForm();
      this.loadInformations();
    }


    getStudentInfo()
    {
      this.aRoute.params.subscribe(x=>
      {
        this.studentId = +x['id'];
        this.studentService.getById(this.studentId).subscribe(x=>
        {
          this.student=x;
          this.patchValue();

        })
      })
    }


  createStudentInfoForm()
  {
    this.studentForm = this.builder.group({
      StudentId: [this.studentId],
      StudentIme: [''],
      StudentPrezime: ['']
    });
  }
  createAddGodinaForm()
  {
    this.godinaForm = this.builder.group({
      Datum: [''],
      GodinaStudija: [0],
      AkademskaGodinaId: [0],
      CijenaSkolarine: [0],
      ObnovaGodine: [0]
    });
  }

  getAkGodine()
  {
    this.studentService.getAkGodine().subscribe(x=>
    {
      this.akGodine = x;
    })
  }


  loadInformations()
    {
      this.maticnaKnjigaService.getByStudent(this.studentId).subscribe((x:any)=>
      {
        this.studentGodine = x;
        console.log(this.studentGodine);
      })
    }

    patchValue()
    {
      this.studentForm.get('StudentIme').patchValue(this.student.ime);
      this.studentForm.get('StudentPrezime').patchValue(this.student.prezime);
    }

   saveChanges()
   {
     this.maticnaKnjigaService.addGodina(this.studentId, this.godinaForm.value).subscribe(()=>
     {
       porukaSuccess("Dodata godina!");
       this.loadInformations();
       this.godinaForm.reset();
       this.showModal=false;
     },
       err=>
       {
         porukaError("Ne mozete dodati postojecu godinu bez obnove!");
         this.godinaForm.reset();
         this.showModal=false;
       })
   }

   showModall()
   {
     this.showModal=true;
   }

   closeModal()
   {
     this.showModal = false;
   }

  ngOnDestroy() {
    this.sub.unsubscribe();
  }



}
