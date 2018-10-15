/* tslint:disable max-line-length */
import { ComponentFixture, TestBed, inject, fakeAsync, tick } from '@angular/core/testing';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { Observable, of } from 'rxjs';
import { JhiEventManager } from 'ng-jhipster';

import { McappWebTestModule } from '../../../test.module';
import { AntwortDeleteDialogComponent } from 'app/entities/antwort/antwort-delete-dialog.component';
import { AntwortService } from 'app/entities/antwort/antwort.service';

describe('Component Tests', () => {
    describe('Antwort Management Delete Component', () => {
        let comp: AntwortDeleteDialogComponent;
        let fixture: ComponentFixture<AntwortDeleteDialogComponent>;
        let service: AntwortService;
        let mockEventManager: any;
        let mockActiveModal: any;

        beforeEach(() => {
            TestBed.configureTestingModule({
                imports: [McappWebTestModule],
                declarations: [AntwortDeleteDialogComponent]
            })
                .overrideTemplate(AntwortDeleteDialogComponent, '')
                .compileComponents();
            fixture = TestBed.createComponent(AntwortDeleteDialogComponent);
            comp = fixture.componentInstance;
            service = fixture.debugElement.injector.get(AntwortService);
            mockEventManager = fixture.debugElement.injector.get(JhiEventManager);
            mockActiveModal = fixture.debugElement.injector.get(NgbActiveModal);
        });

        describe('confirmDelete', () => {
            it('Should call delete service on confirmDelete', inject(
                [],
                fakeAsync(() => {
                    // GIVEN
                    spyOn(service, 'delete').and.returnValue(of({}));

                    // WHEN
                    comp.confirmDelete(123);
                    tick();

                    // THEN
                    expect(service.delete).toHaveBeenCalledWith(123);
                    expect(mockActiveModal.dismissSpy).toHaveBeenCalled();
                    expect(mockEventManager.broadcastSpy).toHaveBeenCalled();
                })
            ));
        });
    });
});
