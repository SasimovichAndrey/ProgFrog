import { Injectable} from '@angular/core';
import { ProgrammingTask } from './../programming-task';
import { ProgrammingTasksService } from './../programming-tasks.service'

import 'rxjs/add/operator/toPromise';

@Injectable()
export class ProgrammingTasksServiceFake extends ProgrammingTasksService{
	private tasks : ProgrammingTask[] = [
		{Description: 'fake task 1'},
		{Description: 'fake task 2'}
	];

	constructor(){
		super(null, null);
	}

	getAllTasks() : Promise<ProgrammingTask[]> {
		return Promise.resolve(this.tasks);
	}
}