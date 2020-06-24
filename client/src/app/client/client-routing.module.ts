import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ClientDetailsComponent } from './client-details/client-details.component';
import { ClientComponent } from './client.component';


const routes: Routes = [
  {path: '', component:ClientComponent},
  {path: ':id', component:ClientDetailsComponent,data: {breadcrumb: {alias: 'clientDetails'}}},
];
@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(routes)
  ],
  exports:[
    RouterModule
  ]
})
export class ClientRoutingModule { }
