import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { JhiDataUtils } from 'ng-jhipster';

import { IAntwort } from 'app/shared/model/antwort.model';

@Component({
    selector: 'jhi-antwort-detail',
    templateUrl: './antwort-detail.component.html'
})
export class AntwortDetailComponent implements OnInit {
    antwort: IAntwort;

    constructor(private dataUtils: JhiDataUtils, private activatedRoute: ActivatedRoute) {}

    ngOnInit() {
        this.activatedRoute.data.subscribe(({ antwort }) => {
            this.antwort = antwort;
        });
    }

    byteSize(field) {
        return this.dataUtils.byteSize(field);
    }

    openFile(contentType, field) {
        return this.dataUtils.openFile(contentType, field);
    }
    previousState() {
        window.history.back();
    }
}
