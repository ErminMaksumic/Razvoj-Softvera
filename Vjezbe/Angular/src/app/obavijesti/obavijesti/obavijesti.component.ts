import { Component, OnInit } from '@angular/core';
import {ObavijestiService} from "../obavijesti.service";
import {FormBuilder, FormGroup} from "@angular/forms";

declare function porukaSuccess(a: string):any;
declare function porukaError(a: string):any;

@Component({
  selector: 'app-obavijesti',
  templateUrl: './obavijesti.component.html',
  styleUrls: ['./obavijesti.component.css']
})
export class ObavijestiComponent implements OnInit {

  obavijesti: any;
  obavijest: any;
  formGroup: FormGroup;
  showModal: boolean = false;
  id: number = -1;

  constructor(private obavijestiService: ObavijestiService, private builder: FormBuilder) { }

  ngOnInit(): void {
    this.loadData();

    this.formInit();

  }

  formInit()
  {
    this.formGroup = this.builder.group({
      naslov: [''],
      sadrzaj: ['']
    });
  }
  loadData()
  {
    this.obavijestiService.getAll().subscribe(x=>
    {
      this.obavijesti = x;
    });
  }
  add()
  {
    this.obavijestiService.add(this.formGroup.value).subscribe(x=>
    {
      porukaSuccess("Obavijest dodata!");
      this.loadData();
      this.closeAndClearModal();
    })
  }

  edit(id: number)
  {
    this.patchValue(id);
    this.id = id;
    this.showModal=true;
  }

  saveEdit()
  {
    this.obavijestiService.update(this.id, this.formGroup.value).subscribe(x=>
    {
      porukaSuccess("Editovana obavijest id: " + x);
      this.closeAndClearModal();
      this.loadData();
    })
  }

  closeAndClearModal()
  {
    this.showModal = false;
    this.formGroup.reset();
    this.formInit();
    this.id=-1;

  }

  patchValue(id: number)
  {
    this.obavijestiService.getById(id).subscribe(x=>
    {
      this.obavijest = x;
      this.formGroup.get('naslov').patchValue(this.obavijest.naslov);
      this.formGroup.get('sadrzaj').patchValue(this.obavijest.tekst);
    });
  }

  delete(id: number)
  {
    this.obavijestiService.delete(id).subscribe(()=>
    {
      this.loadData();
      porukaError("Obrisana obavijest id: " + id);
    })
  }
}
