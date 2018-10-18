import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { HttpResponse, HttpErrorResponse } from '@angular/common/http';
import { Observable } from 'rxjs';

import { IThema } from 'app/shared/model/thema.model';
import { ThemaService } from './thema.service';

@Component({
    selector: 'jhi-thema-update',
    templateUrl: './thema-update.component.html'
})
export class ThemaUpdateComponent implements OnInit {
    thema: IThema;
    isSaving: boolean;

    constructor(private themaService: ThemaService, private activatedRoute: ActivatedRoute) {}

    ngOnInit() {
        this.isSaving = false;
        this.activatedRoute.data.subscribe(({ thema }) => {
            this.thema = thema;
        });
    }

    previousState() {
        window.history.back();
    }

    save() {
        this.isSaving = true;
        if (this.thema.id !== undefined) {
            this.subscribeToSaveResponse(this.themaService.update(this.thema));
        } else {
            this.subscribeToSaveResponse(this.themaService.create(this.thema));
        }
    }

    private subscribeToSaveResponse(result: Observable<HttpResponse<IThema>>) {
        result.subscribe((res: HttpResponse<IThema>) => this.onSaveSuccess(), (res: HttpErrorResponse) => this.onSaveError());
    }

    private onSaveSuccess() {
        this.isSaving = false;
        this.previousState();
    }

    private onSaveError() {
        this.isSaving = false;
    }
}
