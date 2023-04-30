import { Component } from "@angular/core";
import { NgForm } from "@angular/forms";
import { Router } from "@angular/router";
import { AuthUseCase } from "../../../Core/UseCase/auth.usecase";

@Component({
    templateUrl: "Login.component.html"
})

export class LoginComponent {
    username?: string;
    password?: string;
    errorMessage?: string;

    constructor(private router: Router,  private auth: AuthUseCase) { }


    authenticate(form: NgForm) {
        if (form.valid) {
            // perform authentication
            this.auth.authenticate(this.username ?? "", this.password ?? "")
                .subscribe(response => {
                    this.router.navigateByUrl("/home");
                   
                })
        } else {
            this.errorMessage = "Form Data Invalid";
        }
    }
}