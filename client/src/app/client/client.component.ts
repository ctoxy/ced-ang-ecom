import { Component, OnInit, ElementRef, ViewChild } from '@angular/core';
import { IClient } from '../shared/models/client';
import { ClientParams } from '../shared/models/clientParams';
import { ClientService } from './client.service';

@Component({
  selector: 'app-client',
  templateUrl: './client.component.html',
  styleUrls: ['./client.component.scss']
})
export class ClientComponent implements OnInit {
  /* mettre true si il n y a pas  un ngif dans le search*/ 
  @ViewChild('search',{static: false}) searchTerm: ElementRef;
  clients: IClient [];
  totalCount: number;
  clientParams = new ClientParams();
  sortOptions = [
    {name:'Alphabetical', value: 'Nom_Client'},
  ]
  constructor(private clientService: ClientService) { }

  ngOnInit(): void {
    this.getClients();
  }
  getClients(){
    this.clientService.getClients(this.clientParams).subscribe((response) => {
      console.log(response);
      this.clients = response.data;
      this.clientParams.pageNumber = response.pageIndex;
      this.clientParams.pageSize = response.pageSize;
      this.totalCount = response.count;
    }, error => {
      console.log(error);
    });
  }
  onSortSelected(sort: string){
    this.clientParams.sort = sort;
    this.getClients();
  }

  onPageChanged(event: any) {
    /*evite deux requete au serveur */
    if (this.clientParams.pageNumber !== event) {
      this.clientParams.pageNumber = event;
      this.getClients();
    }
  }

  onSearch(){
    this.clientParams.search = this.searchTerm.nativeElement.value;
    this.clientParams.pageNumber = 1;
    this.getClients();
  }

  onReset(){
    this.searchTerm.nativeElement.value = '';
    this.clientParams = new ClientParams();
    this.getClients();
  }

}
