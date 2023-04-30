import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, RouterStateSnapshot, Router } from "@angular/router";
import { AuthUseCase } from "../../../Core/UseCase/auth.usecase";


@Injectable()
export class AuthGuard {

    constructor(private router: Router,  private auth: AuthUseCase) { }


    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
        if (!this.auth.authenticated) {
            this.router.navigateByUrl("/Login");
            return false;
        }
        return true;

    }
}