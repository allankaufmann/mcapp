import { Component, OnInit, ElementRef } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { HttpResponse, HttpErrorResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { JhiDataUtils } from 'ng-jhipster';

import { IBildAntwort } from 'app/shared/model/bild-antwort.model';
import { BildAntwortService } from './bild-antwort.service';

@Component({
    selector: 'jhi-bild-antwort-update',
    templateUrl: './bild-antwort-update.component.html'
})
export class BildAntwortUpdateComponent implements OnInit {
    bildAntwort: IBildAntwort;
    isSaving: boolean;

    constructor(
        private dataUtils: JhiDataUtils,
        private bildAntwortService: BildAntwortService,
        private elementRef: ElementRef,
        private activatedRoute: ActivatedRoute
    ) {}

    ngOnInit() {
        this.isSaving = false;
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

    setFileData(event, entity, field, isImage) {
        this.dataUtils.setFileData(event, entity, field, isImage);
    }

    clearInputImage(field: string, fieldContentType: string, idInput: string) {
        this.dataUtils.clearInputImage(this.bildAntwort, this.elementRef, field, fieldContentType, idInput);
    }

    previousState() {
        window.history.back();
    }

    save() {
        this.isSaving = true;
        if (this.bildAntwort.id !== undefined) {
            this.subscribeToSaveResponse(this.bildAntwortService.update(this.bildAntwort));
        } else {
            this.subscribeToSaveResponse(this.bildAntwortService.create(this.bildAntwort));
        }
    }

    private subscribeToSaveResponse(result: Observable<HttpResponse<IBildAntwort>>) {
        result.subscribe((res: HttpResponse<IBildAntwort>) => this.onSaveSuccess(), (res: HttpErrorResponse) => this.onSaveError());
    }

    private onSaveSuccess() {
        this.isSaving = false;
        this.previousState();
    }

    private onSaveError() {
        this.isSaving = false;
    }
}
