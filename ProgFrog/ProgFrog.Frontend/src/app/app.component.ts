import { Component, OnInit } from '@angular/core';
import { ProgrammingTasksService } from './programming-tasks.service';
import { ProgrammingLanguageService } from './programming-language.service';
import { ProgrammingTask } from './programming-task';
import { ProgrammingLanguage } from './programming-language';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  providers: []
})
export class AppComponent implements OnInit{
  public progTasks : ProgrammingTask[];
  public selectedTask : ProgrammingTask;
  public progLanguages : ProgrammingLanguage[];
  public selectedLanguage : ProgrammingLanguage;
  
  public constructor(private programmingTaskService : ProgrammingTasksService, private programmingLanguageService : ProgrammingLanguageService){
  }

	ngOnInit() : void{
		this.getTasks();
	}

  public getTasks() : void {
  	this.progTasks = [];

    let progTasks;
  	var tasksPromise = this.programmingTaskService.getAllTasks()
      .then(tasks => progTasks = tasks);

    let progLangs;
    var langPromise = this.programmingLanguageService.getAllLanguages()
      .then(langs => progLangs = langs);

    Promise.all([tasksPromise, langPromise])
      .then(value => {
      		 	this.progTasks = progTasks;
            this.selectedTask = progTasks[0];

            this.progLanguages = progLangs;
            this.selectedLanguage = progLangs[0];
          }
  		   );
  }

  public checkTask() : void {
    
  }

  public selectTask(task : ProgrammingTask ) : void{
    this.selectedTask = task;
  }
}
