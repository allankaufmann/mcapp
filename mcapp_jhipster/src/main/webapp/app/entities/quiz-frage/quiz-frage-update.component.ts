import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { HttpResponse, HttpErrorResponse } from '@angular/common/http';
import { Observable } from 'rxjs';

import { IQuizFrage } from 'app/shared/model/quiz-frage.model';
import { QuizFrageService } from './quiz-frage.service';

@Component({
    selector: 'jhi-quiz-frage-update',
    templateUrl: './quiz-frage-update.component.html'
})
export class QuizFrageUpdateComponent implements OnInit {
    quizFrage: IQuizFrage;
    isSaving: boolean;

    constructor(private quizFrageService: QuizFrageService, private activatedRoute: ActivatedRoute) {}

    ngOnInit() {
        this.isSaving = false;
        this.activatedRoute.data.subscribe(({ quizFrage }) => {
            this.quizFrage = quizFrage;
        });
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
}
