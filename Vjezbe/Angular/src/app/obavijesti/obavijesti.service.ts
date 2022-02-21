import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {MojConfig} from "../moj-config";

@Injectable({
  providedIn: 'root'
})
export class ObavijestiService {

  url = 'https://localhost:44326/obavijesti/';
  constructor(private http: HttpClient) { }

  add(data: any)
  {
    return this.http.post(this.url + 'Add', data, MojConfig.http_opcije());
  }

  getAll()
  {
    return this.http.get(this.url + 'GetAll', MojConfig.http_opcije());
  }

  getById(id: number)
  {
    return this.http.get(this.url + `GetById/${id}`, MojConfig.http_opcije());
  }

  update(id: number, data:any)
  {
    return this.http.put(this.url + `Update/${id}`, data, MojConfig.http_opcije());
  }

  delete(id: number)
  {
    return this.http.delete(this.url + `Delete/${id}`, MojConfig.http_opcije());
  }
}
