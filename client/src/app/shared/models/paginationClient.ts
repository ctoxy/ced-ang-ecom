import { IClient } from './client';
export interface IPaginationClient {
  pageIndex: number;
  pageSize: number;
  count: number;
  data: IClient[];
}


