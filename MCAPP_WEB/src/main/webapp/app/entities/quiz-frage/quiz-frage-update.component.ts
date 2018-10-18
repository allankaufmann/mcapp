import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { HttpResponse, HttpErrorResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { JhiAlertService } from 'ng-jhipster';

import { IQuizFrage } from 'app/shared/model/quiz-frage.model';
import { QuizFrageService } from './quiz-frage.service';
import { IFrage } from 'app/shared/model/frage.model';
import { FrageService } from 'app/entities/frage';
import { IQuiz } from 'app/shared/model/quiz.model';
import { QuizService } from 'app/entities/quiz';

@Component({
    selector: 'jhi-quiz-frage-update',
    templateUrl: './quiz-frage-update.component.html'
})
export class QuizFrageUpdateComponent implements OnInit {
    quizFrage: IQuizFrage;
    isSaving: boolean;

    frages: IFrage[];

    quizzes: IQuiz[];

    constructor(
        private jhiAlertService: JhiAlertService,
        private quizFrageService: QuizFrageService,
        private frageService: FrageService,
        private quizService: QuizService,
        private activatedRoute: ActivatedRoute
    ) {}

    ngOnInit() {
        this.isSaving = false;
        this.activatedRoute.data.subscribe(({ quizFrage }) => {
            this.quizFrage = quizFrage;
        });
        this.frageService.query().subscribe(
            (res: HttpResponse<IFrage[]>) => {
                this.frages = res.body;
            },
            (res: HttpErrorResponse) => this.onError(res.message)
        );
        this.quizService.query().subscribe(
            (res: HttpResponse<IQuiz[]>) => {
                this.quizzes = res.body;
            },
            (res: HttpErrorResponse) => this.onError(res.message)
        );
    }

    previousState() {
        window.history.back();
    }

    save() {
        this.isSaving = true;
        if (this.quizFrage.id !== undefined) {
            this.subscribeToSaveResponse(this.quizFrageService.update(this.quizFrage));
        } else {
            this.subscribeToSaveResponse(this.quizFrageService.create(this.quizFrage));
        }
    }

    private subscribeToSaveResponse(result: Observable<HttpResponse<IQuizFrage>>) {
        result.subscribe((res: HttpResponse<IQuizFrage>) => this.onSaveSuccess(), (res: HttpErrorResponse) => this.onSaveError());
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

    trackQuizById(index: number, item: IQuiz) {
        return item.id;
    }
}
