import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { map, Observable } from "rxjs";
import { HttpHeaders } from '@angular/common/http';
import { environment } from './../../../../environments/environment';




@Injectable()
export class AuthRepository {
    baseUrl: string;
 
    constructor(private http: HttpClient) {
        this.baseUrl = `${environment.apiUrl}/Diaries`;
    }
    

}