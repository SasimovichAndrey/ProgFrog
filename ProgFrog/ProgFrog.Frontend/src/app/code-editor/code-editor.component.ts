import {Component, Input, Output} from '@angular/core';

@Component({
    selector: 'code-editor',
    templateUrl: './code-editor.component.html',
    styleUrls: ['./code-editor.component.css'],
    providers: []
  })
  export class CodeEditorComponent{
    userCode: string;
  }
      