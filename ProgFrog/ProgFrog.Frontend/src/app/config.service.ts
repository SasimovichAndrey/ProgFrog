import { Injectable } from '@angular/core';

@Injectable()
export class ConfigService{
	public baseApiUrl : string = 'http://localhost:6334/api/';
}