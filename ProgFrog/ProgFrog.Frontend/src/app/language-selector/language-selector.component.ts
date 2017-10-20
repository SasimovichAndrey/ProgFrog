import {Component, OnInit, Output, EventEmitter} from '@angular/core';
import {ProgrammingLanguage} from '../programming-language';
import {ProgrammingLanguageService} from '../programming-language.service';

@Component({
    selector: 'prog-lang-selector',
    templateUrl: './language-selector.component.html',
    styleUrls: ['./language-selector.component.css'],
    providers: []
  })
  export class ProgrammingLanguageSelectorComponent implements OnInit{
      progLanguages : ProgrammingLanguage[];
      selectedLanguage: ProgrammingLanguage;

      @Output()
      onLanguageSelected : EventEmitter<ProgrammingLanguage>;

      constructor(private programmingLanguageService: ProgrammingLanguageService){
        this.onLanguageSelected = new EventEmitter<ProgrammingLanguage>();  
      }

      ngOnInit(): void{
        this.progLanguages = [];
        this.programmingLanguageService.getAllLanguages()
            .then(langs => {
                this.progLanguages = langs;
                this.selectedLanguage = langs[0];
            });
      }

      selectionChange(event: any){
        this.onLanguageSelected.emit(this.selectedLanguage);
      }
  }