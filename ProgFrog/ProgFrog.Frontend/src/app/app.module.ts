import { BrowserModule } from '@angular/platform-browser';
import { HttpModule }    from '@angular/http';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { ConfigService } from './config.service'
import { ProgrammingTasksService } from './programming-tasks.service'
import { ProgrammingTasksServiceFake } from './fakes/programming-task.service-fake'

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    HttpModule,
  ],
  providers: [
  	{provide: ProgrammingTasksService, useClass: ProgrammingTasksServiceFake},
  	ConfigService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
