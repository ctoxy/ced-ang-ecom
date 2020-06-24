import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { ClientParams } from '../shared/models/clientParams';
import { map } from 'rxjs/internal/operators/map';
import { IClient } from '../shared/models/client';
import { IPaginationClient } from '../shared/models/paginationClient';

@Injectable({
  providedIn: 'root'
})
export class ClientService {
  baseUrl = 'https://localhost:5001/api/'
  constructor(private http: HttpClient) { }
  getClients(clientParams: ClientParams) {
    let params = new HttpParams();

    if (clientParams.search) {
      params = params.append('search',clientParams.search);
    }
    params = params.append('sort', clientParams.sort);
    params = params.append('pageIndex', clientParams.pageNumber.toString());
    params = params.append('pageSize', clientParams.pageSize.toString());

    return this.http.get<IPaginationClient>(this.baseUrl + 'clients', {observe: 'response', params})
    .pipe(
      map(response => {
        return response.body;
      })
    );
  }

  getClient(id: number){
    return this.http.get<IClient>(this.baseUrl + 'clients/' + id);
  }


}
