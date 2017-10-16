import { Injectable} from '@angular/core';
import { Headers, Http } from '@angular/http';
import { ProgrammingLanguage } from './programming-language';
import { ProgrammingTask } from './programming-task';
import { TaskRunResult } from './task-run-result';
import { ConfigService } from './config.service';

import 'rxjs/add/operator/toPromise';

@Injectable()
export class TaskRunnerService{
	constructor(private http: Http, private configService : ConfigService){}

	run(task: ProgrammingTask, userCode: string, progLanguage: ProgrammingLanguage) : Promise<TaskRunResult> {
		let url = this.configService.baseApiUrl + '/taskrunner';
		let reqBody = {
			task: task,
			userCode: userCode,
			programmingLanguage: progLanguage
		};
		return this.http.post(url, reqBody).toPromise().then(response =>
				 response.json() as TaskRunResult
			);

	}
}