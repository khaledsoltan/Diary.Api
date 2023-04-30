import { NgModule } from "@angular/core";
import { HttpClientModule } from "@angular/common/http";
import { AuthRepository } from "./Repository/Auth/auth.repository";
import { AuthUseCase } from "./UseCase/auth.usecase";

@NgModule({
    imports: [HttpClientModule],
    providers: [AuthRepository,AuthUseCase]

})

   
export class CoreModule { }
