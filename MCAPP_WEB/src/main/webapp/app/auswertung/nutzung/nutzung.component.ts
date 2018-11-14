import { Component, OnInit, OnDestroy } from '@angular/core';
import { HttpErrorResponse, HttpResponse } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import { JhiEventManager, JhiAlertService } from 'ng-jhipster';

import { IQuiz } from 'app/shared/model/quiz.model';
import { Quiz } from 'app/shared/model/quiz.model';
import { Principal } from 'app/core';
import { NutzungService } from './nutzung.service';
import { Moment } from 'moment';

@Component({
    selector: 'jhi-quiz',
    templateUrl: './nutzung.component.html'
})
export class NutzungComponent implements OnInit, OnDestroy {
    quizzes: IQuiz[];
    currentAccount: any;
    eventSubscriber: Subscription;
    currentSearch: string;

    constructor(
        private quizService: NutzungService,
        private jhiAlertService: JhiAlertService,
        private eventManager: JhiEventManager,
        private activatedRoute: ActivatedRoute,
        private principal: Principal
    ) {
        this.currentSearch =
            this.activatedRoute.snapshot && this.activatedRoute.snapshot.params['search']
                ? this.activatedRoute.snapshot.params['search']
                : '';
    }

    loadAll() {
        if (this.currentSearch) {
            this.quizService
                .search({
                    query: this.currentSearch
                })
                .subscribe(
                    (res: HttpResponse<IQuiz[]>) => (this.quizzes = res.body),
                    (res: HttpErrorResponse) => this.onError(res.message)
                );
            return;
        }
        this.quizService.query().subscribe(
            (res: HttpResponse<IQuiz[]>) => {
                this.quizzes = res.body;
                this.currentSearch = '';

                if (this.quizzes) {
                    let sevenDays = new Date(Date.now());
                    let thirtydays = new Date(Date.now());

                    sevenDays.setDate(sevenDays.getDate() - 7);
                    thirtydays.setDate(thirtydays.getDate() - 30);

                    console.log(sevenDays);
                    console.log(thirtydays);

                    let seven = 0;
                    let thirty = 0;

                    this.quizzes.forEach(function(value) {
                        if (value.datum.toDate() >= sevenDays) {
                            seven++;
                        }
                        if (value.datum.toDate() >= thirtydays) {
                            thirty++;
                        }
                    });

                    const quiz1 = new Quiz();
                    quiz1.tage = 7;
                    quiz1.nutzung = seven;

                    const quiz2 = new Quiz();
                    quiz2.tage = 30;
                    quiz2.nutzung = thirty;

                    this.quizzes = [quiz1, quiz2];
                }
            },
            (res: HttpErrorResponse) => this.onError(res.message)
        );
    }

    search(query) {
        if (!query) {
            return this.clear();
        }
        this.currentSearch = query;
        this.loadAll();
    }

    clear() {
        this.currentSearch = '';
        this.loadAll();
    }

    ngOnInit() {
        this.loadAll();
        this.principal.identity().then(account => {
            this.currentAccount = account;
        });
        this.registerChangeInQuizzes();
    }

    ngOnDestroy() {
        this.eventManager.destroy(this.eventSubscriber);
    }

    trackId(index: number, item: IQuiz) {
        return item.id;
    }

    registerChangeInQuizzes() {
        this.eventSubscriber = this.eventManager.subscribe('quizListModification', response => this.loadAll());
    }

    private onError(errorMessage: string) {
        this.jhiAlertService.error(errorMessage, null, null);
    }
}
