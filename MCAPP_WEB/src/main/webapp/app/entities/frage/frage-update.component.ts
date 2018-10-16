import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { HttpResponse, HttpErrorResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { JhiAlertService } from 'ng-jhipster';

import { IFrage } from 'app/shared/model/frage.model';
import { FrageService } from './frage.service';
import { ITextAntwort } from 'app/shared/model/text-antwort.model';
import { TextAntwortService } from 'app/entities/text-antwort';
import { IBildAntwort } from 'app/shared/model/bild-antwort.model';
import { BildAntwortService } from 'app/entities/bild-antwort';
import { IQuizFrage } from 'app/shared/model/quiz-frage.model';
import { QuizFrageService } from 'app/entities/quiz-frage';

@Component({
    selector: 'jhi-frage-update',
    templateUrl: './frage-update.component.html'
})
export class FrageUpdateComponent implements OnInit {
    frage: IFrage;
    isSaving: boolean;

    textantworts: ITextAntwort[];

    bildantworts: IBildAntwort[];

    quizfrages: IQuizFrage[];

    constructor(
        private jhiAlertService: JhiAlertService,
        private frageService: FrageService,
        private textAntwortService: TextAntwortService,
        private bildAntwortService: BildAntwortService,
        private quizFrageService: QuizFrageService,
        private activatedRoute: ActivatedRoute
    ) {}

    ngOnInit() {
        this.isSaving = false;
        this.activatedRoute.data.subscribe(({ frage }) => {
            this.frage = frage;
        });
        this.textAntwortService.query().subscribe(
            (res: HttpResponse<ITextAntwort[]>) => {
                this.textantworts = res.body;
            },
            (res: HttpErrorResponse) => this.onError(res.message)
        );
        this.bildAntwortService.query().subscribe(
            (res: HttpResponse<IBildAntwort[]>) => {
                this.bildantworts = res.body;
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

    trackTextAntwortById(index: number, item: ITextAntwort) {
        return item.id;
    }

    trackBildAntwortById(index: number, item: IBildAntwort) {
        return item.id;
    }

    trackQuizFrageById(index: number, item: IQuizFrage) {
        return item.id;
    }
}
