import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { NgbActiveModal, NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { JhiEventManager } from 'ng-jhipster';

import { ITextAntwort } from 'app/shared/model/text-antwort.model';
import { TextAntwortService } from './text-antwort.service';

@Component({
    selector: 'jhi-text-antwort-delete-dialog',
    templateUrl: './text-antwort-delete-dialog.component.html'
})
export class TextAntwortDeleteDialogComponent {
    textAntwort: ITextAntwort;

    constructor(
        private textAntwortService: TextAntwortService,
        public activeModal: NgbActiveModal,
        private eventManager: JhiEventManager
    ) {}

    clear() {
        this.activeModal.dismiss('cancel');
    }

    confirmDelete(id: number) {
        this.textAntwortService.delete(id).subscribe(response => {
            this.eventManager.broadcast({
                name: 'textAntwortListModification',
                content: 'Deleted an textAntwort'
            });
            this.activeModal.dismiss(true);
        });
    }
}

@Component({
    selector: 'jhi-text-antwort-delete-popup',
    template: ''
})
export class TextAntwortDeletePopupComponent implements OnInit, OnDestroy {
    private ngbModalRef: NgbModalRef;

    constructor(private activatedRoute: ActivatedRoute, private router: Router, private modalService: NgbModal) {}

    ngOnInit() {
        this.activatedRoute.data.subscribe(({ textAntwort }) => {
            setTimeout(() => {
                this.ngbModalRef = this.modalService.open(TextAntwortDeleteDialogComponent as Component, {
                    size: 'lg',
                    backdrop: 'static'
                });
                this.ngbModalRef.componentInstance.textAntwort = textAntwort;
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
