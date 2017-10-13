import { Injectable} from '@angular/core';
import { Headers, Http } from '@angular/http';
import { ProgrammingTask } from './programming-task';
import { ConfigService } from './config.service';

import 'rxjs/add/operator/toPromise';

@Injectable()
export class ProgrammingTasksService{
	constructor(private http: Http, private configService : ConfigService){}

	getAllTasks() : Promise<ProgrammingTask[]> {
		let url = this.configService.baseApiUrl + '/programmingtasks'
		return this.http.get(url).toPromise().then(response =>
				 response.json() as ProgrammingTask[]
			);
	}
}