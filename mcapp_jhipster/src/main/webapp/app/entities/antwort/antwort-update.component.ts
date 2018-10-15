import { Component, OnInit, ElementRef } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { HttpResponse, HttpErrorResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { JhiDataUtils } from 'ng-jhipster';

import { IAntwort } from 'app/shared/model/antwort.model';
import { AntwortService } from './antwort.service';

@Component({
    selector: 'jhi-antwort-update',
    templateUrl: './antwort-update.component.html'
})
export class AntwortUpdateComponent implements OnInit {
    antwort: IAntwort;
    isSaving: boolean;

    constructor(
        private dataUtils: JhiDataUtils,
        private antwortService: AntwortService,
        private elementRef: ElementRef,
        private activatedRoute: ActivatedRoute
    ) {}

    ngOnInit() {
        this.isSaving = false;
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

    setFileData(event, entity, field, isImage) {
        this.dataUtils.setFileData(event, entity, field, isImage);
    }

    clearInputImage(field: string, fieldContentType: string, idInput: string) {
        this.dataUtils.clearInputImage(this.antwort, this.elementRef, field, fieldContentType, idInput);
    }

    previousState() {
        window.history.back();
    }

    save() {
        this.isSaving = true;
        if (this.antwort.id !== undefined) {
            this.subscribeToSaveResponse(this.antwortService.update(this.antwort));
        } else {
            this.subscribeToSaveResponse(this.antwortService.create(this.antwort));
        }
    }

    private subscribeToSaveResponse(result: Observable<HttpResponse<IAntwort>>) {
        result.subscribe((res: HttpResponse<IAntwort>) => this.onSaveSuccess(), (res: HttpErrorResponse) => this.onSaveError());
    }

    private onSaveSuccess() {
        this.isSaving = false;
        this.previousState();
    }

    private onSaveError() {
        this.isSaving = false;
    }
}
