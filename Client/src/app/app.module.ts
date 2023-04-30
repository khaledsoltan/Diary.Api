import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { CoreModule } from "./Core/Core.module";
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { DiaryModule } from './Presentation/Diary.module';
import { RouterModule } from "@angular/router";



@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    CoreModule,
    AppRoutingModule,
    DiaryModule,
    RouterModule.forRoot([
      {

        path: "home",
          loadChildren: () => import("./Presentation/Diary.module")
            .then(m => m.DiaryModule),
          canActivate: []
      }
      , { path: "**", redirectTo: "/home" }
    ]),
    
  ],
  providers: [],
  bootstrap: [AppComponent ]
})
export class AppModule { }
