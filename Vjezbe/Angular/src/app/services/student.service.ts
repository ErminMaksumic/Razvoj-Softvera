import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {MojConfig} from "../moj-config";

@Injectable({
  providedIn: 'root'
})
export class StudentService {

  url = 'https://localhost:44326/student/'
  constructor(private http: HttpClient) { }

  getAll()
  {
    return this.http.get(this.url + 'GetAll', MojConfig.http_opcije());
  }

  addStudent(s:any)
  {
    return this.http.post(this.url + 'Add', s,  MojConfig.http_opcije());
  }

  update(s:any)
  {
    return this.http.put(this.url + 'Edit', s,  MojConfig.http_opcije());
  }
}
