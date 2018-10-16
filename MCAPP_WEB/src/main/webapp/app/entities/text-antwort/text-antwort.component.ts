import { Component, OnInit, OnDestroy } from '@angular/core';
import { HttpErrorResponse, HttpResponse } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import { JhiEventManager, JhiAlertService } from 'ng-jhipster';

import { ITextAntwort } from 'app/shared/model/text-antwort.model';
import { Principal } from 'app/core';
import { TextAntwortService } from './text-antwort.service';

@Component({
    selector: 'jhi-text-antwort',
    templateUrl: './text-antwort.component.html'
})
export class TextAntwortComponent implements OnInit, OnDestroy {
    textAntworts: ITextAntwort[];
    currentAccount: any;
    eventSubscriber: Subscription;
    currentSearch: string;

    constructor(
        private textAntwortService: TextAntwortService,
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
            this.textAntwortService
                .search({
                    query: this.currentSearch
                })
                .subscribe(
                    (res: HttpResponse<ITextAntwort[]>) => (this.textAntworts = res.body),
                    (res: HttpErrorResponse) => this.onError(res.message)
                );
            return;
        }
        this.textAntwortService.query().subscribe(
            (res: HttpResponse<ITextAntwort[]>) => {
                this.textAntworts = res.body;
                this.currentSearch = '';
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
        this.registerChangeInTextAntworts();
    }

    ngOnDestroy() {
        this.eventManager.destroy(this.eventSubscriber);
    }

    trackId(index: number, item: ITextAntwort) {
        return item.id;
    }

    registerChangeInTextAntworts() {
        this.eventSubscriber = this.eventManager.subscribe('textAntwortListModification', response => this.loadAll());
    }

    private onError(errorMessage: string) {
        this.jhiAlertService.error(errorMessage, null, null);
    }
}
