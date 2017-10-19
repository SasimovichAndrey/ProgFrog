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
import { TaskRunnerService } from './task-runner.service';
import { TaskRunnerServiceFake } from './fakes/task-runner.service-fake'
import { AppHeaderComponent } from './app-header/app-header.component'
import { ProgTaskSelectorComponent } from './prog-task-selector/prog-task-selector.component'
import {MatSidenavModule, MatSelectModule} from '@angular/material';
import {NoopAnimationsModule} from '@angular/platform-browser/animations';
import {MatListModule} from '@angular/material';

@NgModule({
  declarations: [
    AppComponent,
    AppHeaderComponent,
    ProgTaskSelectorComponent
  ],
  imports: [
    BrowserModule,
    HttpModule,
    FormsModule,
    MatSidenavModule,
    MatSelectModule,
    MatListModule,
    NoopAnimationsModule
  ],
  providers: [
  	{provide: ProgrammingTasksService, useClass: ProgrammingTasksServiceFake},
    {provide: ProgrammingLanguageService, useClass: ProgrammingLanguageServiceFake},
    {provide: TaskRunnerService, useClass: TaskRunnerServiceFake},
  	ConfigService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
