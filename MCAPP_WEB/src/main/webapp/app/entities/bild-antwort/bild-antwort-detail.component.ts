import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { JhiDataUtils } from 'ng-jhipster';

import { IBildAntwort } from 'app/shared/model/bild-antwort.model';

@Component({
    selector: 'jhi-bild-antwort-detail',
    templateUrl: './bild-antwort-detail.component.html'
})
export class BildAntwortDetailComponent implements OnInit {
    bildAntwort: IBildAntwort;

    constructor(private dataUtils: JhiDataUtils, private activatedRoute: ActivatedRoute) {}

    ngOnInit() {
        this.activatedRoute.data.subscribe(({ bildAntwort }) => {
            this.bildAntwort = bildAntwort;
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
