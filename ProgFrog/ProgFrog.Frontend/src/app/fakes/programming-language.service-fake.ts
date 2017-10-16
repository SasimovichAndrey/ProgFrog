import { Injectable} from '@angular/core';
import { ProgrammingLanguage } from './../programming-language';
import { ProgrammingLanguageService } from './../programming-language.service'

import 'rxjs/add/operator/toPromise';

@Injectable()
export class ProgrammingLanguageServiceFake extends ProgrammingLanguageService{
	private languages : ProgrammingLanguage[] = [
		{Id: 1, Name: "CSharp"}
	];

	constructor(){
		super(null, null);
	}

	getAllLanguages() : Promise<ProgrammingLanguage[]> {
		return Promise.resolve(this.languages);
	}
}