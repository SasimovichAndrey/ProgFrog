import { Component, OnInit } from '@angular/core';
import { ProgrammingTasksService } from './programming-tasks.service';
import { ProgrammingLanguageService } from './programming-language.service';
import { TaskRunnerService } from './task-runner.service';
import { ProgrammingTask } from './programming-task';
import { ProgrammingLanguage } from './programming-language';
import { TaskRunResult } from './task-run-result'

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  providers: []
})
export class AppComponent implements OnInit{
  public selectedTask : ProgrammingTask;
  public selectedLanguage : ProgrammingLanguage;
  public taskRunResult : TaskRunResult = new TaskRunResult();
  
  public constructor(private taskRunnerService : TaskRunnerService){
  }

	ngOnInit() : void{
	}

  checkTask(userCode: string) : void {
    this.taskRunnerService.run(this.selectedTask, userCode, this.selectedLanguage)
      .then(result => this.taskRunResult = result);
  }

  selectTask(task : ProgrammingTask ) : void{
    this.selectedTask = task;
  }

  selectLanguage(lang: ProgrammingLanguage){
    this.selectedLanguage = lang;
  }
}
