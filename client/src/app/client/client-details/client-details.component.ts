import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BreadcrumbService } from 'xng-breadcrumb';
import { IClient } from 'src/app/shared/models/client';
import { ClientService } from '../client.service';

@Component({
  selector: 'app-client-details',
  templateUrl: './client-details.component.html',
  styleUrls: ['./client-details.component.scss']
})
export class ClientDetailsComponent implements OnInit {

  client: IClient;
  constructor(private clientService: ClientService,
              private activatedRoute: ActivatedRoute,
              private bcService: BreadcrumbService) {
                this.bcService.set('@clientDetails', '');
               }

  ngOnInit(): void {
    this.loadClient();
  }
  loadClient(){
    // le plus + permet de caster url le chiffre id qui est une string dans url
    this.clientService.getClient(+this.activatedRoute.snapshot.paramMap.get('id')).subscribe(client => {
      this.client = client;
      this.bcService.set('@clientDetails', client.nom_Client);
    }, error => {
      console.log(error);
    });
  }

}
