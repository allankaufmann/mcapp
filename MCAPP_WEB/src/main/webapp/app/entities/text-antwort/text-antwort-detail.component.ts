import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { ITextAntwort } from 'app/shared/model/text-antwort.model';

@Component({
    selector: 'jhi-text-antwort-detail',
    templateUrl: './text-antwort-detail.component.html'
})
export class TextAntwortDetailComponent implements OnInit {
    textAntwort: ITextAntwort;

    constructor(private activatedRoute: ActivatedRoute) {}

    ngOnInit() {
        this.activatedRoute.data.subscribe(({ textAntwort }) => {
            this.textAntwort = textAntwort;
        });
    }

    previousState() {
        window.history.back();
    }
}
