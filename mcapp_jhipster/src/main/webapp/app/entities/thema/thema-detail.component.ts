import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { IThema } from 'app/shared/model/thema.model';

@Component({
    selector: 'jhi-thema-detail',
    templateUrl: './thema-detail.component.html'
})
export class ThemaDetailComponent implements OnInit {
    thema: IThema;

    constructor(private activatedRoute: ActivatedRoute) {}

    ngOnInit() {
        this.activatedRoute.data.subscribe(({ thema }) => {
            this.thema = thema;
        });
    }

    previousState() {
        window.history.back();
    }
}
