import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { IQuizFrage } from 'app/shared/model/quiz-frage.model';

@Component({
    selector: 'jhi-quiz-frage-detail',
    templateUrl: './quiz-frage-detail.component.html'
})
export class QuizFrageDetailComponent implements OnInit {
    quizFrage: IQuizFrage;

    constructor(private activatedRoute: ActivatedRoute) {}

    ngOnInit() {
        this.activatedRoute.data.subscribe(({ quizFrage }) => {
            this.quizFrage = quizFrage;
        });
    }

    previousState() {
        window.history.back();
    }
}
