
import { Component } from "@angular/core";
import { NgForm } from "@angular/forms";
import { Router } from "@angular/router";
import { User } from "./../../../Core/Domain/Auth/User.model";
import { AuthUseCase } from "./../../../Core/UseCase/auth.usecase";


@Component({
    templateUrl: "Register.component.html"
})

export class RegisterComponent {
    userRegisterData: User = new User();
    errorMessage?: string;

    constructor(private auth: AuthUseCase, private router: Router) {

    }


    save(form: NgForm) {
        if (form.valid) {
            debugger;
            this.auth.Register(this.userRegisterData)
                .subscribe(r => {
                    console.log(r);
                    this.router.navigateByUrl("/login");
                })
        }else {
            this.errorMessage = "Form Data Invalid";
        }
       
    }
    

}
