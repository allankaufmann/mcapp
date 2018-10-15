import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { HttpResponse, HttpErrorResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { JhiAlertService } from 'ng-jhipster';

import { IFrage } from 'app/shared/model/frage.model';
import { FrageService } from './frage.service';
import { IAntwort } from 'app/shared/model/antwort.model';
import { AntwortService } from 'app/entities/antwort';
import { IQuizFrage } from 'app/shared/model/quiz-frage.model';
import { QuizFrageService } from 'app/entities/quiz-frage';

@Component({
    selector: 'jhi-frage-update',
    templateUrl: './frage-update.component.html'
})
export class FrageUpdateComponent implements OnInit {
    frage: IFrage;
    isSaving: boolean;

    antworts: IAntwort[];

    quizfrages: IQuizFrage[];

    constructor(
        private jhiAlertService: JhiAlertService,
        private frageService: FrageService,
        private antwortService: AntwortService,
        private quizFrageService: QuizFrageService,
        private activatedRoute: ActivatedRoute
    ) {}

    ngOnInit() {
        this.isSaving = false;
        this.activatedRoute.data.subscribe(({ frage }) => {
            this.frage = frage;
        });
        this.antwortService.query().subscribe(
            (res: HttpResponse<IAntwort[]>) => {
                this.antworts = res.body;
            },
            (res: HttpErrorResponse) => this.onError(res.message)
        );
        this.quizFrageService.query().subscribe(
            (res: HttpResponse<IQuizFrage[]>) => {
                this.quizfrages = res.body;
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

    trackAntwortById(index: number, item: IAntwort) {
        return item.id;
    }

    trackQuizFrageById(index: number, item: IQuizFrage) {
        return item.id;
    }
}
