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

    this.createGodineForm();
    this.studentForm = this.builder.group({
      StudentId: [0],
      StudentIme: [''],
      StudentPrezime: ['']
    });

    this.getAkGodine();
    this.getStudentInfo();
    this.loadInformations();


  }

  createGodineForm()
  {
    this.godinaForm = this.builder.group({
      Datum: [''],
      GodinaStudija: [0],
      AkademskaGodinaId: [0],
      CijenaSkolarine: [0],
      obnovaGodine: [false]
    });
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

  getAkGodine()
  {
    this.studentService.getAkGodine().subscribe(x=>
    {
      this.akGodine = x;
    })
  }


  loadInformations()
    {
      this.sub = this.maticnaKnjigaService.getByStudent(this.studentId).subscribe((x:any)=>
      {
        this.studentGodine = x;
      })
    }

    patchValue()
    {
      this.studentForm.get('StudentIme').patchValue(this.student.ime);
      this.studentForm.get('StudentPrezime').patchValue(this.student.prezime);
      this.studentForm.get('StudentId').patchValue(this.student.id);
    }

   saveChanges()
   {
     this.maticnaKnjigaService.addGodina(this.studentId, this.godinaForm.value).subscribe((x)=>
     {
       porukaSuccess("Dodata godina!");
       this.loadInformations();
       this.showModal=false;
       this.godinaForm.reset();
       this.createGodineForm();
     },
       err=>
       {
         porukaError("Ne mozete dodati postojecu godinu bez obnove!");
       })
   }

   ovjeriSemestar(g: any)
   {
      this.maticnaKnjigaService.ovjeriSemestar(g.id).subscribe(x=>
      {
        this.loadInformations();
      })
   }

   showModall()
   {
     this.showModal=true;
   }

   closeModal()
   {
     this.showModal = false;
     this.godinaForm.reset();
     this.createGodineForm();
   }

  ngOnDestroy() {
    this.sub.unsubscribe();
  }

}
