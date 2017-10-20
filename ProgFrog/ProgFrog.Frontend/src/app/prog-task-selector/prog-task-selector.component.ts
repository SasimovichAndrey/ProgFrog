import {Component, OnInit, Output, EventEmitter} from '@angular/core';
import {ProgrammingTask} from '../programming-task';
import {ProgrammingTasksService} from '../programming-tasks.service';

@Component({
    selector: 'prog-task-selector',
    templateUrl: './prog-task-selector.component.html',
    styleUrls: ['./prog-task-selector.component.css'],
    providers: []
  })
  export class ProgTaskSelectorComponent implements OnInit{
      @Output()
      onTaskSelected : EventEmitter<ProgrammingTask>;
      progTasks : ProgrammingTask[];
      selectedTask: ProgrammingTask;

      constructor(private programmingTaskService: ProgrammingTasksService){
          this.onTaskSelected = new EventEmitter<ProgrammingTask>();
      }

      ngOnInit(): void{
        this.progTasks = [];
        var tasksPromise = this.programmingTaskService.getAllTasks()
            .then(tasks => {
                this.progTasks = tasks;
            });
      }

      selectTask(task : ProgrammingTask ) : void{
        this.selectedTask = task;
        this.onTaskSelected.emit(task);
      }
  }