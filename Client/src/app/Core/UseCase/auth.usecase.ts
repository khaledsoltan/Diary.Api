import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { AuthRepository } from "../../Core/Repository/Auth/auth.repository";
import { User } from "../../../app/Core/Domain/Auth/User.model";
import { RequestResponse } from "../../../app/Core/Domain/Respnse/RequestResponse.model";

@Injectable()
export class AuthUseCase {

    constructor(private repository: AuthRepository) { }


    authenticate(username: string, password: string): Observable<boolean> {
        return this.repository.authenticate(username, password);
    }

    get authenticated(): boolean {
        return this.repository.auth_token != null;
    }

    Register(user: User): Observable<RequestResponse> {
        debugger;
        return this.repository.Register(user);
    }

    clear() {
        this.repository.auth_token = undefined;
    }

}