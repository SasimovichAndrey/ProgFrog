import { Injectable} from '@angular/core';
import { Headers, Http } from '@angular/http';
import { ProgrammingLanguage } from './programming-language';
import { ConfigService } from './config.service';

import 'rxjs/add/operator/toPromise';

@Injectable()
export class ProgrammingLanguageService{
	constructor(private http: Http, private configService : ConfigService){}

	getAllLanguages() : Promise<ProgrammingLanguage[]> {
		let url = this.configService.baseApiUrl + '/programmingLanguages'
		return this.http.get(url).toPromise().then(response =>
				 response.json() as ProgrammingLanguage[]
			);

	}
}