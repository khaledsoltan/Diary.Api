import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LayoutComponent }  from './Diary/Layout/Layout.component'
import { RouterModule } from "@angular/router";
import { LoginComponent }  from './../Presentation/Auth/Login/Login.component'
import { FormsModule } from "@angular/forms";
import { AuthGuard } from './Auth/Login/auth.guard';
import { RegisterComponent } from './../Presentation/Auth/Register/Register.component';



let routing = RouterModule.forChild([
    { path: "login", component: LoginComponent },
    { path: "register", component: RegisterComponent },

    {
        path: "home", component: LayoutComponent, canActivate: [AuthGuard],
        children: [
                
        ]
    },
    { path: "**", redirectTo: "login" }
]);

@NgModule({
    declarations: [LayoutComponent , LoginComponent, RegisterComponent ],providers: [AuthGuard],
    imports: [
        CommonModule,
        FormsModule,
        routing
    ],
})
export class DiaryModule { }
