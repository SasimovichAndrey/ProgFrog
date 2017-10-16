import { BrowserModule } from '@angular/platform-browser';
import { HttpModule }    from '@angular/http';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';

import { AppComponent } from './app.component';
import { ConfigService } from './config.service'
import { ProgrammingTasksService } from './programming-tasks.service'
import { ProgrammingTasksServiceFake } from './fakes/programming-task.service-fake'
import { ProgrammingLanguageService } from './programming-language.service'
import { ProgrammingLanguageServiceFake } from './fakes/programming-language.service-fake'

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    HttpModule,
    FormsModule
  ],
  providers: [
  	{provide: ProgrammingTasksService, useClass: ProgrammingTasksServiceFake},
    {provide: ProgrammingLanguageService, useClass: ProgrammingLanguageServiceFake},
  	ConfigService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
