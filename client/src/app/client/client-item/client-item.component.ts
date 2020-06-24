import { Component, OnInit, Input } from '@angular/core';
import { IClient } from 'src/app/shared/models/client';

@Component({
  selector: 'app-client-item',
  templateUrl: './client-item.component.html',
  styleUrls: ['./client-item.component.scss']
})
export class ClientItemComponent implements OnInit {
  @Input() client:IClient;
  constructor() { }

  ngOnInit(): void {
  }

}
