import {Component, Output, EventEmitter} from '@angular/core';
import {ProgrammingTask} from '../programming-task';

@Component({
    selector: 'prog-task-selector',
    templateUrl: './prog-task-selector.component.html',
    styleUrls: [],
    providers: []
  })
  export class ProgTaskSelectorComponent{
      @Output()
      onTaskSelected : EventEmitter<ProgrammingTask>;

      constructor(){
          this.onTaskSelected = new EventEmitter<ProgrammingTask>();
      }

      public selectTask(task : ProgrammingTask ) : void{
        this.onTaskSelected.emit(task);
      }
  }