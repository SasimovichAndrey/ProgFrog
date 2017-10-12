import { Component, OnInit } from '@angular/core';
import { ProgrammingTasksService } from './programming-tasks.service'
import { ProgrammingTask } from './programming-task'
import { ConfigService } from './config.service'

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  providers: [ProgrammingTasksService, ConfigService]
})
export class AppComponent implements OnInit{
  public progTasks : ProgrammingTask[];
  
  public constructor(private programmingTaskService : ProgrammingTasksService){
  }

	ngOnInit() : void{
		this.getTasks();
	}

  public getTasks() : void {
  	this.progTasks = [];
  	this.programmingTaskService.getAllTasks().then(tasks =>
  		 	this.progTasks = tasks
  		 );
  }
}
