import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { IFrage } from 'app/shared/model/frage.model';

@Component({
    selector: 'jhi-frage-detail',
    templateUrl: './frage-detail.component.html'
})
export class FrageDetailComponent implements OnInit {
    frage: IFrage;

    constructor(private activatedRoute: ActivatedRoute) {}

    ngOnInit() {
        this.activatedRoute.data.subscribe(({ frage }) => {
            this.frage = frage;
        });
    }

    previousState() {
        window.history.back();
    }
}
