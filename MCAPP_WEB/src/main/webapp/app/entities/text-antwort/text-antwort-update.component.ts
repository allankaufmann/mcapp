import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { HttpResponse, HttpErrorResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { JhiAlertService } from 'ng-jhipster';

import { ITextAntwort } from 'app/shared/model/text-antwort.model';
import { TextAntwortService } from './text-antwort.service';
import { IFrage } from 'app/shared/model/frage.model';
import { FrageService } from 'app/entities/frage';

@Component({
    selector: 'jhi-text-antwort-update',
    templateUrl: './text-antwort-update.component.html'
})
export class TextAntwortUpdateComponent implements OnInit {
    textAntwort: ITextAntwort;
    isSaving: boolean;

    frages: IFrage[];

    constructor(
        private jhiAlertService: JhiAlertService,
        private textAntwortService: TextAntwortService,
        private frageService: FrageService,
        private activatedRoute: ActivatedRoute
    ) {}

    ngOnInit() {
        this.isSaving = false;
        this.activatedRoute.data.subscribe(({ textAntwort }) => {
            this.textAntwort = textAntwort;
        });
        this.frageService.query().subscribe(
            (res: HttpResponse<IFrage[]>) => {
                this.frages = res.body;
            },
            (res: HttpErrorResponse) => this.onError(res.message)
        );
    }

    previousState() {
        window.history.back();
    }

    save() {
        this.isSaving = true;
        if (this.textAntwort.id !== undefined) {
            this.subscribeToSaveResponse(this.textAntwortService.update(this.textAntwort));
        } else {
            this.subscribeToSaveResponse(this.textAntwortService.create(this.textAntwort));
        }
    }

    private subscribeToSaveResponse(result: Observable<HttpResponse<ITextAntwort>>) {
        result.subscribe((res: HttpResponse<ITextAntwort>) => this.onSaveSuccess(), (res: HttpErrorResponse) => this.onSaveError());
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
