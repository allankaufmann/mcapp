import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { HttpResponse, HttpErrorResponse } from '@angular/common/http';
import { Observable } from 'rxjs';

import { ITextAntwort } from 'app/shared/model/text-antwort.model';
import { TextAntwortService } from './text-antwort.service';

@Component({
    selector: 'jhi-text-antwort-update',
    templateUrl: './text-antwort-update.component.html'
})
export class TextAntwortUpdateComponent implements OnInit {
    textAntwort: ITextAntwort;
    isSaving: boolean;

    constructor(private textAntwortService: TextAntwortService, private activatedRoute: ActivatedRoute) {}

    ngOnInit() {
        this.isSaving = false;
        this.activatedRoute.data.subscribe(({ textAntwort }) => {
            this.textAntwort = textAntwort;
        });
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
}
