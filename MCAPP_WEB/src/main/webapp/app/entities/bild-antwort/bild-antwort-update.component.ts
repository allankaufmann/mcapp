import { Component, OnInit, ElementRef } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { HttpResponse, HttpErrorResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { JhiAlertService, JhiDataUtils } from 'ng-jhipster';

import { IBildAntwort } from 'app/shared/model/bild-antwort.model';
import { BildAntwortService } from './bild-antwort.service';
import { IFrage } from 'app/shared/model/frage.model';
import { FrageService } from 'app/entities/frage';

@Component({
    selector: 'jhi-bild-antwort-update',
    templateUrl: './bild-antwort-update.component.html'
})
export class BildAntwortUpdateComponent implements OnInit {
    bildAntwort: IBildAntwort;
    isSaving: boolean;

    frages: IFrage[];

    constructor(
        private dataUtils: JhiDataUtils,
        private jhiAlertService: JhiAlertService,
        private bildAntwortService: BildAntwortService,
        private frageService: FrageService,
        private elementRef: ElementRef,
        private activatedRoute: ActivatedRoute
    ) {}

    ngOnInit() {
        this.isSaving = false;
        this.activatedRoute.data.subscribe(({ bildAntwort }) => {
            this.bildAntwort = bildAntwort;
        });
        this.frageService.query().subscribe(
            (res: HttpResponse<IFrage[]>) => {
                this.frages = res.body;
            },
            (res: HttpErrorResponse) => this.onError(res.message)
        );
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

    private onError(errorMessage: string) {
        this.jhiAlertService.error(errorMessage, null, null);
    }

    trackFrageById(index: number, item: IFrage) {
        return item.id;
    }
}
