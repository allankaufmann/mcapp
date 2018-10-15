import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { NgbActiveModal, NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { JhiEventManager } from 'ng-jhipster';

import { IAntwort } from 'app/shared/model/antwort.model';
import { AntwortService } from './antwort.service';

@Component({
    selector: 'jhi-antwort-delete-dialog',
    templateUrl: './antwort-delete-dialog.component.html'
})
export class AntwortDeleteDialogComponent {
    antwort: IAntwort;

    constructor(private antwortService: AntwortService, public activeModal: NgbActiveModal, private eventManager: JhiEventManager) {}

    clear() {
        this.activeModal.dismiss('cancel');
    }

    confirmDelete(id: number) {
        this.antwortService.delete(id).subscribe(response => {
            this.eventManager.broadcast({
                name: 'antwortListModification',
                content: 'Deleted an antwort'
            });
            this.activeModal.dismiss(true);
        });
    }
}

@Component({
    selector: 'jhi-antwort-delete-popup',
    template: ''
})
export class AntwortDeletePopupComponent implements OnInit, OnDestroy {
    private ngbModalRef: NgbModalRef;

    constructor(private activatedRoute: ActivatedRoute, private router: Router, private modalService: NgbModal) {}

    ngOnInit() {
        this.activatedRoute.data.subscribe(({ antwort }) => {
            setTimeout(() => {
                this.ngbModalRef = this.modalService.open(AntwortDeleteDialogComponent as Component, { size: 'lg', backdrop: 'static' });
                this.ngbModalRef.componentInstance.antwort = antwort;
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
