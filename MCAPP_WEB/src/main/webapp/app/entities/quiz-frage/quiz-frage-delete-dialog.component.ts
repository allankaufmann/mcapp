import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { NgbActiveModal, NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { JhiEventManager } from 'ng-jhipster';

import { IQuizFrage } from 'app/shared/model/quiz-frage.model';
import { QuizFrageService } from './quiz-frage.service';

@Component({
    selector: 'jhi-quiz-frage-delete-dialog',
    templateUrl: './quiz-frage-delete-dialog.component.html'
})
export class QuizFrageDeleteDialogComponent {
    quizFrage: IQuizFrage;

    constructor(private quizFrageService: QuizFrageService, public activeModal: NgbActiveModal, private eventManager: JhiEventManager) {}

    clear() {
        this.activeModal.dismiss('cancel');
    }

    confirmDelete(id: number) {
        this.quizFrageService.delete(id).subscribe(response => {
            this.eventManager.broadcast({
                name: 'quizFrageListModification',
                content: 'Deleted an quizFrage'
            });
            this.activeModal.dismiss(true);
        });
    }
}

@Component({
    selector: 'jhi-quiz-frage-delete-popup',
    template: ''
})
export class QuizFrageDeletePopupComponent implements OnInit, OnDestroy {
    private ngbModalRef: NgbModalRef;

    constructor(private activatedRoute: ActivatedRoute, private router: Router, private modalService: NgbModal) {}

    ngOnInit() {
        this.activatedRoute.data.subscribe(({ quizFrage }) => {
            setTimeout(() => {
                this.ngbModalRef = this.modalService.open(QuizFrageDeleteDialogComponent as Component, { size: 'lg', backdrop: 'static' });
                this.ngbModalRef.componentInstance.quizFrage = quizFrage;
                this.ngbModalRef.result.then(
                    result => {
                        this.router.navigate([{ outlets: { popup: null } }], { replaceUrl: true, queryParamsHandling: 'merge' });
                        this.ngbModalRef = null;
                    },
                    reason => {
                        this.router.navigate([{ outlets: { popup: null } }], { replaceUrl: true, queryParamsHandling: 'merge' });
                        this.ngbModalRef = null;
                    }
                );
            }, 0);
        });
    }

    ngOnDestroy() {
        this.ngbModalRef = null;
    }
}
