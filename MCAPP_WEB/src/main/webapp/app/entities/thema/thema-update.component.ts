import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { HttpResponse, HttpErrorResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { JhiAlertService } from 'ng-jhipster';

import { IThema } from 'app/shared/model/thema.model';
import { ThemaService } from './thema.service';
import { IFrage } from 'app/shared/model/frage.model';
import { FrageService } from 'app/entities/frage';

@Component({
    selector: 'jhi-thema-update',
    templateUrl: './thema-update.component.html'
})
export class ThemaUpdateComponent implements OnInit {
    thema: IThema;
    isSaving: boolean;

    frages: IFrage[];

    constructor(
        private jhiAlertService: JhiAlertService,
        private themaService: ThemaService,
        private frageService: FrageService,
        private activatedRoute: ActivatedRoute
    ) {}

    ngOnInit() {
        this.isSaving = false;
        this.activatedRoute.data.subscribe(({ thema }) => {
            this.thema = thema;
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

    private onError(errorMessage: string) {
        this.jhiAlertService.error(errorMessage, null, null);
    }

    trackFrageById(index: number, item: IFrage) {
        return item.id;
    }
}
