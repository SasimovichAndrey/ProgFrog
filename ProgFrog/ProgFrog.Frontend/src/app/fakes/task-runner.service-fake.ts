import { Injectable} from '@angular/core';
import { Headers, Http } from '@angular/http';
import { ProgrammingLanguage } from './../programming-language';
import { ProgrammingTask } from './../programming-task';
import { TaskRunResult } from './../task-run-result';
import { TaskRunnerService } from './../task-runner.service'

import 'rxjs/add/operator/toPromise';

@Injectable()
export class TaskRunnerServiceFake extends TaskRunnerService{
	private result : TaskRunResult = {
		isError: false
	};

	constructor(){
		super(null, null)
	}

	run(task: ProgrammingTask, userCode: string, progLanguage: ProgrammingLanguage) : Promise<TaskRunResult> {
		return Promise.resolve(this.result);
	}
}