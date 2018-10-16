import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { NgbActiveModal, NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { JhiEventManager } from 'ng-jhipster';

import { IBildAntwort } from 'app/shared/model/bild-antwort.model';
import { BildAntwortService } from './bild-antwort.service';

@Component({
    selector: 'jhi-bild-antwort-delete-dialog',
    templateUrl: './bild-antwort-delete-dialog.component.html'
})
export class BildAntwortDeleteDialogComponent {
    bildAntwort: IBildAntwort;

    constructor(
        private bildAntwortService: BildAntwortService,
        public activeModal: NgbActiveModal,
        private eventManager: JhiEventManager
    ) {}

    clear() {
        this.activeModal.dismiss('cancel');
    }

    confirmDelete(id: number) {
        this.bildAntwortService.delete(id).subscribe(response => {
            this.eventManager.broadcast({
                name: 'bildAntwortListModification',
                content: 'Deleted an bildAntwort'
            });
            this.activeModal.dismiss(true);
        });
    }
}

@Component({
    selector: 'jhi-bild-antwort-delete-popup',
    template: ''
})
export class BildAntwortDeletePopupComponent implements OnInit, OnDestroy {
    private ngbModalRef: NgbModalRef;

    constructor(private activatedRoute: ActivatedRoute, private router: Router, private modalService: NgbModal) {}

    ngOnInit() {
        this.activatedRoute.data.subscribe(({ bildAntwort }) => {
            setTimeout(() => {
                this.ngbModalRef = this.modalService.open(BildAntwortDeleteDialogComponent as Component, {
                    size: 'lg',
                    backdrop: 'static'
                });
                this.ngbModalRef.componentInstance.bildAntwort = bildAntwort;
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
