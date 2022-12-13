import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { PaginationService } from '../../SharedServices/PaginationService';


@Injectable()
export class HttpBaseService {
  private headers = new HttpHeaders();
  private endpoint = `https://localhost:44380/api/customers/`;


}
