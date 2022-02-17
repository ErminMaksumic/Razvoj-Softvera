import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {MojConfig} from "../moj-config";

@Injectable({
  providedIn: 'root'
})
export class MaticnaKnjigaService {
  url = 'https://localhost:44326/maticnaknjiga/'

  constructor(private http: HttpClient) { }

  addGodina(studentId: number, data: any)
  {
    console.log(data);
    return this.http.post(this.url + `AddGodina?id=${studentId}`, data, MojConfig.http_opcije());
  }

  getByStudent(studentId: number)
  {
    return this.http.get(this.url + `GetByStudent?id=${studentId}`, MojConfig.http_opcije());
  }

  ovjeriSemestar(godinaId: number)
  {
    console.log(godinaId);
    return this.http.post(this.url + `ovjerisemestar`, godinaId, MojConfig.http_opcije());
  }
}
