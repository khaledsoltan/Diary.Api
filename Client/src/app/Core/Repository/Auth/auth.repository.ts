import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { map, Observable } from "rxjs";
import { HttpHeaders } from '@angular/common/http';
import { environment } from './../../../../environments/environment';
import { User } from "./../../Domain/Auth/User.model";
import { RequestResponse } from "./../../Domain/Respnse/RequestResponse.model";

@Injectable()
export class AuthRepository {

    baseUrl: string;
    auth_token?: string;
    constructor(private http: HttpClient) {
        this.baseUrl = `${environment.apiUrl}/authentication`;
    }

    authenticate(user: string, pass: string): Observable<boolean> {
        debugger;
        return this.http.post<any>(this.baseUrl + "/login", {
            userName: user, password: pass
        }).pipe(map(response => {
            this.auth_token = response.success ? response.accessToken : null;
            console.log(this.auth_token);
            return response.success;
        }));
    }

    Register(user: User): Observable<RequestResponse> {
        debugger;
        return this.http.post<RequestResponse>(this.baseUrl , user);
    }
    
    private getOptions() {
        return {
            headers: new HttpHeaders({
                "Authorization": `Bearer<${this.auth_token}>`
            })
        }
    }
}