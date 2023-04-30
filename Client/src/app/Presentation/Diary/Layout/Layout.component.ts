import { Component }  from "@angular/core";
import { Router } from "@angular/router";
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
    selector: "header",
    templateUrl: "Layout.component.html"
})

export class LayoutComponent {
    currentDate = new Date();

    constructor(
        private router: Router) { }
    
}