import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ClientComponent } from './client.component';
import { ClientItemComponent } from './client-item/client-item.component';
import { ClientDetailsComponent } from './client-details/client-details.component';
import { SharedModule } from '../shared/shared.module';
import { ClientRoutingModule } from './client-routing.module';

@NgModule({
  declarations: [ClientComponent, ClientItemComponent, ClientDetailsComponent],
  imports: [
    CommonModule,
    SharedModule,
    ClientRoutingModule
  ]
})
export class ClientModule { }
