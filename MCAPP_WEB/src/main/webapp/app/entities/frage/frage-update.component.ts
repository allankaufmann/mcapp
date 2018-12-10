import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { HttpResponse, HttpErrorResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { JhiAlertService } from 'ng-jhipster';

import { IFrage } from 'app/shared/model/frage.model';
import { FrageService } from './frage.service';
import { IThema } from 'app/shared/model/thema.model';
import { ThemaService } from 'app/entities/thema';
//import { NgForm } from '@angular/forms';
//import { ViewChild } from '@angular/core';

@Component({
    selector: 'jhi-frage-update',
    templateUrl: './frage-update.component.html'
})
export class FrageUpdateComponent implements OnInit {
    frage: IFrage;
    isSaving: boolean;

    themas: IThema[];

    constructor(
        private jhiAlertService: JhiAlertService,
        private frageService: FrageService,
        private themaService: ThemaService,
        private activatedRoute: ActivatedRoute
    ) {}

    ngOnInit() {
        this.isSaving = false;
        this.activatedRoute.data.subscribe(({ frage }) => {
            this.frage = frage;
        });
        this.themaService.query().subscribe(
            (res: HttpResponse<IThema[]>) => {
                this.themas = res.body;
            },
            (res: HttpErrorResponse) => this.onError(res.message)
        );
    }

    previousState() {
        window.history.back();
    }

    save() {
        this.isSaving = true;
        if (this.frage.id !== undefined) {
            this.subscribeToSaveResponse(this.frageService.update(this.frage));
        } else {
            this.subscribeToSaveResponse(this.frageService.create(this.frage));
        }
    }

    private subscribeToSaveResponse(result: Observable<HttpResponse<IFrage>>) {
        result.subscribe((res: HttpResponse<IFrage>) => this.onSaveSuccess(), (res: HttpErrorResponse) => this.onSaveError());
    }

    //@ViewChild('taskForm') myForm: NgForm;
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

    trackThemaById(index: number, item: IThema) {
        return item.id;
    }
}
