import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule} from "@angular/forms"
import { AppComponent } from './app.component';
import { HttpClientModule} from "@angular/common/http";
import { RouterModule } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { RegistracijaComponent } from './registracija/registracija.component';
import { HomeStudentComponent } from './home-student/home-student.component';
import { HomeComponent } from './home/home.component';
import {AutorizacijaStudentskaSluzbaProvjera} from "./_guards/autorizacija-studentska-sluzba-provjera.service";
import {AutorizacijaLoginProvjera} from "./_guards/autorizacija-login-provjera.service";
import { NotFoundComponent } from './not-found/not-found.component';
import {AutorizacijaAdminProvjera} from "./_guards/autorizacija-admin-provjera.service";
import {AutorizacijaProdekanProvjera} from "./_guards/autorizacija-prodekan-provjera.service";
import { HomeStudentskaSluzbaComponent } from './home-studentska-sluzba/home-studentska-sluzba.component';
import { HomeProdekanComponent } from './home-prodekan/home-prodekan.component';
import { HomeDekanComponent } from './home-dekan/home-dekan.component';
import { HomeNastavnikComponent } from './home-nastavnik/home-nastavnik.component';
import { HomeAdministratorComponent } from './home-administrator/home-administrator.component';
import { StudentMaticnaknjigaComponent } from './student-maticnaknjiga/student-maticnaknjiga.component';
import { ChartsModule } from 'ng2-charts';
import { NoviStudentComponent } from './studenti/novi-student/novi-student.component';
import { StudentiComponent } from './studenti/studenti.component';
import {ReactiveFormsModule} from "@angular/forms";
import {CommonModule} from "@angular/common";
import { EditStudentComponent } from './studenti/edit-student/edit-student.component';


@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    RegistracijaComponent,
    HomeStudentComponent,
    HomeComponent,
    NotFoundComponent,
    HomeStudentskaSluzbaComponent,
    HomeProdekanComponent,
    HomeDekanComponent,
    HomeNastavnikComponent,
    HomeAdministratorComponent,
    StudentMaticnaknjigaComponent,
    NoviStudentComponent,
    StudentiComponent,
    EditStudentComponent

  ],
  imports: [
    BrowserModule,
    RouterModule.forRoot([
      {path: 'studenti', component: StudentiComponent, canActivate: [AutorizacijaStudentskaSluzbaProvjera]},
      {path: 'login', component: LoginComponent},
      {path: 'registracija', component: RegistracijaComponent},
      {path: 'student-maticnaknjiga/:id', component: StudentMaticnaknjigaComponent, canActivate: [AutorizacijaStudentskaSluzbaProvjera]},
      {path: 'home', component: HomeComponent},
      {path: '**', component: NotFoundComponent, canActivate: [AutorizacijaLoginProvjera]},
    ]),
    FormsModule,
    HttpClientModule,
    ChartsModule,
    CommonModule,
    ReactiveFormsModule,
  ],
  providers: [
    AutorizacijaAdminProvjera,
    AutorizacijaLoginProvjera,
    AutorizacijaStudentskaSluzbaProvjera,
    AutorizacijaProdekanProvjera
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
